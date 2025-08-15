using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Net;
using System.Threading.Tasks;

namespace SocialAppApi.Middlewares
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<TokenBlacklistMiddleware> _logger;

        public TokenBlacklistMiddleware(
            RequestDelegate next,
            IConnectionMultiplexer redis,
            ILogger<TokenBlacklistMiddleware> logger)
        {
            _next = next;
            _redis = redis;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            const string BEARER_PREFIX = "Bearer ";
            string? authHeader = context.Request.Headers.Authorization.FirstOrDefault();

            // If no Authorization header or not Bearer, skip
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith(BEARER_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            string accessToken = authHeader[BEARER_PREFIX.Length..].Trim();

            if (string.IsNullOrEmpty(accessToken))
            {
                await _next(context);
                return;
            }

            try
            {
                IDatabase db = _redis.GetDatabase();
                RedisValue isBlacklisted = await db.StringGetAsync($"blacklisted_token:{accessToken}");

                if (!isBlacklisted.IsNullOrEmpty)
                {
                    _logger.LogWarning("Blocked request with blacklisted token: {Token}", accessToken);

                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "Token has been revoked and is no longer valid."
                    });
                    return;
                }
            }
            catch (RedisConnectionException ex)
            {
                _logger.LogError(ex, "Redis connection failed during token blacklist check. Connection: {Configuration}",
                    _redis.Configuration);

                // 🔶 Option 1: Allow request to continue (less secure but highly available)
                // 🔶 Option 2: Reject with 500 or 401 if Redis is critical (more secure)
                // We'll allow it to continue for availability
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while checking Redis for blacklisted token.");
                // Proceed — don't let Redis crash request flow
            }

            // Token is valid and not blacklisted — continue
            await _next(context);
        }
    }

  
}