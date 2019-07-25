using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Lecture.Web.Controllers;
using Security.DAO;
using Microsoft.AspNetCore.Http;
using Capstone.Web.DAO;
using Security.Models.Database;

namespace Capstone.Web.Controllers
{
    public class ParksController : AuthenticationController
    {
        protected const string USER_KEY = "UserData";
        private IParkDAO _db;

        public ParksController(IUserSecurityDAO userDb, IParkDAO db, IHttpContextAccessor httpContext) : base(userDb, httpContext)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Home()
        {
            List<Park> parks = _db.GetParks();
            
            return GetAuthenticatedView("Home", parks);
        }

        [HttpGet]
        public IActionResult Detail(string code)
        {
            Park park = _db.GetPark(code);
            List<Weather> weathers = _db.GetWeather(park);
            ParkWeatherViewModel vm = new ParkWeatherViewModel(park, weathers);
            return GetAuthenticatedView("Detail", vm);
        }

        [HttpGet]
        public IActionResult Survey()
        {
            var userMgr = GetUserManager();
            if (userMgr.IsAuthenticated)
            {
                VoteViewModel vm = new VoteViewModel();
                var User = GetSessionData<UserItem>(USER_KEY);
            
                vm.Email = User.Email;
                return GetAuthenticatedView("Survey", vm);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
        }

        [HttpPost]
        public IActionResult SaveSurvey(VoteViewModel vm)
        {
            _db.Vote(vm);
            return RedirectToAction("Favorites");
        }

        [HttpGet]
        public IActionResult Favorites()
        {
            List<Park> parks = _db.ParksWithVotes();
            return GetAuthenticatedView("Favorites", parks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
