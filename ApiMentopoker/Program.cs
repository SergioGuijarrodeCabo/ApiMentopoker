using ApiMentopoker.Data;
using ApiMentopoker.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Mentopoker",
        Description = "by Sergio Guijarro"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
        , name: "Api Mentopoker");
    options.RoutePrefix = "";
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

