
using System;

namespace UfcStatsWeb.Models
{
    public class UfcFight
    {
        public string EventName { get; set; }           // e.g., "UFC 300"
        public DateTime EventDate { get; set; }         // e.g., 2024-04-13
        public string Location { get; set; }            // e.g., "Las Vegas, NV"
        public string WeightClass { get; set; }         // e.g., "Lightweight"
        public string FighterRed { get; set; }          // e.g., "Justin Gaethje"
        public string FighterBlue { get; set; }         // e.g., "Max Holloway"
        public string Winner { get; set; }              // e.g., "Max Holloway" or "Draw"
        public string Method { get; set; }              // e.g., "KO/TKO", "Submission", "Decision (Unanimous)"
        public int Round { get; set; }                  // e.g., 2
        public string Time { get; set; }                // e.g., "1:39"
        public int RedSigStrikes { get; set; }          // optional stats
        public int BlueSigStrikes { get; set; }         // optional stats
        public int RedTakedowns { get; set; }           // optional stats
        public int BlueTakedowns { get; set; }          // optional stats
    }
}
