using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;

namespace SportsTournamentManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var players = await _context.Players
                .Include(p => p.Team)
                .ToListAsync();
            return View(players);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null) return NotFound();
            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Players.Add(player);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Cầu thủ {player.Name} đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();

            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Player player)
        {
            if (id != player.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingPlayer = await _context.Players.FindAsync(id);
                if (existingPlayer == null) return NotFound();

                existingPlayer.Name = player.Name;
                existingPlayer.Age = player.Age;
                existingPlayer.TeamId = player.TeamId;

                await _context.SaveChangesAsync();
                TempData["Message"] = $"Cầu thủ {player.Name} đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Cầu thủ {player.Name} đã được xóa thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}