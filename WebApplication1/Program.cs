using Invoice_Api;
using Invoice_Api.Auth;
using Invoice_Api.Data;
using Invoice_Api.Models.Humans;
using Invoice_Api.Services;
using Invoice_Api.Services.Jwt_service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddSwagger();
builder.Services.AddDbContext<InvoiceDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceDbContext"));
    }
    );
var jwtConfig = new JwtConfig();
builder.Configuration.GetSection("JWT").Bind(jwtConfig);
builder.Services.AddScoped<IJwtService,JwtService>();




builder.Services.AddScoped<IInvoiceService, InvoiceService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
