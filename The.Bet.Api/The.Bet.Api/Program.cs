using The.Bet.Api.Middleware;
using The.Bet.Api.Repository;
using The.Bet.Api.Repository.Interfaces;
using The.Bet.Api.Services;
using The.Bet.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddTransient<IBetRepository, BetRepository>();
builder.Services.AddTransient<IBetService, BetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandling>();
app.UseAuthorization();

app.MapControllers();

app.Run();
