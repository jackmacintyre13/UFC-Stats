using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System;
using System.Linq;
using System.Collections.Generic;
using UfcStatsWeb.Models;
using UfcStatsWeb.Services;

namespace UfcStatsWeb.Pages.Fighters
{
    public class DetailsModel : PageModel
    {
        private readonly IUfcFightersService _fighters;
        private readonly IFightsDataService _fightsSvc;

        public DetailsModel(IUfcFightersService fighters, IFightsDataService fightsSvc)
        {
            _fighters = fighters;
            _fightsSvc = fightsSvc;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public UfcFighter Fighter { get; set; }

        public IReadOnlyList<FightRecord> Fights { get; private set; } = Array.Empty<FightRecord>();

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(Id)) { Fighter = null; Fights = Array.Empty<FightRecord>(); return; }
            var decoded = WebUtility.UrlDecode(Id);
            Fighter = _fighters.GetById(decoded) ?? _fighters.GetByName(decoded);

            // Load fight history for the selected fighter (match by name, case-insensitive)
            var allFights = _fightsSvc.GetAllFights() ?? Array.Empty<FightRecord>();
            if (!string.IsNullOrWhiteSpace(decoded))
            {
                var lower = decoded.Trim().ToLowerInvariant();
                Fights = allFights.Where(f =>
                    (f.RedFighterName ?? string.Empty).ToLowerInvariant().Contains(lower) ||
                    (f.BlueFighterName ?? string.Empty).ToLowerInvariant().Contains(lower)
                ).OrderByDescending(f => f.EventDate ?? DateTime.MinValue).ToList();
            }
            else
            {
                Fights = Array.Empty<FightRecord>();
            }
        }
    }
}