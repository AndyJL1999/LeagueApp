using LeagueSite.Data;
using LeagueSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LeagueSite.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public List<Conference> Conferences { get; set; }
        public List<Division> Divisions { get; set; }
        public List<Team> Teams { get; set; }

        public async Task<IActionResult> OnGetAsync(string? conferenceId)
        {
            Conferences = await _context.Conferences.ToListAsync();

            if (string.IsNullOrEmpty(conferenceId))
            {
                Divisions = await _context.Divisions.Where(d => d.ConferenceId == "AFC").ToListAsync();
            }
            else
            {
                Divisions = await _context.Divisions.Where(d => d.ConferenceId == conferenceId).ToListAsync();
            }
            
            Teams = await _context.Teams
                .OrderByDescending(t => t.Win)
                .ToListAsync();

            return Page();
        }
    }
}
