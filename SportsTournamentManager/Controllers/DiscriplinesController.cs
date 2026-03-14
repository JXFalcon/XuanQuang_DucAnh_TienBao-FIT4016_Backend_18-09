using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;
using SportsTournamentManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportsTournamentManager.Controllers
{
    [Authorize] // yêu cầu đăng nhập cho toàn bộ controller
    public class DisciplinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Disciplines (Viewer và Admin đều xem được)
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disciplines.ToListAsync());
        }

        // GET: Disciplines/Details/5 (Viewer và Admin đều xem được)
        [Authorize(Roles = "Admin,Viewer")]
        public async Task<IActionResult> Details(int id)
        {
            var discipline = await _context.Disciplines
                .Include(d => d.Tournaments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (discipline == null) return NotFound();
            return View(discipline);
        }

        // GET: Disciplines/Create (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Type"] = new SelectList(Enum.GetValues(typeof(TournamentType)));
            return View();
        }

        // POST: Disciplines/Create (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discipline);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Môn {discipline.Name} đã được tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["Type"] = new SelectList(Enum.GetValues(typeof(TournamentType)), discipline.Type);
            return View(discipline);
        }

        // GET: Disciplines/Edit/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline == null) return NotFound();

            ViewData["Type"] = new SelectList(Enum.GetValues(typeof(TournamentType)), discipline.Type);
            return View(discipline);
        }

        // POST: Disciplines/Edit/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Discipline discipline)
        {
            if (id != discipline.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discipline);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"Môn {discipline.Name} đã được cập nhật!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Disciplines.Any(e => e.Id == discipline.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Type"] = new SelectList(Enum.GetValues(typeof(TournamentType)), discipline.Type);
            return View(discipline);
        }

        // GET: Disciplines/Delete/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var discipline = await _context.Disciplines.FirstOrDefaultAsync(m => m.Id == id);
            if (discipline == null) return NotFound();
            return View(discipline);
        }

        // POST: Disciplines/Delete/5 (chỉ Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline != null)
            {
                _context.Disciplines.Remove(discipline);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"Môn {discipline.Name} đã được xóa!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}