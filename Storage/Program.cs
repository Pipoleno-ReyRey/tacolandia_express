using Microsoft.EntityFrameworkCore;
using Storage.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

var credentials = builder.Configuration.GetConnectionString("credentials");
builder.Services.AddDbContext<StorageDB>(opt => opt.UseMySql(credentials, new MySqlServerVersion(ServerVersion.AutoDetect(credentials))));
builder.Services.AddScoped<StorageService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
