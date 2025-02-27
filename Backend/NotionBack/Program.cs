using Microsoft.EntityFrameworkCore;
using NotionBack.Db.Infrastructure;
using NotionBack.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddNotionContext(connectionString!);
builder.Services.AddUnitOfWorkService();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.TMP();

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
