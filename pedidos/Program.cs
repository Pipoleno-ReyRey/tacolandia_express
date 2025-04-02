using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("credentials");

builder.Services.AddDbContext<OrdersDishDb>(db =>
    db.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddScoped<DishesService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

