using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;

namespace SportsTournamentManager.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponsorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sponsors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sponsors.ToListAsync());
        }

        // GET: Sponsors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sponsors/Create
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

        // GET: Sponsors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null) return NotFound();
            return View(sponsor);
        }

        // POST: Sponsors/Edit/5
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

        // GET: Sponsors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sponsor = await _context.Sponsors.FirstOrDefaultAsync(m => m.Id == id);
            if (sponsor == null) return NotFound();
            return View(sponsor);
        }

        // POST: Sponsors/Delete/5
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
        // GET: Sponsors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sponsor = await _context.Sponsors.FirstOrDefaultAsync(m => m.Id == id);
            if (sponsor == null) return NotFound();
            return View(sponsor);
        }
    }
}