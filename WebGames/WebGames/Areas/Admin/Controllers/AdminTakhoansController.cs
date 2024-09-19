using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    public class AdminTakhoansController : Controller
    {
        private readonly webGameContext _context;
        public INotyfService _notyfService { get; }

        public AdminTakhoansController(webGameContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
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

        // GET: Admin/AdminTakhoans
        public async Task<IActionResult> Index(string name, int? to, int? from, int page = 1)
        {
            //return _context.Takhoans != null ? 
            //            View(await _context.Takhoans.ToListAsync()) :
            //            Problem("Entity set 'webGameContext.Takhoans'  is null.");

            var pageSize = 5;
            page = page < 1 ? 1 : page;
            var lsDonhang = _context.Takhoans.ToPagedList(page, pageSize);
            if (!string.IsNullOrEmpty(name))
            {
                // Cách 1: Chuyển từ chuỗi sang số nguyên sử dụng "int.Parse()"
                //int so = int.Parse(name); // Chuyển từ chuỗi sang số nguyên

                // Cách 2: Chuyển từ chuỗi sang số nguyên sử dụng "Convert.ToInt32()"
                //int so1 = Convert.ToInt32(name);

                if (to != null && from != null)
                {
                    lsDonhang = lsDonhang.Where(x => x.Tentaikhoan.Contains(name) && x.Phanquyen >= to && x.Phanquyen <= from).ToPagedList(page, pageSize);
                }
                else
                {
                    lsDonhang = lsDonhang.Where(x => x.Tentaikhoan.Contains(name)).ToPagedList(page, pageSize);
                }
            }
            else
            {
                if (to != null && from != null)
                {
                    lsDonhang = lsDonhang.Where(x => x.Phanquyen >= to && x.Phanquyen <= from).ToPagedList(page, pageSize);
                }
            }
            return View(lsDonhang);

        }

        // GET: Admin/AdminTakhoans/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Takhoans == null)
            {
                return NotFound();
            }

            var takhoan = await _context.Takhoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (takhoan == null)
            {
                return NotFound();
            }

            return View(takhoan);
        }

        // GET: Admin/AdminTakhoans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTakhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tentaikhoan,Matkhau,Fullname,Email,Phanquyen")] Takhoan takhoan)
        {
            if (ModelState.IsValid)
            {
                takhoan.Matkhau = CalculateMD5Hash(takhoan.Matkhau);
                _context.Add(takhoan);
                await _context.SaveChangesAsync();
                _notyfService.Success("Add thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(takhoan);
        }

        // GET: Admin/AdminTakhoans/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Takhoans == null)
            {
                return NotFound();
            }

            var takhoan = await _context.Takhoans.FirstOrDefaultAsync(x => x.Id == id);
            if (takhoan == null)
            {
                return NotFound();
            }
            return View(takhoan);
        }

        // POST: Admin/AdminTakhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tentaikhoan,Matkhau,Fullname,Email,Phanquyen")] Takhoan takhoan)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var checkTk = _context.Takhoans.Where(x => x.Id == id).FirstOrDefault();
                    string password = CalculateMD5Hash(takhoan.Matkhau);
                    if(checkTk.Matkhau !=  password) {
                        return NotFound();
                    }
                    checkTk.Matkhau = password;
                    checkTk.Tentaikhoan = takhoan.Tentaikhoan;
                    checkTk.Fullname = takhoan.Fullname;
                    checkTk.Email = takhoan.Email;
                    checkTk.Phanquyen = takhoan.Phanquyen;

                    _context.Takhoans.Update(checkTk);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Update thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TakhoanExists(takhoan.Tentaikhoan))
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

            return View(takhoan);
        }

        // GET: Admin/AdminTakhoans/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Takhoans == null)
            {
                return NotFound();
            }

            var takhoan = await _context.Takhoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (takhoan == null)
            {
                return NotFound();
            }

            return View(takhoan);
        }

        // POST: Admin/AdminTakhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Takhoans == null)
            {
                return Problem("Entity set 'webGameContext.Takhoans'  is null.");
            }
            var takhoan = await _context.Takhoans.FindAsync(id);
            if (takhoan != null)
            {
                _context.Takhoans.Remove(takhoan);
                _notyfService.Success("Delete thành công");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TakhoanExists(string id)
        {
          return (_context.Takhoans?.Any(e => e.Tentaikhoan == id)).GetValueOrDefault();
        }
    }
}
