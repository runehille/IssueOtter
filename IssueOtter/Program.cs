using System.Text.Json.Serialization;
using IssueOtter.Api.Auth;
using IssueOtter.Core.Interfaces;
using IssueOtter.Core.Services.Comment;
using IssueOtter.Core.Services.Issue;
using IssueOtter.Core.Services.Project;
using IssueOtter.Core.Services.User;
using IssueOtter.Infrastructure.Repositories;
using IssueOtter.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
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
                .AllowAnyMethod()
                .Build();
        });
});

if (builder.Environment.IsDevelopment())
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Dev";
        options.DefaultChallengeScheme = "Dev";
    }).AddScheme<AuthenticationSchemeOptions, DevAuthHandler>("Dev", null);
else
    Auth0.RegisterAuth0(builder);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddSwaggerGen(options =>
{
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
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

// Database Configuration
if (builder.Environment.IsDevelopment())
{
    // Use InMemory database for development
    builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("IssueOtterDev"); });

    // Register seeding service for development
    builder.Services.AddScoped<DataSeedingService>();
}
else
{
    // Use MySQL for production
    var connectionString = builder.Configuration.GetConnectionString("MySqlConnectionString");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });
}

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Seed data in development environment
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seedingService = scope.ServiceProvider.GetRequiredService<DataSeedingService>();
    await seedingService.SeedDataAsync();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("defaultPolicy");
app.MapControllers();
app.Run();