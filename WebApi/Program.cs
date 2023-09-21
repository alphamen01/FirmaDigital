using AccesoDatos.Context;
using AccesoDatos.Operations;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                        builder => builder.AllowAnyOrigin()
                                                        .AllowAnyHeader()
                                                        .AllowAnyMethod()));    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PruebaContext>(opt =>
                                             opt.UseNpgsql(builder.Configuration.GetConnectionString("MyDB"))); //AddEntityFrameworkNpgsql().
builder.Services.AddScoped<FirmadigitalDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowWebapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
