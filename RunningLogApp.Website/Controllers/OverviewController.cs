using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunningLogApp.Website.Models;
using RunningLogApp.Website.Services;

namespace RunningLogApp.Website.Controllers
{
    public class OverviewController : Controller
    {
        private readonly IActivityDbService _activityDbService;

        public OverviewController(IActivityDbService activityDbService)
        {
            _activityDbService = activityDbService;
        }

        public async Task<IActionResult> Index()
        {
            var monthlySummaries = await _activityDbService.ReadMonthlySummariesAsync();

            var model = new MonthlySummaryViewModel
            {
                MonthlySummaries = monthlySummaries
            };

            return View(model);
        }
    }
}