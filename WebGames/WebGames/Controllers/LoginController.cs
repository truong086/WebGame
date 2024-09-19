using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using WebGames.Models;

namespace WebGame.Controllers
{
    // "Authorize" là 1 thuộc tính của ASP.NET CORE dùng để xác thực quyền hạn khi truy cập vào tài nguyên, khi gặp "Authorize" thì sẽ kiểm tra xem người dùng đã login hay chưa, kiểm tra bằng cách khi chúng ta login sẽ lưu trữ vào các thông số trong hệ thống
    // Phải có "[Authorize]" này thì lúc người dùng mới vào trang web thì hệ thống mới tự động nhảy vào trang "login"
    [Authorize]
    public class LoginController : Controller
    {
        private readonly webGameContext _context;
        public LoginController(webGameContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public static string CalculateMD5Hash(string input)
        {
            // Chuyển đổi chuỗi thành mảng byte
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Tạo đối tượng MD5
            using (MD5 md5 = MD5.Create())
            {
                // Mã hóa mảng byte thành mảng byte chứa mã băm MD5
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi mảng byte thành chuỗi hexa
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" để chuyển byte thành chuỗi hexa
                }

                return sb.ToString();
            }
        }

        [HttpPost]
        [AllowAnonymous] // Cái này là truy cập nặc danh, phải có "[AllowAnonymous]" này thì hệ thống mới biết là hiển thị trang web nào khi bắt đầu vào hệ thống, ở đây lúc mới bắt đầu vào trang web thì hệ thống sẽ hiển thị trang login
        public IActionResult Login(string username, string password)
        {
            password = CalculateMD5Hash(password);
            // Kiểm tra xem tài khoản người dùng có tồn tại trong database không
            var acc = _context.Takhoans.FirstOrDefault(x => x.Tentaikhoan == username && x.Matkhau == password);
            if(acc != null) // Kiểm tra nếu tài khoản tồn tại trong database
            {
                // Cách 1
                //Tạo Identity chứa tông tin người dùng vừa đăng nhập vào trong cookie
                //var identity = new ClaimsIdentity(new[]
                //{
                //     // Tạo new Claim để lưu những thông tin của người dùng mà chúng ta mong muốn
                //     // Ví dụ: Ở đây chúng ta muốn lưu tên đăng nhập của người dùng thì tạo 1 "new Claim(ClaimTypes.Name, acc.Tentaikhoan)" để lưu lại tên tài khoản
                //     // "ClaimTypes.Name" nghĩa là kiểu dữ liệu muốn lưu, và về sau muốn lấy lại, "ClaimTypes.Name" là "Name" này được dùng phổ biến nhất
                //     // "acc.Tentaikhoan" nghĩa là tên tài khoản của người dùng, và sẽ gán dữ liệu này cho "ClaimTypes.Name"
                //     new Claim(ClaimTypes.Name, acc.Tentaikhoan)
                //}, "logins"); // Đây là đặt tên
                // var principal = new ClaimsPrincipal(identity);
                // // Login, Chuyền vào 2 tham số hoặc chuyền vào 1 tham sô, "login" là tên của biểu đồ vừa đc đặt tên ở trên, "principal" là thông tin của người dùng
                // HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Cách 2
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, acc.Id.ToString()), // Chuyển "acc.Id.ToString()" sang dạng "ToString" nghĩa là dạng chuỗi thì mới lưu trữ đc Id của người dùng
                    new Claim(ClaimTypes.Role, acc.Phanquyen.ToString()), // Lưu trữ quyền của người dùng vào cookie, sử dụng "ClaimTypes.Role"
                    // Khi người dùng đăng nhập vào sẽ lấy ra "id" của người dùng, và gán lại cho biến "UserId", và khi nào muốn lấy ra "id" của người dùng từ cookie từ chỉ gần gọi lại biến "UserId"
                    // Khi nào muốn lấy ra "id" của người dùng từ cookie thì gọi lại biến "UserId" bằng cách sử dụng: "string userId = HttpContext.User.FindFirstValue("UserId");"
                    new Claim("UserId", acc.Id.ToString()), // Sử dụng "ToString" để chuyển id này về dạng chuỗi
                    new Claim(ClaimTypes.Name, acc.Fullname)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Tạo Claim Principal để lấy thông tin người dùng login vào
               var principal = new ClaimsPrincipal(identity);
                // Login, Chuyền vào 2 tham số hoặc chuyền vào 1 tham sô, "login" là tên của biểu đồ vừa đc đặt tên ở trên, "principal" là thông tin của người dùng

                /*
                 * Đoạn code này nghĩa là:HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal) trong ASP.NET Core là một phương thức mà chúng ta sử dụng để tạo một cookie xác thực và đăng nhập người dùng.
                    Cụ thể, HttpContext.SignInAsync là một phương thức mở rộng của đối tượng HttpContext, cung cấp cho chúng ta các tùy chọn để tạo và gửi cookie đến trình duyệt của người dùng.
                    Để tạo cookie, chúng ta cần truyền vào phương thức này hai tham số:
                    CookieAuthenticationDefaults.AuthenticationScheme: là scheme xác thực được sử dụng để xác định cookie.
                    principal: là đối tượng ClaimsPrincipal chứa các thông tin xác thực của người dùng, chẳng hạn như tên người dùng, quyền truy cập, v.v.
                    Phương thức HttpContext.SignInAsync sau đó sẽ sử dụng scheme xác thực và ClaimsPrincipal được cung cấp để tạo cookie và gửi cookie đến trình duyệt của người dùng.
                    Sau khi cookie được tạo thành công và gửi đến trình duyệt của người dùng, người dùng sẽ được đăng nhập vào hệ thống và được cấp phép truy cập các tài nguyên yêu cầu xác thực.
                    //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                 */

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            else // Kiểm tra nếu không tồn tại trong database
            {
                ViewBag.error = "<div class='alert alert-danger'>Đăng nhập sai</div>";
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult logout()
        {
            // Đăng xuất
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}
