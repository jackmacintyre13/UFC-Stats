using System;
using System.Globalization;
using System.Collections.Generic;

namespace UfcStatsWeb.Models
{
    public class FightRecord
    {
        public string RedFighterName { get; set; }
        public string BlueFighterName { get; set; }
        public DateTime? EventDate { get; set; }
        public string RedFighterNickname { get; set; }
        public string BlueFighterNickname { get; set; }
        public string RedFighterResult { get; set; }
        public string BlueFighterResult { get; set; }
        public string Method { get; set; }
        public int? Round { get; set; }
        public string Time { get; set; }
        public string TimeFormat { get; set; }
        public string Referee { get; set; }
        public string Details { get; set; }
        public string BoutType { get; set; }
        public string Bonus { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }

        public int? RedFighterKD { get; set; }
        public int? BlueFighterKD { get; set; }
        public int? RedFighterSigStr { get; set; }
        public int? BlueFighterSigStr { get; set; }
        public double? RedFighterSigStrPct { get; set; }
        public double? BlueFighterSigStrPct { get; set; }
        public int? RedFighterTotalStr { get; set; }
        public int? BlueFighterTotalStr { get; set; }

        public int? RedFighterTD { get; set; }
        public int? BlueFighterTD { get; set; }
        public double? RedFighterTDPct { get; set; }
        public double? BlueFighterTDPct { get; set; }

        public int? RedFighterSubAtt { get; set; }
        public int? BlueFighterSubAtt { get; set; }

        public int? RedFighterRev { get; set; }
        public int? BlueFighterRev { get; set; }

        public string RedFighterCtrl { get; set; }
        public string BlueFighterCtrl { get; set; }

        public int? RedFighterSigStrHead { get; set; }
        public int? BlueFighterSigStrHead { get; set; }
        public int? RedFighterSigStrBody { get; set; }
        public int? BlueFighterSigStrBody { get; set; }
        public int? RedFighterSigStrLeg { get; set; }
        public int? BlueFighterSigStrLeg { get; set; }
        public int? RedFighterSigStrDistance { get; set; }
        public int? BlueFighterSigStrDistance { get; set; }
        public int? RedFighterSigStrClinch { get; set; }
        public int? BlueFighterSigStrClinch { get; set; }
        public int? RedFighterSigStrGround { get; set; }
        public int? BlueFighterSigStrGround { get; set; }

        public double? RedFighterSigStrHeadPct { get; set; }
        public double? BlueFighterSigStrHeadPct { get; set; }
        public double? RedFighterSigStrBodyPct { get; set; }
        public double? BlueFighterSigStrBodyPct { get; set; }
        public double? RedFighterSigStrLegPct { get; set; }
        public double? BlueFighterSigStrLegPct { get; set; }
        public double? RedFighterSigStrDistancePct { get; set; }
        public double? BlueFighterSigStrDistancePct { get; set; }
        public double? RedFighterSigStrClinchPct { get; set; }
        public double? BlueFighterSigStrClinchPct { get; set; }
        public double? RedFighterSigStrGroundPct { get; set; }
        public double? BlueFighterSigStrGroundPct { get; set; }

        public int? RedFighterTotalPts { get; set; }
        public int? BlueFighterTotalPts { get; set; }

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
                Round = ParseInt(g("round")),
                Time = g("time"),
                TimeFormat = g("time_format"),
                Referee = g("referee"),
                Details = g("details"),
                BoutType = g("bout_type"),
                Bonus = g("bonus"),
                EventName = g("event_name"),
                EventLocation = g("event_location"),
                RedFighterKD = ParseInt(g("red_fighter_KD")),
                BlueFighterKD = ParseInt(g("blue_fighter_KD")),
                RedFighterSigStr = ParseInt(g("red_fighter_sig_str")),
                BlueFighterSigStr = ParseInt(g("blue_fighter_sig_str")),
                RedFighterSigStrPct = ParseDouble(g("red_fighter_sig_str_pct")),
                BlueFighterSigStrPct = ParseDouble(g("blue_fighter_sig_str_pct")),
                RedFighterTotalStr = ParseInt(g("red_fighter_total_str")),
                BlueFighterTotalStr = ParseInt(g("blue_fighter_total_str")),
                RedFighterTD = ParseInt(g("red_fighter_TD")),
                BlueFighterTD = ParseInt(g("blue_fighter_TD")),
                RedFighterTDPct = ParseDouble(g("red_fighter_TD_pct")),
                BlueFighterTDPct = ParseDouble(g("blue_fighter_TD_pct")),
                RedFighterSubAtt = ParseInt(g("red_fighter_sub_att")),
                BlueFighterSubAtt = ParseInt(g("blue_fighter_sub_att")),
                RedFighterRev = ParseInt(g("red_fighter_rev")),
                BlueFighterRev = ParseInt(g("blue_fighter_rev")),
                RedFighterCtrl = g("red_fighter_ctrl"),
                BlueFighterCtrl = g("blue_fighter_ctrl"),
                RedFighterSigStrHead = ParseInt(g("red_fighter_sig_str_head")),
                BlueFighterSigStrHead = ParseInt(g("blue_fighter_sig_str_head")),
                RedFighterSigStrBody = ParseInt(g("red_fighter_sig_str_body")),
                BlueFighterSigStrBody = ParseInt(g("blue_fighter_sig_str_body")),
                RedFighterSigStrLeg = ParseInt(g("red_fighter_sig_str_leg")),
                BlueFighterSigStrLeg = ParseInt(g("blue_fighter_sig_str_leg")),
                RedFighterSigStrDistance = ParseInt(g("red_fighter_sig_str_distance")),
                BlueFighterSigStrDistance = ParseInt(g("blue_fighter_sig_str_distance")),
                RedFighterSigStrClinch = ParseInt(g("red_fighter_sig_str_clinch")),
                BlueFighterSigStrClinch = ParseInt(g("blue_fighter_sig_str_clinch")),
                RedFighterSigStrGround = ParseInt(g("red_fighter_sig_str_ground")),
                BlueFighterSigStrGround = ParseInt(g("blue_fighter_sig_str_ground")),
                RedFighterSigStrHeadPct = ParseDouble(g("red_fighter_sig_str_head_pct")),
                BlueFighterSigStrHeadPct = ParseDouble(g("blue_fighter_sig_str_head_pct")),
                RedFighterSigStrBodyPct = ParseDouble(g("red_fighter_sig_str_body_pct")),
                BlueFighterSigStrBodyPct = ParseDouble(g("blue_fighter_sig_str_body_pct")),
                RedFighterSigStrLegPct = ParseDouble(g("red_fighter_sig_str_leg_pct")),
                BlueFighterSigStrLegPct = ParseDouble(g("blue_fighter_sig_str_leg_pct")),
                RedFighterSigStrDistancePct = ParseDouble(g("red_fighter_sig_str_distance_pct")),
                BlueFighterSigStrDistancePct = ParseDouble(g("blue_fighter_sig_str_distance_pct")),
                RedFighterSigStrClinchPct = ParseDouble(g("red_fighter_sig_str_clinch_pct")),
                BlueFighterSigStrClinchPct = ParseDouble(g("blue_fighter_sig_str_clinch_pct")),
                RedFighterSigStrGroundPct = ParseDouble(g("red_fighter_sig_str_ground_pct")),
                BlueFighterSigStrGroundPct = ParseDouble(g("blue_fighter_sig_str_ground_pct")),
                RedFighterTotalPts = ParseInt(g("red_fighter_total_pts")),
                BlueFighterTotalPts = ParseInt(g("blue_fighter_total_pts"))
            };
        }
    }
}