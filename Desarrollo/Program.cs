//using System.Configuration;
//using Microsoft.Extensions.Configuration;
//using Desarrollo.Data;
using Microsoft.EntityFrameworkCore;
using Desarrollo.ContextDB;
using Desarrollo.Data;
using Desarrollo.Services;

//using Context = Desarrollo.ContextDB.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();//agregar esto para que tome los controladores desde la carpeta controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TestContext>(options=>options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));//esto es para obtener la conexion a la base de datos
builder.Services.AddTransient<IUserService, UserServices>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapControllers();//agregar esto para que mapee todos los controladores que tengamos;

app.Run();

