using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Exemplo de configuração direto na program.cs se desejar use método de extensão
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.Console()
//    .WriteTo.File("Logs/MyLogApplication-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

// Exemplo da configuracao ficar na appsettings.json
Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

// Adicionando o Serilog no container de serviços, para ser usado na requisição
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adicionando o serviço do Serilog no Pipeline da requisição, como um middleware
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
