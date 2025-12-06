
using System;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
    // Converts "56%" -> 56m, "0.56" -> 56m, "56" -> 56m; blanks -> null
    public class PercentToDecimalConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            text = text.Trim();

            // Strip % if present
            bool hadPercent = text.EndsWith("%");
            if (hadPercent) text = text[..^1];

            // Try parse numeric
            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
            {
                // If original had %, use numeric value directly (e.g., "56%" -> 56)
                // If it didn't, but looks like fraction (<=1), treat as 0–1 -> convert to %
                if (!hadPercent && val <= 1m) val *= 100m;
                return val;
            }
            return null;
        }
    }

    public sealed class FighterStatMap : ClassMap<FighterStat>
    {
        public FighterStatMap()
        {
            Map(m => m.Name).Name("name");
            Map(m => m.Nickname).Name("nickname");
            Map(m => m.Wins).Name("wins").TypeConverterOption.NullValues(string.Empty, "NA", "N/A");
            Map(m => m.Losses).Name("losses").TypeConverterOption.NullValues(string.Empty, "NA", "N/A");
            Map(m => m.Draws).Name("draws").TypeConverterOption.NullValues(string.Empty, "NA", "N/A");

            Map(m => m.HeightCm).Name("height_cm").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
            Map(m => m.WeightInKg).Name("weight_in_kg").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
            Map(m => m.ReachInCm).Name("reach_in_cm").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);

            Map(m => m.Stance).Name("stance");

            Map(m => m.DateOfBirth).Name("date_of_birth")
                .TypeConverterOption.Format("yyyy-MM-dd", "MM/dd/yyyy", "dd/MM/yyyy")
                .TypeConverterOption.NullValues(string.Empty, "NA", "N/A");

            Map(m => m.SignificantStrikesLandedPerMinute).Name("significant_strikes_landed_per_minute")
                .TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
            Map(m => m.SignificantStrikingAccuracyPct).Name("significant_striking_accuracy").TypeConverter<PercentToDecimalConverter>();
            Map(m => m.SignificantStrikesAbsorbedPerMinute).Name("significant_strikes_absorbed_per_minute")
                .TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
            Map(m => m.SignificantStrikeDefencePct).Name("significant_strike_defence").TypeConverter<PercentToDecimalConverter>();
            Map(m => m.AverageTakedownsLandedPer15Minutes).Name("average_takedowns_landed_per_15_minutes")
                .TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
            Map(m => m.TakedownAccuracyPct).Name("takedown_accuracy").TypeConverter<PercentToDecimalConverter>();
            Map(m => m.TakedownDefensePct).Name("takedown_defense").TypeConverter<PercentToDecimalConverter>();
            Map(m => m.AverageSubmissionsAttemptedPer15Minutes).Name("average_submissions_attempted_per_15_minutes")
                .TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
        }
    }
}
