using System;
using System.Globalization;
using System.Collections.Generic;

namespace UfcStatsWeb.Models
{
    public class FightRecord
    {
        public string RedFighterName { get; set; }
        public string RedFighterNickname { get; set; }
        public string RedFighterResult { get; set; }

        public string BlueFighterName { get; set; }
        public string BlueFighterNickname { get; set; }
        public string BlueFighterResult { get; set; }

        public string Method { get; set; }
        public string BoutType { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime? EventDate { get; set; }

        // optional fields sometimes present in CSVs
        public string Bonus { get; set; }
        public string Notes { get; set; }

        private static int? ParseInt(string s)
        {
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v)) return v;
            return null;
        }

        private static double? ParseDouble(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            s = s.Replace("%", "", StringComparison.Ordinal).Trim();
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var v)) return v;
            return null;
        }

        private static DateTime? ParseDate(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dt)) return dt;
            if (DateTime.TryParseExact(s, new[] { "yyyy-MM-dd", "MM/dd/yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) return dt;
            return null;
        }

        // build from a case-insensitive dictionary (service provides StringComparer.OrdinalIgnoreCase)
        public static FightRecord FromDictionary(IDictionary<string, string> d)
        {
            string g(string key) => d.TryGetValue(key, out var v) ? v : null;

            return new FightRecord
            {
                RedFighterName = g("red_fighter_name"),
                BlueFighterName = g("blue_fighter_name"),
                EventDate = ParseDate(g("event_date")),
                RedFighterNickname = g("red_fighter_nickname"),
                BlueFighterNickname = g("blue_fighter_nickname"),
                RedFighterResult = g("red_fighter_result"),
                BlueFighterResult = g("blue_fighter_result"),
                Method = g("method"),
                BoutType = g("bout_type"),
                Bonus = g("bonus"),
                EventName = g("event_name"),
                EventLocation = g("event_location")
            };
        }
    }
}