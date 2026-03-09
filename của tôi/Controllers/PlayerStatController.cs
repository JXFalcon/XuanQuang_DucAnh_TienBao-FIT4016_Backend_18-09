using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // ✅ cần thêm để dùng [Authorize]
using qlgiaidau.Models;
using qlgiaidau.Data;
using Microsoft.EntityFrameworkCore;

public class PlayerStatController : Controller
{
    private readonly AppDbContext _context;

    public PlayerStatController(AppDbContext context)
    {
        _context = context; 
    }

    // ✅ Viewer, Admin và Coach đều xem được danh sách thống kê
    [Authorize(Roles = "Viewer,Admin,Coach")]
    public async Task<IActionResult> Index()
    {
        var stats = await _context.PlayerStat
            .Include(p => p.Team)
            .ToListAsync();

        return View(stats);
    }

    // ✅ Chỉ Admin và Coach mới được thêm thống kê cầu thủ
    [Authorize(Roles = "Admin,Coach")]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Coach")]
    public IActionResult Create(PlayerStat stat)
    {
        if (ModelState.IsValid)
        {
            _context.Add(stat);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(stat);
    }

    // ✅ Chỉ Admin mới được sửa thống kê
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var stat = _context.PlayerStat.Find(id);
        if (stat == null) return NotFound();
        return View(stat);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(PlayerStat stat)
    {
        if (ModelState.IsValid)
        {
            _context.Update(stat);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(stat);
    }

    // ✅ Chỉ Admin mới được xóa thống kê
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var stat = _context.PlayerStat.Find(id);
        if (stat == null) return NotFound();
        return View(stat);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var stat = _context.PlayerStat.Find(id);
        if (stat != null)
        {
            _context.PlayerStat.Remove(stat);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}