using RunningLogApp.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Services
{
    public interface IActivityDbService
    {
        Task<int> AddAthleteDataAsync(Athlete athlete);
        Task<List<Athlete>> ReadAthleteDataAsync();
        Task<int> AddActivitiesAsync(List<StravaActivity> activities);
        Task<List<StravaActivity>> ReadActivitiesAsync();
        Task<int> AddMonthlySummariesAsync(List<MonthlySummary> summaries);
        Task<List<MonthlySummary>> ReadMonthlySummariesAsync();
    }
}
