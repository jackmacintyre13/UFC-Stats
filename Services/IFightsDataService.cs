using System.Collections.Generic;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
    public interface IFightsDataService
    {
        IReadOnlyList<FightRecord> GetAllFights();
    }
}