using Microsoft.AspNetCore.Mvc;
using qlgiaidau.Models;
using qlgiaidau.Data;

public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Users
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    // GET: Users/Create
    public IActionResult Create() => View();

    // POST: Users/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(UserRole user)
    {
        if (ModelState.IsValid)
        {
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: Users/Edit/5
    public IActionResult Edit(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();
        return View(user);
    }

    // POST: Users/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: Users/Delete/5
    public IActionResult Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return NotFound();
        return View(user);
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}