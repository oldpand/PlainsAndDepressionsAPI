using MediatR;
using PlainsAndDepressions.Services.Commands;
using PlainsAndDepressions.Services.Handlers;
using PlainsAndDepressions.Services.Results;
using PlainsAndDepressions.Services.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMediatR(Assembly.GetExecutingAssembly())
    .AddScoped<IRequestHandler<MeadowProcessCommand, Result>, ProcessCommandHandler>()
    ;

builder.Services
    .AddTransient<IRabbitMqService, RabbitMqService>();

builder.Services
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
