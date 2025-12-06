using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
    // Loads and parses a CSV placed at wwwroot/data/fights.csv (loaded once).
    public class FightsCsvService : IFightsDataService
    {
        private readonly IReadOnlyList<FightRecord> _fights;
        private readonly ILogger<FightsCsvService> _logger;

        public FightsCsvService(IHostEnvironment env, ILogger<FightsCsvService> logger)
        {
            _logger = logger;
            try
            {
                var path = Path.Combine(env.ContentRootPath, "wwwroot", "data", "fights.csv");
                if (!File.Exists(path))
                {
                    _logger.LogWarning("Fights CSV not found at {Path}", path);
                    _fights = Array.Empty<FightRecord>();
                    return;
                }

                var lines = File.ReadAllLines(path, Encoding.UTF8);
                _fights = ParseCsv(lines).ToList().AsReadOnly();
                _logger.LogInformation("Loaded {Count} records from fights CSV", _fights.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load fights CSV");
                _fights = Array.Empty<FightRecord>();
            }
        }

        public IReadOnlyList<FightRecord> GetAllFights() => _fights;

        private static IEnumerable<FightRecord> ParseCsv(string[] lines)
        {
            if (lines == null || lines.Length == 0) yield break;

            int idx = 0;
            while (idx < lines.Length && string.IsNullOrWhiteSpace(lines[idx])) idx++;
            if (idx >= lines.Length) yield break;

            var headerFields = SplitCsvLine(lines[idx]).Select(h => h.Trim()).ToArray();
            idx++;

            for (; idx < lines.Length; idx++)
            {
                var line = lines[idx];
                if (string.IsNullOrWhiteSpace(line)) continue;
                var fields = SplitCsvLine(line);

                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headerFields.Length; i++)
                {
                    var key = headerFields[i];
                    var val = i < fields.Length ? fields[i] : string.Empty;
                    dict[key] = val;
                }

                yield return FightRecord.FromDictionary(dict);
            }
        }

        // Basic CSV splitter (handles quoted fields and escaped quotes).
        private static string[] SplitCsvLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return Array.Empty<string>();
            var sb = new StringBuilder();
            var list = new List<string>();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"'); // escaped quote
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                    continue;
                }

                if (c == ',' && !inQuotes)
                {
                    list.Add(sb.ToString());
                    sb.Clear();
                    continue;
                }

                sb.Append(c);
            }

            list.Add(sb.ToString());
            return list.ToArray();
        }
    }
}