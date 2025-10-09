using Microsoft.EntityFrameworkCore;
using To_do_List_API.Data;

var builder = WebApplication.CreateBuilder(args);

//1. Get connection string from the appsettings.json file
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Add database context to builder's service.
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
