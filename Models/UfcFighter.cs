using System;

namespace UfcStatsWeb.Models
{
 public class UfcFighter
 {
 public string Name { get; set; }
 public string Nickname { get; set; }
 public int? Wins { get; set; }
 public int? Losses { get; set; }
 public int? Draws { get; set; }
 public int? HeightCm { get; set; }
 public double? WeightInKg { get; set; }
 public int? ReachInCm { get; set; }
 public string Stance { get; set; }
 public DateTime? DateOfBirth { get; set; }

 // striking / grappling stats
 public double? SignificantStrikesLandedPerMinute { get; set; }
 public double? SignificantStrikingAccuracy { get; set; }
 public double? SignificantStrikesAbsorbedPerMinute { get; set; }
 public double? SignificantStrikeDefence { get; set; }
 public double? AverageTakedownsLandedPer15Minutes { get; set; }
 public double? TakedownAccuracy { get; set; }
 public double? TakedownDefense { get; set; }
 public double? AverageSubmissionsAttemptedPer15Minutes { get; set; }
 }
}