using Biblioteca.Application.Interfaces;
using Biblioteca.Application.Services;
using Biblioteca.Application.Validators;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Biblioteca.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Biblioteca API",
        Version = "v1",
        Description = "API RESTful de gerenciamento da Biblioteca",
      });
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BibliotecaContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ILivroService, LivroService>();

builder.Services.AddValidatorsFromAssemblyContaining<AutorDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddCors(p => p.AddPolicy("AllowAll",
    b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();

//if (app.Environment.IsDevelopment())
 app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca API v1");
        options.RoutePrefix = "swagger";
    });
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
