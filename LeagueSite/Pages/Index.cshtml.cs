using LeagueSite.Data;
using LeagueSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LeagueSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly LeagueContext _context;

        public IndexModel(ILogger<IndexModel> logger, LeagueContext context)
        {
            _logger = logger;
            _context = context;
        }

        public League League { get; set; }

        public async Task OnGetAsync()
        {
            League = await _context.Leagues.FirstOrDefaultAsync();
        }
    }
}