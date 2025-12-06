namespace UfcStatsWeb.Models
{
 public class Ranking
 {
 public string Division { get; set; } = string.Empty;
 public int Rank { get; set; }
 public string Name { get; set; } = string.Empty;
 public string Profile_Link { get; set; } = string.Empty; // matches CSV header `profile_link`
 }
}
