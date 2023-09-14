using LeagueSite.Data;
using LeagueSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeagueSite.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public List<Player> Players { get; set; }
        public SelectList Teams { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; } = "Name";
        [BindProperty(SupportsGet = true)]
        public string SelectedTeam { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Players = await _context.Players
                .OrderBy(p => p.Position)
                .ToListAsync();

            if (!string.IsNullOrEmpty(SearchString))
            {
                Players = Players
                    .Where(c => c.Name.ToLower().Contains(SearchString.ToLower()))
                    .ToList();
            }

            switch (SortField)
            {
                case "TeamId":
                    Players = Players.OrderBy(p => p.TeamId).ToList();
                    break;
                case "Name":
                    Players = Players.OrderBy(p => p.Name).ToList();
                    break;
                case "Position":
                    Players = Players.OrderBy(c => c.Position).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(SelectedTeam))
            {
                Players = Players.Where(c => c.TeamId == SelectedTeam).ToList();
            }

            Teams = new SelectList(await _context.Teams
                .OrderBy(t => t.TeamId)
                .Select(t => t.TeamId)
                .ToListAsync());

            return Page();
        }
    }
}
