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
        Task<Athlete[]> ReadAthleteDataAsync();
        Task<int> AddActivitiesAsync(StravaActivity[] activities);
        Task<StravaActivity[]> ReadActivitiesAsync();
        Task<int> AddMonthlySummariesAsync(MonthlySummary[] summaries);
        Task<MonthlySummary[]> ReadMonthlySummariesAsync();
    }
}
