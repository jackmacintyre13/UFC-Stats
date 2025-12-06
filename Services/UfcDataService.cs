
using System;
using System.Collections.Generic;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
    public class UfcDataService : IUfcDataService
    {
        private readonly List<UfcFight> _fights = new()
        {
            // --- Synthetic sample data (edit/extend freely) ---
            new UfcFight {
                EventName = "UFC 300",
                EventDate = new DateTime(2024, 4, 13),
                Location = "Las Vegas, NV",
                WeightClass = "Bantamweight",
                FighterRed = "Aljamain Sterling",
                FighterBlue = "Calvin Kattar",
                Winner = "Aljamain Sterling",
                Method = "Decision (Unanimous)",
                Round = 3, Time = "5:00",
                RedSigStrikes = 54, BlueSigStrikes = 39,
                RedTakedowns = 3, BlueTakedowns = 0
            },
            new UfcFight {
                EventName = "UFC 300",
                EventDate = new DateTime(2024, 4, 13),
                Location = "Las Vegas, NV",
                WeightClass = "Bantamweight",
                FighterRed = "Deiveson Figueiredo",
                FighterBlue = "Cody Garbrandt",
                Winner = "Deiveson Figueiredo",
                Method = "Submission",
                Round = 2, Time = "3:23",
                RedSigStrikes = 27, BlueSigStrikes = 16,
                RedTakedowns = 2, BlueTakedowns = 0
            },
            new UfcFight {
                EventName = "UFC 300",
                EventDate = new DateTime(2024, 4, 13),
                Location = "Las Vegas, NV",
                WeightClass = "Lightweight",
                FighterRed = "Justin Gaethje",
                FighterBlue = "Max Holloway",
                Winner = "Max Holloway",
                Method = "KO/TKO",
                Round = 5, Time = "4:59",
                RedSigStrikes = 89, BlueSigStrikes = 121,
                RedTakedowns = 0, BlueTakedowns = 0
            },
            new UfcFight {
                EventName = "UFC 299",
                EventDate = new DateTime(2024, 3, 9),
                Location = "Miami, FL",
                WeightClass = "Bantamweight",
                FighterRed = "Sean O'Malley",
                FighterBlue = "Marlon Vera",
                Winner = "Sean O'Malley",
                Method = "Decision (Unanimous)",
                Round = 5, Time = "5:00",
                RedSigStrikes = 143, BlueSigStrikes = 63,
                RedTakedowns = 0, BlueTakedowns = 1
            },
            new UfcFight {
                EventName = "UFC 298",
                EventDate = new DateTime(2024, 2, 17),
                Location = "Anaheim, CA",
                WeightClass = "Featherweight",
                FighterRed = "Alexander Volkanovski",
                FighterBlue = "Ilia Topuria",
                Winner = "Ilia Topuria",
                Method = "KO/TKO",
                Round = 2, Time = "3:32",
                RedSigStrikes = 25, BlueSigStrikes = 36,
                RedTakedowns = 0, BlueTakedowns = 0
            },
            new UfcFight {
                EventName = "UFC 295",
                EventDate = new DateTime(2023, 11, 11),
                Location = "New York, NY",
                WeightClass = "Light Heavyweight",
                FighterRed = "Jiří Procházka",
                FighterBlue = "Alex Pereira",
                Winner = "Alex Pereira",
                Method = "KO/TKO",
                Round = 2, Time = "4:08",
                RedSigStrikes = 23, BlueSigStrikes = 36,
                RedTakedowns = 1, BlueTakedowns = 0
            },
            new UfcFight {
                EventName = "UFC 294",
                EventDate = new DateTime(2023, 10, 21),
                Location = "Abu Dhabi, UAE",
                WeightClass = "Lightweight",
                FighterRed = "Islam Makhachev",
                FighterBlue = "Alexander Volkanovski",
                Winner = "Islam Makhachev",
                Method = "KO/TKO",
                Round = 1, Time = "3:06",
                RedSigStrikes = 20, BlueSigStrikes = 6,
                RedTakedowns = 0, BlueTakedowns = 0
            },
            new UfcFight {
                EventName = "UFC 287",
                EventDate = new DateTime(2023, 4, 8),
                Location = "Miami, FL",
                WeightClass = "Middleweight",
                FighterRed = "Alex Pereira",
                FighterBlue = "Israel Adesanya",
                Winner = "Israel Adesanya",
                Method = "KO/TKO",
                Round = 2, Time = "4:21",
                RedSigStrikes = 25, BlueSigStrikes = 30,
                RedTakedowns = 0, BlueTakedowns = 0
            }
            // Add/modify fights as you wish
        };

        public IReadOnlyList<UfcFight> GetAllFights() => _fights;
    }
}
