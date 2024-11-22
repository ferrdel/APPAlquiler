using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Uso del DbContext
builder.Services.AddDbContext<AlquilerDbContext>
    (
        options=>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                )    
    );

builder.Services.AddScoped<IBikeRepository, BikeRepository>();
builder.Services.AddScoped<IBikeService, BikeService>();

builder.Services.AddScoped<IBoatRepository, BoatRepository>();
builder.Services.AddScoped<IBoatService, BoatService>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<IModelService, ModelService>();

builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();

builder.Services.AddScoped<ITypeMotorcycleRepository, TypeMotorcycleRepository>();
builder.Services.AddScoped<ITypeMotorcycleService, TypeMotorcycleService>();


builder.Services.AddControllers()
    //Agregado para errores ciclicos
    //.AddJsonOptions(x =>     //agrega un id adicional para referenciar las relaciones
      //  x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

    .AddJsonOptions(options =>  //deja en null el objeto relacion, las ignora
     {
         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
     })

;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS
app.UseCors(
    x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    //.WithOrigins("https://mybeautifullpage.com")
    .AllowAnyOrigin()
    //.SetIsOriginAllowed(origin => true)
);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
