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

var mapperConfig = new MapperConfiguration(mapperConfigExpression, loggerFactory);

mapperConfig.AssertConfigurationIsValid();

builder.Services.AddSingleton<IMapper>(mapperConfig.CreateMapper());

builder.Services.AddDbContext<ApiDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ResolveDependecies();
builder.Services.AddControllers();
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
