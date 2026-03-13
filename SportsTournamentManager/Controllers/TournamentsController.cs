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
            var tournaments = await _context.Tournaments
                .Include(t => t.Discipline)
                .Include(t => t.Venue)
                .ToListAsync();
            return View(tournaments);
        }

        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Discipline)
                .Include(t => t.Venue)
                .Include(t => t.Matches)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null) return NotFound();
            return View(tournament);
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
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
                _context.Tournaments.Add(tournament);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Giải đấu {tournament.Name} đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.TournamentSponsors)
                .ThenInclude(ts => ts.Sponsor)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null) return NotFound();

            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Name", tournament.DisciplineId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Name", tournament.VenueId);

            // Load tất cả sponsor
            var sponsors = await _context.Sponsors.ToListAsync();
            ViewBag.Sponsors = sponsors.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
                Selected = tournament.TournamentSponsors.Any(ts => ts.SponsorId == s.Id)
            }).ToList();

            return View(tournament);
        }

        // POST: Tournaments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tournament tournament, int[] selectedSponsors)
        {
            if (id != tournament.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = await _context.Tournaments
                    .Include(t => t.TournamentSponsors)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (existing == null) return NotFound();

                existing.Name = tournament.Name;
                existing.Type = tournament.Type;
                existing.StartDate = tournament.StartDate;
                existing.EndDate = tournament.EndDate;
                existing.DisciplineId = tournament.DisciplineId;
                existing.VenueId = tournament.VenueId;

                // Cập nhật sponsor
                existing.TournamentSponsors.Clear();
                foreach (var sponsorId in selectedSponsors)
                {
                    existing.TournamentSponsors.Add(new TournamentSponsor
                    {
                        TournamentId = existing.Id,
                        SponsorId = sponsorId
                    });
                }

                await _context.SaveChangesAsync();
                TempData["Message"] = $"Giải đấu {tournament.Name} đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Discipline)
                .Include(t => t.Venue)
                .FirstOrDefaultAsync(t => t.Id == id);

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
                TempData["Message"] = $"Giải đấu {tournament.Name} đã được xóa thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}