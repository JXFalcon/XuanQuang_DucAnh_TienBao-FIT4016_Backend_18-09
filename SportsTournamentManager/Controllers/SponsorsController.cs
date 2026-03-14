using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportsTournamentManager.Controllers
{
    [Authorize] // yêu cầu đăng nhập cho toàn bộ controller
    public class SponsorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponsorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sponsors (Viewer và Admin đều xem được)
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sponsors.ToListAsync());
        }

        // GET: Sponsors/Details/5 (Viewer và Admin đều xem được)
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<IActionResult> Details(int id)
        {
            var sponsor = await _context.Sponsors.FirstOrDefaultAsync(m => m.Id == id);
            if (sponsor == null) return NotFound();
            return View(sponsor);
        }

        // GET: Sponsors/Create (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sponsors/Create (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsor);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Nhà tài trợ {sponsor.Name} đã được tạo!";
                return RedirectToAction(nameof(Index));
            }
            return View(sponsor);
        }

        // GET: Sponsors/Edit/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null) return NotFound();
            return View(sponsor);
        }

        // POST: Sponsors/Edit/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sponsor sponsor)
        {
            if (id != sponsor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsor);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"Nhà tài trợ {sponsor.Name} đã được cập nhật!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Sponsors.Any(e => e.Id == sponsor.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sponsor);
        }

        // GET: Sponsors/Delete/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var sponsor = await _context.Sponsors.FirstOrDefaultAsync(m => m.Id == id);
            if (sponsor == null) return NotFound();
            return View(sponsor);
        }

        // POST: Sponsors/Delete/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor != null)
            {
                _context.Sponsors.Remove(sponsor);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Nhà tài trợ {sponsor.Name} đã được xóa!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}