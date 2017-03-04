using System.Web.Mvc;
using TSP.Services;

namespace TSP.Controllers
{
    public class HomeController : Controller
    {
        private readonly Configuration _config;

        public HomeController(IConfigurationService config)
        {
            _config = config.Get();
        }

        public ActionResult Index()
        {
            ViewBag.IsStartingPointVisible = _config.StartingPoint != null;
            if (ViewBag.IsStartingPointVisible)
            {
                ViewBag.StartingPoint = _config.StartingPoint;
            }
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}