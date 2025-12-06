using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using UfcStatsWeb.Services;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Pages.Fights
{
    public class IndexModel : PageModel
    {
        private readonly IFightsDataService _svc;

        public IReadOnlyList<FightRecord> Fights { get; private set; } = Array.Empty<FightRecord>();

        [BindProperty(SupportsGet = true)]
        public string q { get; set; }

        public IndexModel(IFightsDataService svc) => _svc = svc ?? throw new ArgumentNullException(nameof(svc));

        public void OnGet()
        {
            var fights = _svc.GetAllFights() ?? Array.Empty<FightRecord>();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var lower = q.Trim().ToLowerInvariant();
                fights = fights.Where(f =>
                    (f.RedFighterName ?? string.Empty).ToLowerInvariant().Contains(lower) ||
                    (f.BlueFighterName ?? string.Empty).ToLowerInvariant().Contains(lower) ||
                    (f.EventName ?? string.Empty).ToLowerInvariant().Contains(lower) ||
                    (f.EventLocation ?? string.Empty).ToLowerInvariant().Contains(lower)
                ).ToList();
            }

            Fights = fights;
        }
    public string NormalizeName(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return s;
            return System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant());
        }
    }



}
