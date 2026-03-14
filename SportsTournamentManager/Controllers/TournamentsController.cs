using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;

namespace SportsTournamentManager.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TournamentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tournaments
        public async Task<IActionResult> Index()
        {
            var tournaments = _context.Tournaments
                .Include(t => t.Discipline)
                .Include(t => t.Venue);
            return View(await tournaments.ToListAsync());
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            ViewData["Type"] = new SelectList(Enum.GetValues(typeof(TournamentType)));
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name");
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name");
            return View();
        }

        // POST: Tournaments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournament);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Giải {tournament.Name} đã được tạo!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["Type"] = new SelectList(Enum.GetValues(typeof(TournamentType)), tournament.Type);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name", tournament.DisciplineId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name", tournament.VenueId);
            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null) return NotFound();

            ViewData["Type"] = new SelectList(Enum.GetValues(typeof(TournamentType)), tournament.Type);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name", tournament.DisciplineId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name", tournament.VenueId);
            return View(tournament);
        }

        // POST: Tournaments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tournament tournament)
        {
            if (id != tournament.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournament);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"Giải {tournament.Name} đã được cập nhật!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tournaments.Any(e => e.Id == tournament.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Type"] = new SelectList(Enum.GetValues(typeof(TournamentType)), tournament.Type);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name", tournament.DisciplineId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name", tournament.VenueId);
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Discipline)
                .Include(t => t.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null) return NotFound();
            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament != null)
            {
                _context.Tournaments.Remove(tournament);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Giải {tournament.Name} đã được xóa!";
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Discipline)
                .Include(t => t.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tournament == null) return NotFound();
            return View(tournament);
        }
    }
}