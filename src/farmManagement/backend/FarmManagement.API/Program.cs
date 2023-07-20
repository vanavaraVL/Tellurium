using AutoMapper;
using FarmManagement.API.Middleware.Extensions;
using FarmManagement.Dal.InMemory.Extensions;
using FarmManagement.Services.Extensions;
using FarmManagement.Services.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .RegisterServices()
    .RegisterInMemoryStorage();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLazyCache();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(b =>
        b.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AnimalMappingProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterMiddleware();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
