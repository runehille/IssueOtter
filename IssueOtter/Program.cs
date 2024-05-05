using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using IssueOtter.Api.Utils.Auth;
using IssueOtter.Infrastructure.Repositories;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Services.Project;
using IssueOtter.Core.Services.Issue;
using IssueOtter.Core.Services.Comment;
using IssueOtter.Core.Services.User;

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

builder.Services.AddControllers()
  .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); ;
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

  options.SchemaFilter<EnumSchemaFilter>();
});

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();

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

