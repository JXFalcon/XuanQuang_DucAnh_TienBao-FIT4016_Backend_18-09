using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // ✅ cần thêm để dùng [Authorize]
using qlgiaidau.Data;
using qlgiaidau.Models;
using System.Linq;

namespace qlgiaidau.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Viewer và Admin đều xem được danh sách đội bóng
        [Authorize(Roles = "Viewer,Admin")]
        public IActionResult Index()
        {
            var teams = _context.Teams.ToList();
            return View(teams);
        }

        // ✅ Chỉ Admin mới được thêm đội bóng
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Teams.Add(team);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // ✅ Chỉ Admin mới được sửa đội bóng
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Teams.Update(team);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // ✅ Chỉ Admin mới được xóa đội bóng
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // ✅ Viewer và Admin đều xem được chi tiết đội bóng
        [Authorize(Roles = "Viewer,Admin")]
        public IActionResult Details(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null) return NotFound();
            return View(team);
        }
    }
}