using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportsTournamentManager.Controllers
{
    [Authorize] // yêu cầu đăng nhập cho toàn bộ controller
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TeamsController(ApplicationDbContext context) => _context = context;

        // GET: Teams (Viewer và Admin đều xem được)
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .ToListAsync();
            return View(teams);
        }

        // GET: Teams/Details/5 (Viewer và Admin đều xem được)
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<IActionResult> Details(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return NotFound();
            return View(team);
        }

        // GET: Teams/Create (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                var newTeam = new Team
                {
                    Name = team.Name,
                    Country = team.Country
                };

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

        // GET: Teams/Edit/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Coach)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return NotFound();
            return View(team);
        }

        // POST: Teams/Edit/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
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

                existingTeam.Name = team.Name;
                existingTeam.Country = team.Country;

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

        // GET: Teams/Delete/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return NotFound();
            return View(team);
        }

        // POST: Teams/Delete/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
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