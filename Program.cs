using qlgiaidau.Data;
using qlgiaidau.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký repository
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

// Đăng ký MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // nếu chưa đăng nhập thì chuyển về Login
        options.AccessDeniedPath = "/Account/AccessDenied"; // nếu không đủ quyền
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Seed dữ liệu mẫu
using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
    repo.SeedSampleTeam();
    repo.SeedSampleMatches();
    repo.SeedSampleMatchResults();
    repo.SeedSamplePlayerStats();
    repo.SeedSampleUsers();
    repo.SeedSampleNotifications();
}

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ phải gọi UseAuthentication trước UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

// Routing mặc định: vào trang Account/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();