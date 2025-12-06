using Microsoft.AspNetCore.Mvc.RazorPages;
using UfcStatsWeb.Models;
using UfcStatsWeb.Services;

namespace UfcStatsWeb.Pages.Rankings
{
    public class IndexModel : PageModel
    {
        private readonly IRankingsDataService _rankingsService;
        public IEnumerable<Ranking>? Rankings { get; private set; }

        public IndexModel(IRankingsDataService rankingsService)
        {
            _rankingsService = rankingsService;
        }

        public void OnGet()
        {
            Rankings = _rankingsService.GetRankings();
        }
    }
}
