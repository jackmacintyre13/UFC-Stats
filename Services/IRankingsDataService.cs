using System.Collections.Generic;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
 public interface IRankingsDataService
 {
 IEnumerable<Ranking> GetRankings();
 }
}
