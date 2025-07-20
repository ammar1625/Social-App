using ApiAuthenticationAndSecurity.User_Management;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialAppApi.Mapping;
using SocialAppApi.websockets_service;
using System.Text;

var options = new WebApplicationOptions
{
    // ContentRootPath ?? ??? ??????? (??? ??? csproj)
    ContentRootPath = AppContext.BaseDirectory,

    // ??? ???? ???? wwwroot ???? ContentRootPath
    WebRootPath = "wwwroot",
};

var builder = WebApplication.CreateBuilder(options);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add security definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Add security requirement
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

//websocket service 
builder.Services.AddHostedService<WebSocketServerService>();


//cores enabling 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//inject automapper configs class
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

//get email configurations from appsettings 
var EmailConfigurations = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfigurations>();

//inject email configurations using dependency injection
builder.Services.AddSingleton(EmailConfigurations);

//inject email services class using dependency injection
builder.Services.AddScoped<IemailService, EmailService>();

//get jwt secret key
var Key = Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ValidateIssuer = false,
        // ValidIssuer = builder.Configuration.GetValue<string>("issuer"),
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
        //ValidAudience = builder.Configuration.GetValue<string>("audience")
    };
});


//builder.WebHost.UseWebRoot("wwwroot");

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
