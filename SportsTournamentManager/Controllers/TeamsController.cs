using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;

namespace SportsTournamentManager.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TeamsController(ApplicationDbContext context) => _context = context;

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .ToListAsync();
            return View(teams);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return NotFound();
            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                // Tạo Team mới
                var newTeam = new Team
                {
                    Name = team.Name,
                    Country = team.Country
                };

                // Nếu có nhập Coach thì tạo Coach gắn với Team
                if (team.Coach != null)
                {
                    newTeam.Coach = new Coach
                    {
                        Name = team.Coach.Name,
                        Nationality = team.Coach.Nationality
                    };
                }

                _context.Teams.Add(newTeam);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Tạo đội thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Coach) // load Coach nếu có
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return NotFound();
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingTeam = await _context.Teams
                    .Include(t => t.Coach)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (existingTeam == null) return NotFound();

                // Cập nhật Team
                existingTeam.Name = team.Name;
                existingTeam.Country = team.Country;

                // Nếu có Coach thì cập nhật Coach
                if (existingTeam.Coach != null && team.Coach != null)
                {
                    existingTeam.Coach.Name = team.Coach.Name;
                    existingTeam.Coach.Nationality = team.Coach.Nationality;
                }

                await _context.SaveChangesAsync();
                TempData["Message"] = "Cập nhật đội thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Đội {team.Name} đã được xóa thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
