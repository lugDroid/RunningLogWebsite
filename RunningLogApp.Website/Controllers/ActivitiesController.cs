using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunningLogApp.Website.Models;
using RunningLogApp.Website.Services;

namespace RunningLogApp.Website.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityDbService _activityDbService;

        public ActivitiesController(IActivityDbService activitiesService)
        {
            _activityDbService = activitiesService;
        }

        public async Task<IActionResult> Index()
        {
            var activities = await _activityDbService.ReadActivitiesAsync();

            var model = new ActivitiesViewModel()
            {
                Activities = activities
            };

            return View(model);
        }
    }
}