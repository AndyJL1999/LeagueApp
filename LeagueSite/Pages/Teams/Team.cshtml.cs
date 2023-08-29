using LeagueSite.Data;
using LeagueSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeagueSite.Pages.Teams
{
    public class TeamModel : PageModel
    {
        private readonly LeagueContext _context;

        public TeamModel(LeagueContext context)
        {
            _context = context;
        }

        public Team Team { get; set; }
        public string LogoUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Team = await _context.Teams.FindAsync(id);
            LogoUrl = Team.TeamId + ".png";

            return Page();
        }
    }
}
