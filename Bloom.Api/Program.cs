using AutoMapper;
using Bloom.Api.Configuration;
using Bloom.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

var mapperConfigExpression = new MapperConfigurationExpression();
mapperConfigExpression.AddProfile<MappingProfile>();

// Cria a configuração com LoggerFactory
var mapperConfig = new MapperConfiguration(mapperConfigExpression, loggerFactory);

// Valida os mapeamentos (opcional)
mapperConfig.AssertConfigurationIsValid();

// Cria e registra o IMapper
builder.Services.AddSingleton<IMapper>(mapperConfig.CreateMapper());

builder.Services.AddDbContext<ApiDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ResolveDependecies();

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
