using ApiMentopoker.Data;
using ApiMentopoker.Helpers;
using ApiMentopoker.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.Generation.Processors.Security;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

//PONEMOS EL HELPER EN LA INYECCION
builder.Services.AddSingleton<HelperOAuthToken>();
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
//AÑADIMOS LAS OPCIONES DE AUTENTIFICACION
builder.Services.AddAuthentication(helper.GetAuthenticationOptions())
    .AddJwtBearer(helper.GetJwtOptions());


// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryEstadisticas>();
builder.Services.AddTransient<RepositoryLogin>();
builder.Services.AddTransient<RepositoryTablas>();

builder.Services.AddDbContext<MentopokerContext>
    (options => options.UseSqlServer(connectionString));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Api Mentopoker",
//        Description = "by Sergio Guijarro"
//    });
//});
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api OAuth Empleados";
    document.Description = "Api Token Empleados 2023.  Ejemplo OAuth";
    // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER,
    // PERMITE AÑADIR EL TOKEN JWT A LA CABECERA.
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
        }
    );
    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();
app.UseOpenApi();
app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/css/bootstrap.css");
    options.InjectStylesheet("/css/monokai.css");
    //options.InjectStylesheet("/css/material3x.css");
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api Mentopoker v1");
    options.RoutePrefix = "";
    options.DocExpansion(DocExpansion.None);
});
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
//        , name: "Api Mentopoker");
//    options.RoutePrefix = "";
//});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
