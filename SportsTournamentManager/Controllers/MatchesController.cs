using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;

namespace SportsTournamentManager.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var matches = await _context.Matches
                .Include(m => m.Tournament)
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .ToListAsync();
            return View(matches);
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var match = await _context.Matches
                .Include(m => m.Tournament)
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return NotFound();
            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name");
            ViewData["TeamAId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewData["TeamBId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Matches.Add(match);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Trận đấu đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();

            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name", match.TournamentId);
            ViewData["TeamAId"] = new SelectList(_context.Teams, "Id", "Name", match.TeamAId);
            ViewData["TeamBId"] = new SelectList(_context.Teams, "Id", "Name", match.TeamBId);
            return View(match);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Match match)
        {
            if (id != match.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingMatch = await _context.Matches.FindAsync(id);
                if (existingMatch == null) return NotFound();

                existingMatch.Date = match.Date;
                existingMatch.TournamentId = match.TournamentId;
                existingMatch.TeamAId = match.TeamAId;
                existingMatch.TeamBId = match.TeamBId;
                existingMatch.ScoreA = match.ScoreA;
                existingMatch.ScoreB = match.ScoreB;

                await _context.SaveChangesAsync();
                TempData["Message"] = "Trận đấu đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var match = await _context.Matches
                .Include(m => m.Tournament)
                .Include(m => m.TeamA)
                .Include(m => m.TeamB)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return NotFound();
            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Trận đấu đã được xóa thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}