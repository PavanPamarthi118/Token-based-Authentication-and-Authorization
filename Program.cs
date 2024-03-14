using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Token_based_Authentication___Authorization.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
          {
                new OpenApiSecurityScheme
                  {
                      Reference = new OpenApiReference
                      {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                      }
                  },
                  new string[] {}
          }
      });
});

// Retrieve JWT configurations from appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");
var tokenExpiration = jwtSettings.GetValue<int>("TokenExpiration");
var issuer = jwtSettings.GetValue<string>("Issuer");
var audience = jwtSettings.GetValue<string>("Audience");

// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
        };
    });

// Register AuthService with configured parameters
builder.Services.AddSingleton<AuthService>(new AuthService(secretKey, tokenExpiration, issuer, audience));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapControllers();


app.Run();
