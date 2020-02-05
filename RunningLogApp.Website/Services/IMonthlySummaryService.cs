using RunningLogApp.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Services
{
    public interface IMonthlySummaryService
    {
        List<MonthlySummary> Calculate(List<StravaActivity> activities);
    }
}
