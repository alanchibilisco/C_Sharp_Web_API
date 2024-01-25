using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();//agregar esto para que tome los controladores desde la carpeta controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>{
    options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                // Log información sobre el usuario autenticado
                var username = context.Principal.Identity?.Name;
                Console.WriteLine($"Usuario autenticado: {username}");

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                // Log información sobre fallos de autenticación
                Console.WriteLine($"Error de autenticación: {context.Exception}");
                return Task.CompletedTask;
            }
        };
    options.TokenValidationParameters=new TokenValidationParameters{
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer="http://localhost:5174",
        ValidAudience="ApiRest",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECRET_KEY_TOKEN_TEST_FOR_LEARNING")),
    };
});
builder.Services.AddAuthorization(options=>{
    options.AddPolicy("IsAdmin", policy=>policy.RequireRole("Admin"));
});
builder.Services.AddSwaggerGen(c=>{
    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Bearer",
        Description = "Ingrese el token JWT obtenido al iniciar sesión.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } },
    };

    c.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();


app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();//agregar esto para que mapee todos los controladores que tengamos;

app.Run();


