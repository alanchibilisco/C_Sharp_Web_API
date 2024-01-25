using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(/*opt=>{
    var policy=new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}*/);//agregar esto para que tome los controladores desde la carpeta controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>{
    
    options.TokenValidationParameters=new TokenValidationParameters{
        ValidateIssuer=false,
        ValidateAudience=false,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        //ValidIssuer="http://localhost:5174",
        //ValidAudience="ApiRest",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECRET_KEY_TOKEN_TEST_FOR_LEARNING")),
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


