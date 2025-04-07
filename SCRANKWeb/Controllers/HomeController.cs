using Microsoft.AspNetCore.Mvc;
using SCRANKWeb.Models;
using System.Diagnostics;

namespace SCRANKWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Projections()
        {
            return View();
        }
        //public ActionResult Rankings()
        //{
        //    using (MvcDB dataBase = new MvcDB())
        //    {

        //    }
        //    return View(RankingsModel);
        //}

        public IActionResult Rankings()
        {
            
            return View();
        }

        public IActionResult Fantasy()
        {
            return View();
        }
        
        public IActionResult HighSchoolFootballRankings(string pstrClass, string pstrSeason, string pstrRankingView, string pstrState, HighSchoolFootballRankingsModel rank)
        {
            rank.GetSeasons(pstrState);
            rank.GetClasses(pstrSeason,pstrState);
            rank.setStateInfo(pstrState);
            rank.strSeason = pstrSeason;
            rank.strClass = pstrClass;
            rank.strRankingView = pstrRankingView;
            rank.strState = pstrState;



            switch (pstrRankingView)
            {
                case "Power":
                    rank.GetRankings(pstrClass, pstrSeason, pstrState, "flngPowerRanking");
                    break;
                case "RPI":
                    rank.GetRankings(pstrClass, pstrSeason, pstrState, "flngRegRPI");
                    break;
                case "SOV":
                    rank.GetRankings(pstrClass, pstrSeason, pstrState, "flngAdjustedStrengthOfVictory");
                    break;

            }
            
            
            return View(rank);
        }

        public IActionResult CollegeBasketballRankings(string pstrSeason, string pstrRankingView, CollegeBasketballRankingsModel rank)
        {
            rank.GetSeasons();
            rank.strSeason = pstrSeason;
            rank.strRankingView = pstrRankingView;
            switch (pstrRankingView)
            {
                case "Power":
                    rank.GetRankings(pstrSeason, "flngPowerRanking");
                    break;
                case "Dominance":
                    rank.GetRankings(pstrSeason, "flngDominanceRating");
                    break;
                case "SOV":
                    rank.GetRankings(pstrSeason, "flngAdjustedStrengthOfVictory");
                    break;
            }

            return View(rank);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
