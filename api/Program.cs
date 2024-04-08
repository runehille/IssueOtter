using System.Security.Claims;
using api.Data;
using api.Interfaces;
using api.Repositories;
using api.Utils.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
  options.AddPolicy("defaultPolicy",
                    policy =>
                    {
                      policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
});

var domain = builder.Configuration["Auth0:Domain"];
var audience = builder.Configuration["Auth0:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
  options.Authority = domain;
  options.Audience = audience;
  options.TokenValidationParameters = new TokenValidationParameters
  {
    NameClaimType = ClaimTypes.NameIdentifier
  };
});

builder.Services
  .AddAuthorization(options =>
  {
    options.AddPolicy(
      "read:messages",
      policy => policy.Requirements.Add(
        new HasScopeRequirement("read:messages", domain!)
      )
    );
  });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "IssueOtterAPI",
    Version = "v1",
    Description = "API for IssueOtter"
  });

  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "bearer"
  });

  options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
  });
});

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("defaultPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

