using API_Motel_Luxor.Db;
using API_Motel_Luxor.Services.Administadores;
using API_Motel_Luxor.Services.Colaboradores;
using API_Motel_Luxor.Services.Senha;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crud Colabradores", Version = "v1" });
    c.EnableAnnotations();
    c.ExampleFilters();
});


builder.Services.AddScoped<IColaboradoresRepository, ColaboradoresRepository>();
builder.Services.AddScoped<IAdminstradoresRepository, AdministadoresRepository>();
builder.Services.AddScoped<ISenhaRepository, SenhaRepository>();

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud Colabradores v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
