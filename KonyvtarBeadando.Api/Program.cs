using KonyvtarSzerver.Api;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
	(_, loggerConfiguration) => loggerConfiguration
		.MinimumLevel.Information()
		.WriteTo.Console());

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<KonyvtarSzerverContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
		options.UseLazyLoadingProxies();
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(o => o.AllowAnyOrigin().AllowAnyOrigin().AllowAnyHeader());

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
