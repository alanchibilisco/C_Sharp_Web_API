using System.Text;
using Desarrollo.Services.helpers;
using Desarrollo.Services;
using Desarrollo.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();//agregar esto para que tome los controladores desde la carpeta controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>{
    
    options.TokenValidationParameters=new TokenValidationParameters{
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer=Security.IssuerToken,
        ValidAudience=Security.AudienceToken,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Security.SecretKey)),
    };
});
builder.Services.AddAuthorization(options=>{
    options.AddPolicy("IsAdmin", policy=>policy.RequireRole("Admin"));
});
builder.Services.AddSwaggerGen(c=>{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese el token JWT obtenido al iniciar sesión.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    /*var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } },
    };

    c.AddSecurityRequirement(securityRequirement);*/
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
string? db=Environment.GetEnvironmentVariable("DB_CONNECTION");
string dir=Environment.CurrentDirectory;
System.Console.WriteLine("###Directorio--> {0}",dir);
System.Console.WriteLine("###DB--> {0}",db);
var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();//agregar esto para que mapee todos los controladores que tengamos;

app.Run();


