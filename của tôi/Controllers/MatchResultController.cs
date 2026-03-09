using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; // ✅ cần thêm để dùng [Authorize]
using qlgiaidau.Data;
using qlgiaidau.Models;

namespace qlgiaidau.Controllers
{
    public class MatchResultController : Controller
    {
        private readonly AppDbContext _context;

        public MatchResultController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Chỉ giữ lại một hàm Index, gắn [AllowAnonymous] để ai cũng xem được
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var results = await _context.MatchResults
                .Include(r => r.Match!)
                    .ThenInclude(m => m.TeamA!)
                .Include(r => r.Match!)
                    .ThenInclude(m => m.TeamB!)
                .ToListAsync();

            // Nếu không có dữ liệu, trả về danh sách rỗng để tránh null
            return View(results ?? new List<MatchResult>());
        }

        // ✅ Chỉ Admin mới được thêm kết quả
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(MatchResult result)
        {
            // lưu vào DB
            _context.MatchResults.Add(result);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ✅ Chỉ Admin mới được sửa
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var matchResult = _context.MatchResults.Find(id);
            if (matchResult == null) return NotFound();
            return View(matchResult);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(MatchResult result)
        {
            _context.MatchResults.Update(result);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ✅ Viewer và Admin đều xem được chi tiết
        [Authorize(Roles = "Viewer,Admin")]
        public IActionResult Details(int id)
        {
            var matchResult = _context.MatchResults
                .Include(r => r.Match!)
                    .ThenInclude(m => m.TeamA!)
                .Include(r => r.Match!)
                    .ThenInclude(m => m.TeamB!)
                .FirstOrDefault(r => r.Id == id);

            if (matchResult == null) return NotFound();
            return View(matchResult);
        }
    }
}