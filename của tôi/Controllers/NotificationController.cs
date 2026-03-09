using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // ✅ cần thêm để dùng [Authorize]
using qlgiaidau.Models;
using qlgiaidau.Data;

public class NotificationController : Controller
{
    private readonly AppDbContext _context;

    public NotificationController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Viewer và Admin đều xem được danh sách thông báo
    [Authorize(Roles = "Viewer,Admin")]
    public IActionResult Index()
    {
        var notifications = _context.Notifications.ToList();
        return View(notifications);
    }

    // ✅ Chỉ Admin mới được tạo thông báo
    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Notification notification)
    {
        if (ModelState.IsValid)
        {
            _context.Add(notification);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(notification);
    }

    // ✅ Chỉ Admin mới được sửa thông báo
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var notification = _context.Notifications.Find(id);
        if (notification == null) return NotFound();
        return View(notification);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(Notification notification)
    {
        if (ModelState.IsValid)
        {
            _context.Update(notification);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(notification);
    }

    // ✅ Chỉ Admin mới được xóa thông báo
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var notification = _context.Notifications.Find(id);
        if (notification == null) return NotFound();
        return View(notification);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var notification = _context.Notifications.Find(id);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    // ✅ Viewer và Admin đều xem được chi tiết thông báo
    [Authorize(Roles = "Viewer,Admin")]
    public IActionResult Details(int id)
    {
        var notification = _context.Notifications.Find(id);
        if (notification == null) return NotFound();
        return View(notification);
    }
}