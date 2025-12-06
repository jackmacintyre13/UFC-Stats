
using Microsoft.AspNetCore.Mvc;
using UfcStatsWeb.Services;

namespace UfcStatsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFighterDataService _data;

        public HomeController(IFighterDataService data) => _data = data;

        [HttpGet]
        public IActionResult Index()
        {
            var fighters = _data.GetAll();
            return View(fighters);
        }
    }
}
