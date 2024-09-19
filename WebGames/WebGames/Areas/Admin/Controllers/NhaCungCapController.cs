using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGames.Models;
using X.PagedList;

namespace WebGames.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NhaCungCapController : Controller
    {
        private readonly webGameContext webGameContext;
        public INotyfService _notyfService { get; }
        public NhaCungCapController(webGameContext _webGameContext, INotyfService notyfService)
        {
            this.webGameContext = _webGameContext;
            _notyfService = notyfService;
        }
        public async Task<IActionResult> Index(string? name, int page = 1, int pageSize = 20)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 20 : pageSize;

            var list = webGameContext.Congtycungcaps.ToPagedList(page, pageSize);

            if (!string.IsNullOrWhiteSpace(name))
                list = list.Where(x => !string.IsNullOrEmpty(x.Ten) ? x.Ten.Contains(name) : x.Ten == "a").ToPagedList(page, pageSize);
            
            return View(list);
        }

        public async Task<IActionResult> RouterAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Congtycungcap cty)
        {
            if(ModelState.IsValid)
            {
                webGameContext.Congtycungcaps.Add(cty);
                await webGameContext.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));

            }
            return View(cty);
        }

        public async Task<IActionResult> RouterEdit(int id)
        {
            var checkId = webGameContext.Congtycungcaps.FirstOrDefault(x => x.Id == id);

            return View(checkId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Congtycungcap cty)
        {
            try
            {
                var checkId = webGameContext.Congtycungcaps.Where(x => x.Id == id).FirstOrDefault();
                if(checkId == null) {
                    _notyfService.Error("Bản ghi không tồn tại");
                    return RedirectToAction(nameof(Index));
                }
                if (ModelState.IsValid)
                {
                    checkId.Ngaytao = cty.Ngaytao;
                    checkId.Ten = cty.Ten;
                    checkId.Email = cty.Email;
                    checkId.Diachi = cty.Diachi;
                    
                    webGameContext.Congtycungcaps.Update(checkId);
                    await webGameContext.SaveChangesAsync();
                    _notyfService.Success("Edit thành công");
                    return RedirectToAction(nameof(Index));
                }
            }catch(Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(cty);
        }
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var checkId = webGameContext.Congtycungcaps.SingleOrDefault(x => x.Id == id);
                if(checkId  != null)
                {
                    webGameContext.Congtycungcaps.Remove(checkId);
                    await webGameContext.SaveChangesAsync();
                    _notyfService.Success("Delete thành công");
                    return RedirectToAction(nameof(Index));
                }
            }catch(Exception ex)
            {
                return Problem("Entity set 'webGameContext.Theloais'  is null.");
            }
            return View(id);
        }

    }
}
