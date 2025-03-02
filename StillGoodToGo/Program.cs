using Microsoft.EntityFrameworkCore;
using StillGoodToGo.DataContext;
using StillGoodToGo.Services.ServicesInterfaces;
using StillGoodToGo.Services;
using StillGoodToGo.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StillGoodToGoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StillGoodToGoDB")));

// Service Injection
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();

// Mapper Injection
builder.Services.AddScoped<EstablishmentMapper>();


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
