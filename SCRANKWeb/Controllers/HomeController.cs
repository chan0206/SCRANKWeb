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

        public IActionResult Probabilities()
        {
            return View();
        }

        public IActionResult Rankings()
        {
            
            return View();
        }

        public IActionResult Fantasy()
        {
            return View();
        }
        
        public IActionResult HighSchoolFootballRankings(string pstrClass, int pintSeason, string pstrRankingView, string pstrState, HighSchoolFootballModel rank)
        {
            rank.GetSeasons(pstrState);
            rank.GetClasses(pintSeason,pstrState);
            rank.setStateInfo(pstrState);
            rank.intSeason = pintSeason;
            rank.strClass = pstrClass;
            rank.strRankingView = pstrRankingView;
            rank.strState = pstrState;
            switch (pstrRankingView)
            {
                case "Power":
                    rank.GetRankings(pstrClass, pintSeason, pstrState, "Power");
                    break;
                case "RPI":
                    rank.GetRankings(pstrClass, pintSeason, pstrState, "RPI");
                    break;
                case "SOV":
                    rank.GetRankings(pstrClass, pintSeason, pstrState, "SOV");
                    break;
                case "DOM":
                    rank.GetRankings(pstrClass, pintSeason, pstrState, "DOM");
                    break;
            }            
            
            return View(rank);
        }

        public IActionResult HighSchoolFootballTeam(string pstrClass, int pintSeason, string pstrRankingView, string pstrState, string pstrTeam, HighSchoolFootballModel rank)
        {
            rank.GetSeasons(pstrState);
            rank.GetClasses(pintSeason, pstrState);
            rank.setStateInfo(pstrState);
            rank.GetTeamViewInfo(pintSeason, pstrState);
            rank.intSeason = pintSeason;
            rank.strClass = pstrClass;
            rank.strState = pstrState;
            rank.strTeam = pstrTeam;
            rank.strRankingView = pstrRankingView;


            return View(rank);
        }

        public IActionResult HighSchoolFootballDistrictStandings(string pstrClass, int pintSeason, string pstrRankingView, string pstrState, HighSchoolFootballModel rank)
        {
            rank.GetSeasons(pstrState);
            rank.GetClasses(pintSeason, pstrState);
            rank.setStateInfo(pstrState);
            rank.GetDistricts(pintSeason, pstrState, pstrClass);
            rank.GetDistrictStandings(pintSeason, pstrState);
            rank.intSeason = pintSeason;
            rank.strClass = pstrClass;
            rank.strRankingView = pstrRankingView;
            rank.strState = pstrState;            

            return View(rank);
        }

        public IActionResult CollegeBasketballRankings(Int64 pintSeason, string pstrRankingView, CollegeBasketballModel rank)
        {
            rank.GetSeasons();
            rank.intSeason = pintSeason;
            rank.strRankingView = pstrRankingView;
            switch (pstrRankingView)
            {
                case "Power":
                    rank.GetRankings(pintSeason, "Power");
                    break;
                case "DOM":
                    rank.GetRankings(pintSeason, "Dominance");
                    break;
                case "SOV":
                    rank.GetRankings(pintSeason, "SOV");
                    break;
            }

            return View(rank);
        }

        public IActionResult CollegeBasketballConferenceStandings(Int64 pintSeason, string pstrRankingView, CollegeBasketballModel rank)
        {
            rank.GetSeasons();
            rank.GetConferences(pintSeason);
            rank.GetConferenceStandings(pintSeason);
            rank.intSeason = pintSeason;
            rank.strRankingView = pstrRankingView;

            return View(rank);
        }

        public IActionResult CollegeBasketballTeam(string pstrConference, int pintSeason, string pstrRankingView, string pstrState, string pstrTeam, CollegeBasketballModel rank)
        {
            rank.GetSeasons();
            //rank.GetClasses(pintSeason, pstrState);
            rank.GetTeamViewInfo(pintSeason, pstrState);
            rank.intSeason = pintSeason;
            rank.strConference = pstrConference;
            rank.strTeam = pstrTeam;
            rank.strRankingView = pstrRankingView;


            return View(rank);
        }

        public IActionResult HighSchoolFootballProjections(string pstrClass, int pintSeason, string pstrRankingView, string pstrState, HighSchoolFootballModel rank)
        {
            rank.GetProjectionSeasons(pstrState);
            rank.GetClasses(pintSeason, pstrState);
            rank.setStateInfo(pstrState);
            rank.intSeason = pintSeason;
            rank.strClass = pstrClass;
            rank.strRankingView = pstrRankingView;
            rank.strState = pstrState;
            rank.GetProjections(pstrClass, pintSeason, pstrState);
                    

            return View(rank);
        }

        public IActionResult HighSchoolFootballProbabilities(string pstrClass, int pintSeason, string pstrRankingView, string pstrState, HighSchoolFootballModel rank)
        {
            rank.GetProjectionSeasons(pstrState);
            rank.GetClasses(pintSeason, pstrState);
            rank.setStateInfo(pstrState);
            rank.intSeason = pintSeason;
            rank.strClass = pstrClass;
            rank.strRankingView = pstrRankingView;
            rank.strState = pstrState;
            rank.GetProbabilities(pstrClass, pintSeason, pstrState);


            return View(rank);
        }

        public IActionResult HighSchoolFootballProjectedDistrictStandings(string pstrClass, int pintSeason, string pstrRankingView, string pstrState, HighSchoolFootballModel rank)
        {
            rank.GetSeasons(pstrState);
            rank.GetClasses(pintSeason, pstrState);
            rank.setStateInfo(pstrState);
            rank.GetDistricts(pintSeason, pstrState, pstrClass);
            rank.GetProjectedDistrictStandings(pintSeason, pstrState);
            rank.intSeason = pintSeason;
            rank.strClass = pstrClass;
            rank.strRankingView = pstrRankingView;
            rank.strState = pstrState;

            return View(rank);
        }

        public IActionResult HighSchoolFootballDistrictStandingProbabilities(string pstrClass, int pintSeason, string pstrRankingView, string pstrState, HighSchoolFootballModel rank)
        {
            rank.GetProjectionSeasons(pstrState);
            rank.GetClasses(pintSeason, pstrState);
            rank.setStateInfo(pstrState);
            rank.GetDistricts(pintSeason, pstrState, pstrClass);
            rank.GetDistrictStandingProbabilities(pintSeason, pstrState);
            rank.intSeason = pintSeason;
            rank.strClass = pstrClass;
            rank.strRankingView = pstrRankingView;
            rank.strState = pstrState;

            return View(rank);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
