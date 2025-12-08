using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
 public class UfcFightersCsvService : IUfcFightersService
 {
 private readonly List<UfcFighter> _fighters = new();
 public UfcFightersCsvService()
 {
 var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "ufc_fighters.csv");
 if (!File.Exists(file)) return;
 var lines = File.ReadAllLines(file);
 if (lines.Length <=1) return;
 string[] headers = lines[0].Contains('\t') ? lines[0].Split('\t').Select(h => h.Trim()).ToArray() : lines[0].Split(',').Select(h => h.Trim().Trim('"')).ToArray();
 for (int i=1;i<lines.Length;i++)
 {
 var cols = lines[i].Contains('\t') ? lines[i].Split('\t') : SplitCsvLine(lines[i]);
 if (cols.Length==0) continue;
 var map = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
 for (int c=0;c<cols.Length && c<headers.Length;c++) map[headers[c]] = cols[c].Trim('"');
 var f = new UfcFighter();
 f.Name = TryGet(map, "name") ?? TryGet(map, "fighter_name") ?? TryGet(map, "full_name");
 f.Nickname = TryGet(map, "nickname") ?? TryGet(map, "alias");
 f.Wins = ParseInt(TryGet(map, "wins"));
 f.Losses = ParseInt(TryGet(map, "losses"));
 f.Draws = ParseInt(TryGet(map, "draws"));
 f.HeightCm = ParseInt(TryGet(map, "height_cm") ?? TryGet(map, "height"));
 f.WeightInKg = ParseDouble(TryGet(map, "weight_in_kg") ?? TryGet(map, "weight"));
 f.ReachInCm = ParseInt(TryGet(map, "reach_in_cm") ?? TryGet(map, "reach"));
 f.Stance = TryGet(map, "stance");
 f.DateOfBirth = ParseDate(TryGet(map, "date_of_birth") ?? TryGet(map, "dob") ?? TryGet(map, "birth_date"));
 f.SignificantStrikesLandedPerMinute = ParseDouble(TryGet(map, "significant_strikes_landed_per_minute"));
 f.SignificantStrikingAccuracy = ParseDouble(TryGet(map, "significant_striking_accuracy"));
 f.SignificantStrikesAbsorbedPerMinute = ParseDouble(TryGet(map, "significant_strikes_absorbed_per_minute"));
 f.SignificantStrikeDefence = ParseDouble(TryGet(map, "significant_strike_defence"));
 f.AverageTakedownsLandedPer15Minutes = ParseDouble(TryGet(map, "average_takedowns_landed_per_15_minutes"));
 f.TakedownAccuracy = ParseDouble(TryGet(map, "takedown_accuracy"));
 f.TakedownDefense = ParseDouble(TryGet(map, "takedown_defense"));
 f.AverageSubmissionsAttemptedPer15Minutes = ParseDouble(TryGet(map, "average_submissions_attempted_per_15_minutes"));
 _fighters.Add(f);
 }
 }

 private static string[] SplitCsvLine(string line)
 {
 var cols = new List<string>();
 var cur = string.Empty;
 var inQuotes = false;
 for (int i=0;i<line.Length;i++)
 {
 var ch = line[i];
 if (ch == '"') { inQuotes = !inQuotes; continue; }
 if (ch == ',' && !inQuotes) { cols.Add(cur); cur = string.Empty; continue; }
 cur += ch;
 }
 cols.Add(cur);
 return cols.ToArray();
 }

 private static double? ParseDouble(string s)
 {
 if (string.IsNullOrWhiteSpace(s)) return null;
 if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d)) return d;
 if (double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out d)) return d;
 return null;
 }

 private static int? ParseInt(string s)
 {
 if (string.IsNullOrWhiteSpace(s)) return null;
 if (int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var v)) return v;
 return null;
 }

 private static DateTime? ParseDate(string s)
 {
 if (string.IsNullOrWhiteSpace(s)) return null;
 if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var dt)) return dt;
 if (DateTime.TryParse(s, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out dt)) return dt;
 return null;
 }

 private static string TryGet(Dictionary<string,string> map, string key)
 {
 if (map.TryGetValue(key, out var v)) return v;
 var alt = map.Keys.FirstOrDefault(k => k.Equals(key, StringComparison.OrdinalIgnoreCase) || k.Replace(" ", "_").Equals(key, StringComparison.OrdinalIgnoreCase));
 if (alt != null) return map[alt];
 return null;
 }

 public IEnumerable<UfcFighter> GetAll() => _fighters;
 public UfcFighter GetById(string id) => _fighters.FirstOrDefault(f => string.Equals(f.Name, id, StringComparison.OrdinalIgnoreCase));
 public UfcFighter GetByName(string name) => _fighters.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase) || (f.Name ?? string.Empty).ToLowerInvariant().Contains(name.ToLowerInvariant()));
 }
}