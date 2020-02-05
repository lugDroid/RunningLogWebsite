using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RunningLogApp.Website.Models;
using RunningLogApp.Website.Services;

namespace RunningLogApp.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStravaAPIService _stravaAPIService;
        private readonly IActivityDbService _activityDbService;
        private readonly IMonthlySummaryService _monthlySummaryService;

        public HomeController(ILogger<HomeController> logger, 
                              IStravaAPIService stravaAPIService, 
                              IActivityDbService activityDbService,
                              IMonthlySummaryService monthlySummaryService)
        {
            _logger = logger;
            _stravaAPIService = stravaAPIService;
            _activityDbService = activityDbService;
            _monthlySummaryService = monthlySummaryService;
        }

        public async Task<IActionResult> Index()
        {
            // TO-DO: Get authenticated athlete data from Strava API
            Athlete athlete = await _stravaAPIService.GetAthleteDataAsync();

            var result = await _activityDbService.AddAthleteDataAsync(athlete);

            // TO-DO: Get activities from Strava API
            // now it's reading them from a JSON file
            var activities = await _stravaAPIService.GetActivitiesAsync();

            // Add new activities to database
            result = await _activityDbService.AddActivitiesAsync(activities);

            Console.WriteLine($"{ result } new activities added to the database");

            var tempActivity = activities[0];
            tempActivity.Id = 9054596044;
            tempActivity.Distance = 40000;
            activities.Add(tempActivity);

            // Calculate monthly summaries and save them to the database
            var summaries = _monthlySummaryService.Calculate(activities);
            result = await _activityDbService.AddMonthlySummariesAsync(summaries);

            Console.WriteLine($"{ result } new monthly summaries added to the database");

            return View(athlete);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
