using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // ✅ cần thêm để dùng [Authorize]
using Microsoft.EntityFrameworkCore;
using qlgiaidau.Models;
using qlgiaidau.Data;

public class MatchController : Controller
{
    private readonly AppDbContext _context;

    public MatchController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Viewer và Admin đều xem được danh sách trận đấu
    [Authorize(Roles = "Viewer,Admin")]
    public IActionResult Index()
    {
        var matches = _context.Matches
            .Include(m => m.TeamA)
            .Include(m => m.TeamB)
            .ToList();
        return View(matches);
    }

    // ✅ Chỉ Admin mới được tạo trận đấu
    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Match match)
    {
        if (ModelState.IsValid)
        {
            _context.Add(match);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(match);
    }

    // ✅ Chỉ Admin mới được sửa trận đấu
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var match = _context.Matches.Find(id);
        if (match == null) return NotFound();
        return View(match);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(Match match)
    {
        if (ModelState.IsValid)
        {
            _context.Update(match);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(match);
    }

    // ✅ Chỉ Admin mới được xóa trận đấu
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var match = _context.Matches
            .Include(m => m.TeamA)
            .Include(m => m.TeamB)
            .FirstOrDefault(m => m.Id == id);
        if (match == null) return NotFound();
        return View(match);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var match = _context.Matches.Find(id);
        if (match != null)
        {
            _context.Matches.Remove(match);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    // ✅ Viewer và Admin đều xem được chi tiết trận đấu
    [Authorize(Roles = "Viewer,Admin")]
    public IActionResult Details(int id)
    {
        var match = _context.Matches
            .Include(m => m.TeamA)
            .Include(m => m.TeamB)
            .FirstOrDefault(m => m.Id == id);
        if (match == null) return NotFound();
        return View(match);
    }
}