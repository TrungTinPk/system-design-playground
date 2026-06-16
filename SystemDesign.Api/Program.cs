using SystemDesign.Api.Endpoints;
using SystemDesign.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);

// Auth scaffolding. TODO: configure a real authentication scheme (e.g. JWT Bearer)
// so RequireAuthorization()-protected endpoints can authenticate users.
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapDiagramEndpoints();

app.Run();
