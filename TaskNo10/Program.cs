using TaskNo10.Services;
using TaskNo10.Data;
using Microsoft.EntityFrameworkCore;
using TaskNo10.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

// DB Context and Service Registration
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("BookDb"));
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Middleware Registration
app.UseMiddleware<RequestLoggingMiddleware>();

// Swagger Registration
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
