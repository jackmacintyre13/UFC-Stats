using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
    // Loads and parses a TSV (tab-separated values) file placed at wwwroot/data/ufc_fights.csv (loaded once).
    public class FightsCsvService : IFightsDataService
    {
        private readonly List<FightRecord> _fights = new List<FightRecord>();

        public FightsCsvService()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "ufc_fights.csv");
            if (!File.Exists(path)) return;
            var lines = File.ReadAllLines(path);
            if (lines.Length <= 1) return;
            string[] headers = lines[0].Contains('\t') ? lines[0].Split('\t').Select(h => h.Trim()).ToArray() : lines[0].Split(',').Select(h => h.Trim().Trim('"')).ToArray();
            for (int i = 1; i < lines.Length; i++)
            {
                var row = lines[i];
                string[] cols = row.Contains('\t') ? row.Split('\t') : SplitCsvLine(row);
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int c = 0; c < cols.Length && c < headers.Length; c++) dict[headers[c]] = cols[c].Trim('"');
                try
                {
                    var fr = FightRecord.FromDictionary(dict);
                    _fights.Add(fr);
                }
                catch
                {
                    // ignore malformed lines
                }
            }
        }

        // CSV splitting that respects quoted commas
        private static string[] SplitCsvLine(string line)
        {
            var cols = new List<string>();
            var cur = string.Empty;
            var inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                var ch = line[i];
                if (ch == '"') { inQuotes = !inQuotes; continue; }
                if (ch == ',' && !inQuotes) { cols.Add(cur); cur = string.Empty; continue; }
                cur += ch;
            }
            cols.Add(cur);
            return cols.ToArray();
        }

        public IEnumerable<FightRecord> GetAll() => _fights;

        public IEnumerable<FightRecord> GetAllFights() => _fights;

        public IEnumerable<FightRecord> GetByFighterName(string name) => _fights.Where(f => (f.RedFighterName ?? string.Empty).IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0 || (f.BlueFighterName ?? string.Empty).IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
    }
}