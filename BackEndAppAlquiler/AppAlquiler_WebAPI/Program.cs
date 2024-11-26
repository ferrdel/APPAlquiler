using AppAlquiler_BusinessLayer.Interfaces;
using AppAlquiler_BusinessLayer.Services;
using AppAlquiler_DataAccessLayer.Converters;
using AppAlquiler_DataAccessLayer.Data;
using AppAlquiler_DataAccessLayer.Interfaces;
using AppAlquiler_DataAccessLayer.Models;
using AppAlquiler_DataAccessLayer.Repositories;
using AppAlquiler_DataAccessLayer.Seeds;
using AppAlquiler_WebAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

builder.Services.AddIdentityCore<User>(
    options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddRoles<IdentityRole<int>>()
    .AddRoleManager<RoleManager<IdentityRole<int>>>()
    .AddSignInManager<SignInManager<User>>()
    .AddRoleValidator<RoleValidator<IdentityRole<int>>>()
    .AddEntityFrameworkStores<AlquilerDbContext>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

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

builder.Services.AddScoped<IRentService, RentService>();


builder.Services.AddControllers()
    //Agregado para errores ciclicos
    //.AddJsonOptions(x =>     //agrega un id adicional para referenciar las relaciones
      //  x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

    .AddJsonOptions(options =>  //deja en null el objeto relacion, las ignora
     {
         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
         options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
         options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
     })

;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
     setup =>
     {
         setup.SwaggerDoc("v1", new OpenApiInfo { Title = "AppAlquiler", Version = "v1" });
         //Agregado para cambiar el Json
         setup.SchemaFilter<SwaggerSchemaFilter>();
         setup.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
         setup.MapType<TimeOnly>(() => new OpenApiSchema { Type = "string", Format = "time" });

         setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             Name = "Authorization",
             In = ParameterLocation.Header,
             Type = SecuritySchemeType.ApiKey,
             Scheme = "Bearer",
             BearerFormat = "JWT",
             Description = "Input your bearer token in this format - Bearer {your token} to access this API"
         });
         setup.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "Bearer",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new string[] {}
            }
        });
     }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            NameClaimType = "nameid", // Mapea el nameid como el identificador principal
            //RoleClaimType = "role",    // Mapea el role para roles
            //ValidAudience = builder.Configuration["Jwt:Audience"],
            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"));
});

var app = builder.Build();

//Luego del build, podemos hacer uso de los servicios registrados, como el context, para la carga de datos iniciales
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AlquilerDbContext>();
    //context.Database.EnsureCreated(); // This will create the database if it does not exist
    context.Database.Migrate(); // This will apply any pending migrations

    //Seed the database
    //Movemos las clases seed a un archivo separado, en el proyecto de DataAccessLayer
    SeedUsersAndRoles.Initialize(services);
    SeedData.Initialize(services);
}

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

//AGREGADO 
app.UseAuthentication();                //Faltaba

app.UseAuthorization();

app.MapControllers();

app.Run();

