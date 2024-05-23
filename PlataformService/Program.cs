using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PlataformService.Data;
using PlataformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMem"));

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddHttpClient<ICommandDataClients, HttpCommandDataClient>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

Console.WriteLine($"Command Service Endpoint {configuration["CommanService"]}");

var app = builder.Build();

PrepDb.PrepPopulation(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints =>

endpoints.MapControllers()

);

app.Run();


