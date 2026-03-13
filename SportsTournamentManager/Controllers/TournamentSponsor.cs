using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Data;

namespace SportsTournamentManager.Controllers
{
    public class TournamentSponsorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TournamentSponsorsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var tournamentSponsors = await _context.TournamentSponsors
                .Include(ts => ts.Tournament)
                .Include(ts => ts.Sponsor)
                .ToListAsync();
            return View(tournamentSponsors);
        }
    }
}