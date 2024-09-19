using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WebGames.Models;

namespace WebGames.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ThongKeController : Controller
    {
        private readonly webGameContext _webGameContextl;
        public INotyfService _notyfService { get; }
        public ThongKeController(webGameContext webGameContextl, INotyfService notyfService)
        {
            this._webGameContextl = webGameContextl;
            this._notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var sanphamBanHot = _webGameContextl.Phieuxuats.GroupBy(x => x.SanphamId).OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();

            var sanphamBanHots = _webGameContextl.Phieuxuats.GroupBy(x => x.SanphamId).OrderByDescending(g => g.Count())
                .Select(g => new
                {
                    a = g.FirstOrDefault().Sanpham.Ten,
                })
                .ToList();

            var sanpham = _webGameContextl.Sanphams.Where(x => sanphamBanHot.Contains(x.Id)).ToList();

            var totalXuat = _webGameContextl.Phieuxuats.Sum(x => x.Tongtien);

            var totalNhap = _webGameContextl.Phieunhaps.Sum(x => x.Tongtien);

            var nhanvienbannhieunhat = _webGameContextl.Phieuxuats.GroupBy(x => x.NguoicapId).OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();

            var nhanvien = _webGameContextl.Takhoans.Where(x => nhanvienbannhieunhat.Contains(x.Id)).ToList();

            var congtycungcapnhieunhat = _webGameContextl.Phieuxuats.GroupBy(x => x.CongtycungcapId).OrderByDescending(a => a.Count())
                .Select(d => d.Key)
                .ToList();

            var congty = _webGameContextl.Congtycungcaps.Where(x => congtycungcapnhieunhat.Contains(x.Id)).ToList();

          


            ViewData["congtycungcapnhieunhat"] = congty;
            ViewData["nhanvienbannhieunhat"] = nhanvien;
            ViewData["totalNhap"] = totalNhap;
            ViewData["totalXuat"] = totalXuat;
            ViewBag.sanphamBanHot = sanphamBanHots.Select(x => x.a).ToList();

            return View();
        }

        public async Task<IActionResult> excel()
        {
            var obj = new List<object>();
            var sanphamBanHot = _webGameContextl.Phieuxuats.GroupBy(x => x.SanphamId).OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();

            var sanphamBanHots = _webGameContextl.Phieuxuats.GroupBy(x => x.SanphamId).OrderByDescending(g => g.Count())
                .Select(g => new
                {
                    a = g.FirstOrDefault().Sanpham.Ten,
                })
                .ToList();


            var sanpham = _webGameContextl.Sanphams.Where(x => sanphamBanHot.Contains(x.Id)).ToList();

            var totalXuat = _webGameContextl.Phieuxuats.Sum(x => x.Tongtien);

            var totalNhap = _webGameContextl.Phieunhaps.Sum(x => x.Tongtien);

            var nhanvienbannhieunhat = _webGameContextl.Phieuxuats.GroupBy(x => x.NguoicapId).OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToList();

            var nhanvien = _webGameContextl.Takhoans.Where(x => nhanvienbannhieunhat.Contains(x.Id)).ToList();

            var congtycungcapnhieunhat = _webGameContextl.Phieuxuats.GroupBy(x => x.CongtycungcapId).OrderByDescending(a => a.Count())
                .Select(d => d.Key)
                .ToList();

            var congty = _webGameContextl.Congtycungcaps.Where(x => congtycungcapnhieunhat.Contains(x.Id)).ToList();


            using (var package = new ExcelPackage())
            {
                
                var worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells.LoadFromCollection(nhanvien, true);
                var test2 = package.Workbook.Worksheets.Add("Products1");
                test2.Cells.LoadFromCollection(congty, true);
                var test3 = package.Workbook.Worksheets.Add("Products2");
                test3.Cells.LoadFromCollection(sanpham, true);

                // Thiết lập tên các cột trong Excel (nếu cần)
                worksheet.Cells["A1"].Value = "Tên nhân viên";
                worksheet.Cells["B1"].Value = "Product Name";
                worksheet.Cells["C15"].Value = "Tổng tiền nhập: " + totalNhap;
                worksheet.Cells["C16"].Value = "Tổng tiền nhập: " + totalXuat;



                // Lưu file Excel
                byte[] fileBytes = package.GetAsByteArray();
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
            }
        }
    }
}
