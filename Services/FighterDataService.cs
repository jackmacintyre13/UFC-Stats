using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.Extensions.Hosting;
using UfcStatsWeb.Models;

namespace UfcStatsWeb.Services
{
    public interface IFighterDataService
    {
        IReadOnlyList<FighterStat> GetAll();
    }

    public class FighterDataService : IFighterDataService
    {
        private readonly List<FighterStat> _cache = new();

        public FighterDataService(IHostEnvironment env)
        {
            var csvPath = Path.Combine(env.ContentRootPath, "wwwroot", "data", "ufc_fighters.csv");
            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"CSV not found at {csvPath}. Ensure the file exists and is set to Copy to Output Directory.");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<FighterStatMap>();


            var records = csv.GetRecords<FighterStat>().ToList();
            _cache.AddRange(records);
        }

        public IReadOnlyList<FighterStat> GetAll() => _cache;
    }
}
