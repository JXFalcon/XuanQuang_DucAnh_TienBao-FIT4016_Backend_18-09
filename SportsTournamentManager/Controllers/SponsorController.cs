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
            var sponsors = await _context.Sponsors.ToListAsync();
            return View(sponsors);
        }

        // GET: Sponsors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sponsor = await _context.Sponsors
                .Include(s => s.TournamentSponsors)
                .ThenInclude(ts => ts.Tournament)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sponsor == null) return NotFound();
            return View(sponsor);
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
                _context.Sponsors.Add(sponsor);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Nhà tài trợ {sponsor.Name} đã được tạo thành công!";
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
                var existing = await _context.Sponsors.FindAsync(id);
                if (existing == null) return NotFound();

                existing.Name = sponsor.Name;
                existing.Industry = sponsor.Industry;

                await _context.SaveChangesAsync();
                TempData["Message"] = $"Nhà tài trợ {sponsor.Name} đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(sponsor);
        }

        // GET: Sponsors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sponsor = await _context.Sponsors.FindAsync(id);
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
                TempData["Message"] = $"Nhà tài trợ {sponsor.Name} đã được xóa thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}