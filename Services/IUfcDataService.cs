
using System.Collections.Generic;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
    public interface IUfcDataService
    {
        IReadOnlyList<UfcFight> GetAllFights();
    }
}
