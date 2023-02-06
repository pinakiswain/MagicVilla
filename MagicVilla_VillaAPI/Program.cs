using MagicVilla_VillaAPI.log;
using MagicVilla_VillaAPI.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
//    .WriteTo.File("log/VillLog.txt").CreateLogger();
//builder.Host.UseSerilog();
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
