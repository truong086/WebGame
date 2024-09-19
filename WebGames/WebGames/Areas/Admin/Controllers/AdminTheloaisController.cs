using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebGames.Models;
using X.PagedList;

namespace WebGame.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminTheloaisController : Controller
    {
        private readonly webGameContext _context;
        public INotyfService _notyfService { get; }

        public AdminTheloaisController(webGameContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminTheloais
        public async Task<IActionResult> Index(string name, int? to, int? from, int page = 1)
        {
            //return _context.Theloais != null ? 
            //            View(await _context.Theloais.ToListAsync()) :
            //            Problem("Entity set 'webGameContext.Theloais'  is null.");
            var pageSize = 5;
            page = page < 1 ? 1 : page;
            var lsDonhang = _context.Theloais.ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(name))
            {
                // Cách 1: Chuyển từ chuỗi sang số nguyên sử dụng "int.Parse()"
                //int so = int.Parse(name); // Chuyển từ chuỗi sang số nguyên

                // Cách 2: Chuyển từ chuỗi sang số nguyên sử dụng "Convert.ToInt32()"
                //int so1 = Convert.ToInt32(name);

                if (to != null && from != null)
                {
                    lsDonhang = lsDonhang.Where(x => x.Ten.Contains(name) && x.Id >= to && x.Id <= from).ToPagedList(page, pageSize);
                }
                else
                {
                    lsDonhang = lsDonhang.Where(x => x.Ten.Contains(name)).ToPagedList(page, pageSize);
                }
            }
            else
            {
                if (to != null && from != null)
                {
                    lsDonhang = lsDonhang.Where(x => x.Id >= to && x.Id <= from).ToPagedList(page, pageSize);
                }
            }
            return View(lsDonhang);
        }

        // GET: Admin/AdminTheloais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Theloais == null)
            {
                return NotFound();
            }

            var theloai = await _context.Theloais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theloai == null)
            {
                return NotFound();
            }

            return View(theloai);
        }

        // GET: Admin/AdminTheloais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTheloais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Ngaytao")] Theloai theloai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theloai);
                await _context.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(theloai);
        }

        // GET: Admin/AdminTheloais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Theloais == null)
            {
                return NotFound();
            }

            var theloai = await _context.Theloais.FindAsync(id);
            if (theloai == null)
            {
                return NotFound();
            }
            return View(theloai);
        }

        // POST: Admin/AdminTheloais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,Ngaytao")] Theloai theloai)
        {
            if (id != theloai.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theloai);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Edit thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheloaiExists(theloai.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(theloai);
        }

        // GET: Admin/AdminTheloais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Theloais == null)
            {
                return NotFound();
            }

            var theloai = await _context.Theloais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theloai == null)
            {
                return NotFound();
            }

            return View(theloai);
        }

        // POST: Admin/AdminTheloais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Theloais == null)
            {
                return Problem("Entity set 'webGameContext.Theloais'  is null.");
            }
            var theloai = await _context.Theloais.FindAsync(id);
            if (theloai != null)
            {
                _context.Theloais.Remove(theloai);
                _notyfService.Success("Xóa thành công");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheloaiExists(int id)
        {
          return (_context.Theloais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
