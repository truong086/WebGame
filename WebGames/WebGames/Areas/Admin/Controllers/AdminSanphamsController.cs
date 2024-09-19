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
    public class AdminSanphamsController : Controller
    {
        private readonly webGameContext _context;
        public INotyfService _notyfService { get; }

        public AdminSanphamsController(webGameContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminSanphams
        public async Task<IActionResult> Index(string name, int? to, int? from, int page = 1)
        {
            //return _context.Sanphams != null ? 
            //            View(await _context.Sanphams.ToListAsync()) :
            //            Problem("Entity set 'webGameContext.Sanphams'  is null.");

            var pageSize = 5;
            page = page < 1 ? 1 : page;
            var lsDonhang = _context.Sanphams.ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(name))
            {
                // Cách 1: Chuyển từ chuỗi sang số nguyên sử dụng "int.Parse()"
                //int so = int.Parse(name); // Chuyển từ chuỗi sang số nguyên

                // Cách 2: Chuyển từ chuỗi sang số nguyên sử dụng "Convert.ToInt32()"
                //int so1 = Convert.ToInt32(name);

                if (to != null && from != null)
                {
                    lsDonhang = lsDonhang.Where(x => x.Ten.Contains(name) && x.Solanclick >= to && x.Solanclick <= from).ToPagedList(page, pageSize);
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
                    lsDonhang = lsDonhang.Where(x => x.Solanclick >= to && x.Solanclick <= from).ToPagedList(page, pageSize);
                }
            }
            return View(lsDonhang);
        }

        // GET: Admin/AdminSanphams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sanphams == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // GET: Admin/AdminSanphams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminSanphams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Noidung,Hinhanh,Solanclick,Gia,TheloaiId,Ngaytao")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanpham);
                await _context.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(sanpham);
        }

        // GET: Admin/AdminSanphams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sanphams == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            return View(sanpham);
        }

        // POST: Admin/AdminSanphams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,Noidung,Hinhanh,Solanclick,Gia,TheloaiId,Ngaytao")] Sanpham sanpham)
        {
            if (id != sanpham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanpham);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Edit thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.Id))
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
            return View(sanpham);
        }

        // GET: Admin/AdminSanphams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sanphams == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // POST: Admin/AdminSanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sanphams == null)
            {
                return Problem("Entity set 'webGameContext.Sanphams'  is null.");
            }
            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham != null)
            {
                _context.Sanphams.Remove(sanpham);
                _notyfService.Success("Xóa thành công");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanphamExists(int id)
        {
          return (_context.Sanphams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
