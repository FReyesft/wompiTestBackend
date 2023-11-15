using Microsoft.EntityFrameworkCore;
using Wompi.Business.Services;
using Wompi.Core;
using Wompi.Core.IRepositories;
using Wompi.Data;
using Wompi.Data.Context;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());
var logger = loggerFactory.CreateLogger("Startup");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WompiDevDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WompiDevDb")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient<GeLinkService>(client =>
{
    client.BaseAddress =  new Uri("https://sandbox.wompi.co/v1/");
});
builder.Services.AddHttpClient<ReceiverWompiEventService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
