using InsumosAPI.Entities;
using InsumosAPI.Middleware;
using InsumosAPI.Repositories.ClienteRepository;
using InsumosAPI.Repositories.LaboratorioRepository;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Repositories.UsuarioRepository;
using InsumosAPI.Repositories.ProveedorRepository;
using InsumosAPI.Services.ClienteService;
using InsumosAPI.Services.LaboratorioService;
using InsumosAPI.Services.LoginService;
using InsumosAPI.Services.UsuarioService;
using InsumosAPI.Services.ProveedorService;
using InsumosAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using InsumosAPI.Repositories.ProductoRepository;
using InsumosAPI.Services.ProductoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Agregar el contexto de la base de datos
builder.Services.AddScoped<IUserAccessRepository, UserAccessRepository>();
builder.Services.AddScoped<CRUDInterceptor>();

builder.Services.AddDbContext<InsumosDBContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetRequiredService<CRUDInterceptor>();
    options.UseSqlServer(builder.Configuration.GetConnectionString("InsumosDBConnection"))
           .AddInterceptors(interceptor);
});

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<ILaboratorioRepository, LaboratorioRepository>();
builder.Services.AddScoped<ILaboratorioService, LaboratorioService>();

builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Apptelink", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
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
            new string[] {}
        }
    });

});

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Environment.GetEnvironmentVariable("JWTIssuer"),
        ValidAudience = Environment.GetEnvironmentVariable("JWTAudience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTKey")))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddAuthorization();

// Configuración de logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddFilter("System.IdentityModel.Tokens.Jwt", LogLevel.Debug);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseGlobalExceptionHandler();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthorization();

app.MapControllers();

app.Run();