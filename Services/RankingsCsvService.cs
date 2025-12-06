using System.Globalization;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
 public class RankingsCsvService : IRankingsDataService
 {
 private readonly IEnumerable<Ranking> _rankings;

 public RankingsCsvService()
 {
 // read the full rankings CSV
 var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "ufc_rankings_full.csv");
 if (!File.Exists(path))
 {
 _rankings = Array.Empty<Ranking>();
 return;
 }

 var config = new CsvConfiguration(CultureInfo.InvariantCulture)
 {
 PrepareHeaderForMatch = args =>
 (args.Header)?.Trim().ToLowerInvariant(),
 MissingFieldFound = null
 };

 using var reader = new StreamReader(path);
 using var csv = new CsvReader(reader, config);
 var records = csv.GetRecords<Ranking>().ToList();
 _rankings = records;
 }

 public IEnumerable<Ranking> GetRankings() => _rankings;
 }
}
