using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebGames.Models;
using X.PagedList;

namespace WebGames.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NoiNhapController : Controller
    {
        private readonly webGameContext webGameContext;
        public INotyfService _notyfService { get; }
        public NoiNhapController(webGameContext _webGameContext, INotyfService notyfService)
        {
            this.webGameContext = _webGameContext;
            _notyfService = notyfService;
        }
        public IActionResult Index(string? name, int page = 1, int  pageSize = 20)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 20 ? 20 : pageSize;

            var list = webGameContext.Noinhaps.ToPagedList(page, pageSize);
            if(!string.IsNullOrEmpty(name))
                list = list.Where(x => x.Ten.Contains(name)).ToPagedList(page, pageSize);


            return View(list);
        }

        public async Task<IActionResult> RouterAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Noinhap cty)
        {
            if (ModelState.IsValid)
            {
                webGameContext.Noinhaps.Add(cty);
                await webGameContext.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));

            }
            return View(cty);
        }

        public async Task<IActionResult> RouterEdit(int id)
        {
            var checkId = webGameContext.Noinhaps.FirstOrDefault(x => x.Id == id);

            return View(checkId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Noinhap cty)
        {
            try
            {
                var checkId = webGameContext.Noinhaps.Where(x => x.Id == id).FirstOrDefault();
                if (checkId == null)
                {
                    _notyfService.Error("Bản ghi không tồn tại");
                    return RedirectToAction(nameof(Index));
                }
                if (ModelState.IsValid)
                {

                    checkId.Ten = cty.Ten;
                    checkId.Ngaytao = cty.Ngaytao;
                    webGameContext.Update(checkId);
                    await webGameContext.SaveChangesAsync();
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
                var checkId = webGameContext.Noinhaps.SingleOrDefault(x => x.Id == id);
                if (checkId != null)
                {
                    webGameContext.Noinhaps.Remove(checkId);
                    await webGameContext.SaveChangesAsync();
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
