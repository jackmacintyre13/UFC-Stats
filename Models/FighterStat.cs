
using System;

namespace UfcStatsWeb.Models
{
    public class FighterStat
    {
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public int? Draws { get; set; }
        public decimal? HeightCm { get; set; }
        public decimal? WeightInKg { get; set; }
        public decimal? ReachInCm { get; set; }
        public string? Stance { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public decimal? SignificantStrikesLandedPerMinute { get; set; }
        public decimal? SignificantStrikingAccuracyPct { get; set; }          // stored as 0–100
        public decimal? SignificantStrikesAbsorbedPerMinute { get; set; }
        public decimal? SignificantStrikeDefencePct { get; set; }              // 0–100
        public decimal? AverageTakedownsLandedPer15Minutes { get; set; }
        public decimal? TakedownAccuracyPct { get; set; }                      // 0–100
        public decimal? TakedownDefensePct { get; set; }                       // 0–100
        public decimal? AverageSubmissionsAttemptedPer15Minutes { get; set; }
    }
}
