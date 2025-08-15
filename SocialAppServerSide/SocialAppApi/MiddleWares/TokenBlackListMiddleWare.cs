using StackExchange.Redis;

namespace SocialAppApi.MiddleWares
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConnectionMultiplexer _redis;

        public TokenBlacklistMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
        {
            _next = next;
            _redis = redis;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessToken = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(accessToken))
            {
                var db = _redis.GetDatabase();
                var isBlacklisted = await db.StringGetAsync($"blacklisted_token:{accessToken}");

                if (!isBlacklisted.IsNullOrEmpty)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new { error = "Unauthorized - Token revoked" });
                    return;
                }
            }

            await _next(context);
        }
    }
}
