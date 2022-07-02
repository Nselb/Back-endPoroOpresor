using Back_end_Poro_Opresor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GameDBContext>(opt => 
opt.UseSqlServer(builder.Configuration.GetConnectionString("GameStatsConnectionString"))
);
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy( policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.WithOrigins("http://192.168.200.26:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
