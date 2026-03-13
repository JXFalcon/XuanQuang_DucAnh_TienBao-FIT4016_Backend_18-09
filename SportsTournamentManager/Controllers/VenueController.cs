using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;

namespace SportsTournamentManager.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues.ToListAsync();
            return View(venues);
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.Tournaments)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venue == null) return NotFound();
            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Venues.Add(venue);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Địa điểm {venue.Name} đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();
            return View(venue);
        }

        // POST: Venues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            if (id != venue.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = await _context.Venues.FindAsync(id);
                if (existing == null) return NotFound();

                existing.Name = venue.Name;
                existing.Location = venue.Location;
                existing.Capacity = venue.Capacity;

                await _context.SaveChangesAsync();
                TempData["Message"] = $"Địa điểm {venue.Name} đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();
            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Địa điểm {venue.Name} đã được xóa thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}