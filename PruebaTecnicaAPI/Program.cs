using System.Globalization;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using PruebaTecnicaAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.HttpOverrides;
using PruebaTecnicaAPI.Util;

var builder = WebApplication.CreateBuilder(args);

var supportedCultures = new[]
{
    new CultureInfo("es-ES"), // Español - España
};

// Configura valores de appsettings
var appSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
builder.Configuration.AddJsonFile(appSettingsPath, optional: true, reloadOnChange: false);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Configura Serilog para registrar en un archivo de texto
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        "logs/InterActionLog-.txt", // Ajusta la ruta y el nombre del archivo
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
// Configura el proveedor de registro de Serilog
builder.Logging.AddSerilog();

// Add services to the container.

builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddControllers();

//string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
//                            builder.Configuration.GetConnectionString("PT");

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

//Configuracion de idioma
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-ES");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddLocalization();
//Fin Configuracion de idioma

//Inicio de verificacion del JWT
var key = Encoding.ASCII.GetBytes("MIHcAgEBBEIBASFZbNv1GJv5QDwzj5AN2wogCd2XMw5ykPds/AZgc6EsCp4Brzd7SXoK0lISMURaUNBy7xLem9/dpnM4kTs6NBugBwYFK4EEACOhgYkDgYYABAF2p+x8h/rhGCW5bSuf47Zm1z619eGEf2Q8i7fF3DIIRGRlWXnLm8LVBrtPzzN+6G3zb4QMigdzjOCBlQxU5QMW1gDWi79Y5mkbNVQOuTor7cleqRqV46er10D3L8keYKNL978yDHj+dpLlRPdb5Rs5ec8A7dhIa4FslSLu3ausvkrOvQ==");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Cambia a 'true' en producción
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
//Fin de verificacion del JWT

//Inicio de configuiraciones de cors
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});
//Fin de configuiraciones de cors

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseHttpsRedirection();
 
app.UseCors("AllowOrigin");
app.UseMiddleware<CorsErrorMiddleware>();
app.UseMiddleware<UnauthorizedMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
