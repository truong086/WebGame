using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using System.Security.Claims;
using WebGames.Models;
using X.PagedList;

namespace WebGames.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PhieuNhapController : Controller
    {
        private readonly webGameContext _webGameContextl;
        public INotyfService _notyfService { get; }
        public PhieuNhapController(webGameContext webGameContextl, INotyfService notyfService)
        {
            this._webGameContextl = webGameContextl;
            this._notyfService = notyfService;
        }
        public async Task<IActionResult> Index(string? tungay, string? denngay, int page = 1, int pageSize = 20)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 20 : pageSize;

            var list = _webGameContextl.Phieunhaps.ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(tungay) && !string.IsNullOrEmpty(denngay))
                list = list.Where(x => x.Ngaynhap >= DateTime.Parse(tungay) && x.Ngaynhap <= DateTime.Parse(denngay)).ToPagedList(page, pageSize);

            if(!string.IsNullOrEmpty(tungay) && !string.IsNullOrEmpty(denngay))
                list = list.Where(x => x.Ngaynhap >= DateTime.Parse(tungay)).ToPagedList(page, pageSize);

            if (!string.IsNullOrEmpty(denngay) && !string.IsNullOrEmpty(tungay))
                list = list.Where(x => x.Ngaynhap <= DateTime.Parse(denngay)).ToPagedList(page, pageSize);


            return View(list);
        }

        public async Task<IActionResult> RouterAdd()
        {
            ViewData["Noinhap"] = new SelectList(_webGameContextl.Noinhaps, "Id", "Ten");
            ViewData["Sanpham"] = new SelectList(_webGameContextl.Sanphams, "Id", "Ten");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Phieunhap cty)
        {
            if (ModelState.IsValid)
            {
                var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity != null)
                {
                    var NameUser = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                    if(NameUser != null)
                    {
                        cty.NguoinhapId = int.Parse(NameUser.Value);
                    }
                }
                _webGameContextl.Phieunhaps.Add(cty);
                await _webGameContextl.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));

            }
            return View(cty);
        }

        public async Task<IActionResult> RouterEdit(int id)
        {
            var checkId = _webGameContextl.Phieunhaps.FirstOrDefault(x => x.Id == id);

            if(checkId == null)
            {
                return NotFound();
            }

            ViewData["Noinhan"] = new SelectList(_webGameContextl.Noinhaps, "Id", "Ten", checkId.NoinhapId);
            ViewData["Sanpham"] = new SelectList(_webGameContextl.Sanphams, "Id", "Ten", checkId.SanphamId);
            return View(checkId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Phieunhap cty)
        {
            try
            {
                var checkId = _webGameContextl.Phieunhaps.Where(x => x.Id == id).FirstOrDefault();
                if (checkId == null)
                {
                    _notyfService.Error("Bản ghi không tồn tại");
                    return RedirectToAction(nameof(Index));
                }
                if (ModelState.IsValid)
                {
                    var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
                    if (claimIdentity != null)
                    {
                        var NameUser = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                        if (NameUser != null)
                        {
                            checkId.NguoinhapId = int.Parse(NameUser.Value);
                        }
                    }
                    checkId.SanphamId = cty.SanphamId;
                    checkId.Soluong = cty.Soluong;
                    checkId.NoinhapId = cty.NoinhapId;
                    checkId.NguoinhapId = cty.NguoinhapId;
                    checkId.Tongtien = cty.Tongtien;
                    checkId.Ngaynhap = cty.Ngaynhap;
                    _webGameContextl.Update(checkId);
                    await _webGameContextl.SaveChangesAsync();
                    _notyfService.Success("Edit thành công");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(cty);
        }
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var checkId = _webGameContextl.Phieunhaps.SingleOrDefault(x => x.Id == id);
                if (checkId != null)
                {
                    _webGameContextl.Phieunhaps.Remove(checkId);
                    await _webGameContextl.SaveChangesAsync();
                    _notyfService.Success("Delete thành công");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return Problem("Entity set 'webGameContext.Theloais'  is null.");
            }
            return View(id);
        }
    }
}
