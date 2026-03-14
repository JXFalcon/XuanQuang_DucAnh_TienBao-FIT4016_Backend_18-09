using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportsTournamentManager.Controllers
{
    [Authorize] // yêu cầu đăng nhập cho toàn bộ controller
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coaches (Viewer và Admin đều xem được)
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var coaches = await _context.Coaches.Include(c => c.Team).ToListAsync();
            return View(coaches);
        }

        // GET: Coaches/Details/5 (Viewer và Admin đều xem được)
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var coach = await _context.Coaches
                .Include(c => c.Team)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coach == null) return NotFound();
            return View(coach);
        }

        // GET: Coaches/Create (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Coaches/Create (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coach coach)
        {
            if (ModelState.IsValid)
            {
                bool teamHasCoach = await _context.Coaches.AnyAsync(c => c.TeamId == coach.TeamId);
                if (teamHasCoach)
                {
                    ModelState.AddModelError("TeamId", "Đội này đã có HLV, không thể thêm HLV khác.");
                    ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", coach.TeamId);
                    return View(coach);
                }

                _context.Coaches.Add(coach);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"HLV {coach.Name} đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(coach);
        }

        // GET: Coaches/Edit/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null) return NotFound();

            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", coach.TeamId);
            return View(coach);
        }

        // POST: Coaches/Edit/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Coach coach)
        {
            if (id != coach.Id) return NotFound();

            if (ModelState.IsValid)
            {
                bool teamHasOtherCoach = await _context.Coaches
                    .AnyAsync(c => c.TeamId == coach.TeamId && c.Id != coach.Id);

                if (teamHasOtherCoach)
                {
                    ModelState.AddModelError("TeamId", "Đội này đã có HLV khác, không thể gán thêm.");
                    ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", coach.TeamId);
                    return View(coach);
                }

                var existingCoach = await _context.Coaches.FindAsync(id);
                if (existingCoach == null) return NotFound();

                existingCoach.Name = coach.Name;
                existingCoach.Nationality = coach.Nationality;
                existingCoach.TeamId = coach.TeamId;

                await _context.SaveChangesAsync();
                TempData["Message"] = $"HLV {coach.Name} đã được cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var coach = await _context.Coaches
                .Include(c => c.Team)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coach == null) return NotFound();
            return View(coach);
        }

        // POST: Coaches/Delete/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach != null)
            {
                _context.Coaches.Remove(coach);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"HLV {coach.Name} đã được xóa thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}