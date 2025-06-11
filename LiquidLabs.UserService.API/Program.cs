using LiquidLabs.UserService.API.Services;
using LiquidLabs.UserService.DataAccess.Extensions;
using LiquidLabs.UserService.Services.Services.Interfaces;
using LiquidLabs.UserService.Services.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<IExternalUserService, ExternalUserService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["user_api"]);
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDataAccessServices();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
