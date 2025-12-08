using System.Collections.Generic;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
 public interface IUfcFightersService
 {
 IEnumerable<UfcFighter> GetAll();
 UfcFighter GetById(string id);
 UfcFighter GetByName(string name);
 }
}