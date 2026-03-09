using Microsoft.EntityFrameworkCore;
using Web_BongDa_Login.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Thêm Services TRƯỚC builder.Build()
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 🔹 Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();   // phải đặt trước UseAuthorization

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
