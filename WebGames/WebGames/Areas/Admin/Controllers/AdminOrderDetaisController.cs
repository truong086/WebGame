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
    public class AdminOrderDetaisController : Controller
    {
        private readonly webGameContext _context;
        public INotyfService _notyfService { get; }

        public AdminOrderDetaisController(webGameContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminOrderDetais
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Index(string name, int? to, int? from, int page = 1)
        {
            //var webGameContext = _context.OrderDetais.Include(o => o.Order).Include(o => o.Sanpham);
            //return View(await webGameContext.ToListAsync());
            var pageSize = 1;
            page = page < 1 ? 1 : page;
            var lsDonhang = _context.OrderDetais.ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(name))
            {
                // Cách 1: Chuyển từ chuỗi sang số nguyên sử dụng "int.Parse()"
                int so = int.Parse(name); // Chuyển từ chuỗi sang số nguyên

                // Cách 2: Chuyển từ chuỗi sang số nguyên sử dụng "Convert.ToInt32()"
                //int so1 = Convert.ToInt32(name);

                if (to != null && from != null)
                {
                    lsDonhang = lsDonhang.Where(x => x.Id == so && x.SanphamId >= to && x.SanphamId <= from).ToPagedList(page, pageSize);
                }
                else
                {
                    lsDonhang = lsDonhang.Where(x => x.Id == so).ToPagedList(page, pageSize);
                }
            }
            else
            {
                if(to != null && from != null)
                {
                    lsDonhang = lsDonhang.Where(x => x.SanphamId >= to && x.SanphamId <= from).ToPagedList(page, pageSize);
                }
            }
            return View(lsDonhang);
        }

        // GET: Admin/AdminOrderDetais/Details/5
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderDetais == null)
            {
                return NotFound();
            }

            var orderDetai = await _context.OrderDetais
                .Include(o => o.Order)
                .Include(o => o.Sanpham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetai == null)
            {
                return NotFound();
            }

            return View(orderDetai);
        }

        // GET: Admin/AdminOrderDetais/Create
        [Authorize(Roles = "1")]
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["SanphamId"] = new SelectList(_context.Sanphams, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminOrderDetais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Create([Bind("Id,OrderId,SanphamId,Soluong")] OrderDetai orderDetai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetai);
                await _context.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderDetai.OrderId);
            ViewData["SanphamId"] = new SelectList(_context.Sanphams, "Id", "Id", orderDetai.SanphamId);
            return View(orderDetai);
        }

        // GET: Admin/AdminOrderDetais/Edit/5
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderDetais == null)
            {
                return NotFound();
            }

            var orderDetai = await _context.OrderDetais.FindAsync(id);
            if (orderDetai == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderDetai.OrderId);
            ViewData["SanphamId"] = new SelectList(_context.Sanphams, "Id", "Id", orderDetai.SanphamId);
            return View(orderDetai);
        }

        // POST: Admin/AdminOrderDetais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,SanphamId,Soluong")] OrderDetai orderDetai)
        {
            if (id != orderDetai.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetai);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Edit thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetaiExists(orderDetai.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderDetai.OrderId);
            ViewData["SanphamId"] = new SelectList(_context.Sanphams, "Id", "Id", orderDetai.SanphamId);
            return View(orderDetai);
        }

        // GET: Admin/AdminOrderDetais/Delete/5
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderDetais == null)
            {
                return NotFound();
            }

            var orderDetai = await _context.OrderDetais
                .Include(o => o.Order)
                .Include(o => o.Sanpham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetai == null)
            {
                return NotFound();
            }

            return View(orderDetai);
        }

        // POST: Admin/AdminOrderDetais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderDetais == null)
            {
                return Problem("Entity set 'webGameContext.OrderDetais'  is null.");
            }
            var orderDetai = await _context.OrderDetais.FindAsync(id);
            if (orderDetai != null)
            {
                _context.OrderDetais.Remove(orderDetai);
                _notyfService.Success("Xóa thành công");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetaiExists(int id)
        {
          return (_context.OrderDetais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
