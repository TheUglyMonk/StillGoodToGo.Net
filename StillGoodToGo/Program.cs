using Microsoft.EntityFrameworkCore;
using StillGoodToGo.DataContext;
using StillGoodToGo.Services.ServicesInterfaces;
using StillGoodToGo.Services;
using StillGoodToGo.Mappers;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StillGoodToGoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StillGoodToGoDB")));

// Register services
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
builder.Services.AddScoped<EstablishmentMapper>();
builder.Services.AddScoped<IPublicationService, PublicationService>();
builder.Services.AddScoped<PublicationMapper>();

// Service Injection
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
builder.Services.AddScoped<IPublicationService, PublicationService>();

// Mapper Injection
builder.Services.AddScoped<EstablishmentMapper>();
builder.Services.AddScoped<PublicationMapper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/*
app.UseHttpsRedirection();*/

app.UseAuthorization();

app.MapControllers();

app.Run();
