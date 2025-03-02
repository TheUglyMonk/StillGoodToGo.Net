using Microsoft.EntityFrameworkCore;
using StillGoodToGo.DataContext;
using StillGoodToGo.Mappers;
using StillGoodToGo.Services;
using StillGoodToGo.Services.ServicesInterfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StillGoodToGoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GoodToGoDotNetDB")));

// Register services
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
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
