using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using WebGames.AddCart;
using WebGames.Entity;
using WebGames.Models;
using WebGames.ModelView;

namespace WebGames.Controllers
{
    [Authorize(Roles = "1, 2")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly webGameContext _context;

        public HomeController(ILogger<HomeController> logger, webGameContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string? name = null)
        {
            //// Gọi lại lớp "HomeVM" để sử dụng các thuộc tính trong đó
            //HomeVM home = new HomeVM();

            //// Câu lệnh này là lấy ra toàn bộ dữ liệu trong bảng "tintuc" với điều kiện(where) là "Tieude" phải khác null và sắp xếp theo kiểu giảm dần theo ngày tháng
            //var lsTinTuc = _context.Tintucs.AsNoTracking().Where(x => x.Tieude != null).Take(3).ToList();

            //// Câu lệnh này là lấy ra toàn bộ dữ liệu trong bảng "loaitintuc"
            //var lsLoaiTinTuc = _context.Loaitins.AsNoTracking().OrderByDescending(x => x.Ngaytao).ToList();

            //var tintucTop1 = _context.Tintucs.AsNoTracking().OrderByDescending(x => x.Id).Take(3).ToList();
            //List<TinTucHomeVM> tintucs = new List<TinTucHomeVM>();

            //var danhsachTinTuc = _context.Tintucs.AsNoTracking().OrderByDescending(x => x.Ngaytao).Take(10).ToList();
            //foreach (var item in lsLoaiTinTuc)
            //{
            //    TinTucHomeVM tintuc = new TinTucHomeVM();
            //    tintuc.tintucs = lsTinTuc.Where(x => x.LoaitinId == item.Id).ToList(); // Lấy ra những bản ghi được liên kết đến bảng "loaitintuc" bằng mệnh đề "where()"
            //    tintuc.loaitin = item;
            //    tintucs.Add(tintuc); // Add tất cả dữ liệu vào List"tintucs" vừa được khai báo ở trên
            //}

            //home.tintucList = tintucs;
            //ViewBag.AllTinTuc = lsTinTuc;
            //ViewBag.tintuctop1 = tintucTop1;
            //ViewBag.lienquan = lsTinTuc;
            //ViewBag.loaitin = lsLoaiTinTuc;
            //ViewBag.danhsachTinTuc = danhsachTinTuc;

            //return View(home);

            GameHomeVM home = new GameHomeVM();

            var lsGame = _context.Sanphams.AsNoTracking().Where(x => x.Ten != null).ToList();
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToLower();
				lsGame = lsGame.Where(x => x.Ten.ToLower().Contains(name)).ToList();
			}
                
            var lsTheLoai = _context.Theloais.AsNoTracking().ToList();
            var lsTop1 = _context.Sanphams.AsNoTracking().OrderByDescending(x => x.Id).Take(1).SingleOrDefault();
            var lsTop5 = _context.Sanphams.AsNoTracking().OrderByDescending(x => x.Id).Take(5).ToList();
            List<GameHome> games = new List<GameHome>();
            foreach (var item in lsTheLoai)
            {
                GameHome gamess = new GameHome();
                gamess.sanphams = lsGame.Where(x => x.TheloaiId == item.Id).ToList();
                gamess.theloais = item;
                games.Add(gamess);

            }

            home.lsDanhSach = games;
            ViewBag.game = lsGame;
            ViewBag.theloai = lsTheLoai;
            ViewBag.top1 = lsTop1;
            ViewBag.top5 = lsTop5;
            return View(home);
        }

        public IActionResult details(int id)
        {
            var findOne = _context.Sanphams.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            if (findOne != null)
            {
                findOne = new Sanpham
                {
                    Id = id,
                    Ten = findOne.Ten,
                    Noidung = findOne.Noidung,
                    Solanclick = findOne.Solanclick + 1,
                    Gia = findOne.Gia,
                    Ngaytao = findOne.Ngaytao,
                    TheloaiId = findOne.TheloaiId,
                    Hinhanh = findOne.Hinhanh
                };
                var lienquan = _context.Sanphams.AsNoTracking().Where(x => x.TheloaiId == findOne.TheloaiId).ToList();
                ViewBag.lienquans = lienquan;
                _context.Update(findOne);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return View(findOne);
        }

        public List<Cart> listCart()
        {
            var cart = HttpContext.Session.Get<List<Cart>>("giohang");
            if (cart == null)
            {
                cart = new List<Cart>();
            }
            return cart;
        }
        public IActionResult AddToCart(int id)
        {
            var lsCart = listCart();
            var myCart = lsCart.SingleOrDefault(x => x.Mahh == id);
            if (myCart == null)
            {
                var sanphams = _context.Sanphams.SingleOrDefault(x => x.Id == id);
                myCart = new Cart
                {
                    Mahh = id,
                    tieude = sanphams.Ten,
                    hinhanh = sanphams.Hinhanh,
                    gia = sanphams.Gia.Value,
                    soluong = 1
                };
                lsCart.Add(myCart);
            }
            else
            {
                myCart.soluong += 1;
            }
            HttpContext.Session.Set("giohang", lsCart);
            return RedirectToAction("Index");
        }

        public IActionResult thanhtoan()
        {
            //Đầu tiên, cần lấy cookie của người dùng bằng cách sử dụng đối tượng HttpContext như sau:
            //var cookie = HttpContext.Request.Cookies[CookieAuthenticationDefaults.AuthenticationScheme];

            //// Tiếp theo, bạn cần giải mã cookie thành đối tượng ClaimsPrincipal để lấy thông tin người dùng bằng cách sử dụng phương thức AuthenticationHttpContextExtensions.AuthenticateAsync của đối tượng HttpContext như sau:
            //var giaima = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //var principal = HttpContext.User as ClaimsPrincipal;
            //var ids = principal.Identity.Name;
            //int id1 = int.Parse(ids.ToString());
            
            //string userId = HttpContext.User.FindFirstValue(ClaimTypes.Role); // Lấy ra quyền của người dùng
            string userId = HttpContext.User.FindFirstValue("UserId");
            if(userId != null)
            {
                // Lấy thông tin trong giỏ hàng
                var lsCart = HttpContext.Session.Get<List<Cart>>("giohang");
                Order oders = new Order();
                // Gán dữ liệu cho bảng "Order"
                oders.Ten = "Don hang - " + DateTime.Now.ToString("yyyyMMddHHmmss");
                oders.Statuc = 1;
                oders.UsersId = int.Parse(userId);
                oders.Ngaytao = DateTime.Now;
                _context.Orders.Add(oders);
                // Lưu thông tin vào bảng "Order"
                _context.SaveChanges();

                // Lấy ra "id" của bảng "Order" vừa mới tạo để lưu vào bảng "OrderDetails"
                var order_id = oders.Id;

                List<OrderDetai> lsOrderDetail = new List<OrderDetai>();
                foreach (var item in lsCart)
                {
                    OrderDetai orderdetail = new OrderDetai();
                    orderdetail.Soluong = item.soluong;
                    orderdetail.OrderId = order_id;
                    orderdetail.Tongtien = (int)item.tonggia; // Ép kiểu sang "int". Vì "item.tonggia" là kiểu float còn "orderdetail.Tongtien" là kiểu int, nên phải ép kiểu từ float sang int để lưu vào database
                    orderdetail.SanphamId = item.Mahh;
                    lsOrderDetail.Add(orderdetail); // Lưu dữ liệu vừa thêm vào List(lsOrderDetail)
                }

                _context.OrderDetais.AddRange(lsOrderDetail);
                _context.SaveChanges();
                return View();
            }
            return RedirectToAction("Login", "Login");
        }
        public IActionResult Cart()
        {
            return View(listCart());
        }

        [Route("/loaigame/{Id}", Name = "TheLoai")]
        public IActionResult loaigame(int id)
        {
            try
            {
                var theloai = _context.Theloais.AsNoTracking().SingleOrDefault(x => x.Id == id);
                var game = _context.Sanphams.AsNoTracking().Where(x => x.TheloaiId == theloai.Id).OrderByDescending(x => x.Ngaytao);
                List<Sanpham> list = new List<Sanpham>(game); // Chuyền danh sách dữ liệu vừa đc lấy ra lưu vào List
                ViewBag.theloai = theloai;
                return View(list); // Đưa dữ liệu từ List ra trang View()
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}