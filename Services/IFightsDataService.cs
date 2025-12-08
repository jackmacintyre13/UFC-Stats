using System.Collections.Generic;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
    public interface IFightsDataService
    {
        IEnumerable<FightRecord> GetAll();
        IEnumerable<FightRecord> GetAllFights();
        IEnumerable<FightRecord> GetByFighterName(string name);
    }
}