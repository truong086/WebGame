using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebGames.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Gọi lại chuỗi kết nối đến database
var stringConnectdb = builder.Configuration.GetConnectionString("ketnoi");
builder.Services.AddDbContext<webGameContext>(options => options.UseSqlServer(stringConnectdb)); // "btlNetCoreContext" là lớp "btlNetCoreContext.cs" trong phần model
// Cấu hình xác thực người dùng bằng cookie
//builder.Services.Configure<CookiePolicyOptions> (options =>
//{
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    // Chỉ ra đường dẫn cấm truy cập cho dù đã đăng nhập
    //options.AccessDeniedPath = new PathString("/Manager/Account");
    //options.Cookie = new CookieBuilder
    //{
    //    HttpOnly = true,
    //    Name = "abc", // Đặt tên
    //    Path= "/", // Đây là chỉ ra đường dẫn mặc định
    //    SameSite = SameSiteMode.Lax,
    //    SecurePolicy = CookieSecurePolicy.SameAsRequest
    //};
    // Chỉ ra đường dẫn login
    options.LoginPath = new PathString("/Login/Login");
    options.AccessDeniedPath = "/Login/Login"; // cấu hình đường dẫn tới trang AccessDenied
    // Câu lệnh này là khi người dùng cố tình nhảy vào 1 tài nguyên của Admin thì sẽ hệ thống sẽ tự nhảy đến đường dẫn là "/Home/Login"
    // Tham số "redirect" này khi người dùng đăng nhập thành công thì sẽ đưa đến đường dẫn này
    options.ReturnUrlParameter = "url";
    // Thiết lập thời gian sống của Cookie
    options.SlidingExpiration = true;

    // Đây là phân quyền trong file "program"
    // Phân quyền người dung
    //builder.Services.AddAuthorization(options =>
    //{
    //    // Chỉ định người dùng có quyền là "Admin" thì mới vào được controller là "RequireAdminRole"
    //    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    //});
});

// Đọa code này nghĩa là: Cho trang web hiểu những từ tiếng Việt có dấu và chèn thêm tiếng Việt vào trong trang web
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
builder.Services.AddNotyf(config => {
    config.DurationInSeconds = 10;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});
// Khai báo "Session" để thực hiện chức năng giỏ hàng
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
// Sử dụng policy cookie
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
