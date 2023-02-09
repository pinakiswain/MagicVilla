using MagicVilla_VillaAPI;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.log;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MagicVillaDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("log/VillLog.txt").CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddScoped<IVillaRepository, VillaRepository>();// register the Villa repository
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();// register the VillaNumber repository
builder.Services.AddAutoMapper(typeof(MappingCofig));//Register Automapper class
builder.Services.AddControllers(options=>options.ReturnHttpNotAcceptable=true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging,LoggingV2>();
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
