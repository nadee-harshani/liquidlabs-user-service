using LiquidLabs.UserService.API.Services;
using LiquidLabs.UserService.DataAccess.Extensions;
using LiquidLabs.UserService.Services.Services.Interfaces;
using LiquidLabs.UserService.Services.Services;
using LiquidLabs.UserService.API.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<IExternalUserService, ExternalUserService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["user_api"]);
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDataAccessServices();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDataProtection();
builder.Services.AddSingleton<EncryptService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints. Use 'X-Api-Key: {userservice@apikey}'",
        In = ParameterLocation.Header,
        Name = "X-Api-Key",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeySchema"

    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header
            },
            new List<string>()
        }

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<AuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
