using RunningLogApp.Website.Data;
using RunningLogApp.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Services
{
    public class ActivityDbService : IActivityDbService
    {
        private readonly ApplicationDbContext _context;

        public ActivityDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddActivitiesAsync(StravaActivity[] newActivities)
        {
            var existingActivities = await ReadActivitiesAsync();
            
            foreach(var newActivity in newActivities)
            {
                if (newActivity.Type != "Run" || existingActivities.Any(i => i.Id == newActivity.Id))
                {
                    continue;
                }
                else
                {
                    _context.Activities.Add(newActivity);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAthleteDataAsync(Athlete athlete)
        {
            var existingAthletes = await ReadAthleteDataAsync();

            // TO-DO: Add code to update existing athlete data with new info
            if (!existingAthletes.Any(x => x.Id == athlete.Id))
            {
                _context.Athlete.Add(athlete);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddMonthlySummariesAsync(MonthlySummary[] summaries)
        {
            var existingMonthlySummaries = await ReadMonthlySummariesAsync();

            // TO-DO: Add code to update existing summaries with new info
            foreach (var newSummary in summaries)
            {
                if (existingMonthlySummaries.Any(x => x.Id == newSummary.Id))
                {
                    continue;
                }
                else
                {
                    _context.MonthlySummaries.Add(newSummary);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public Task<StravaActivity[]> ReadActivitiesAsync()
        {
            return Task.FromResult(_context.Activities.ToArray());
        }

        public Task<Athlete[]> ReadAthleteDataAsync()
        {
            return Task.FromResult(_context.Athlete.ToArray());
        }

        public Task<MonthlySummary[]> ReadMonthlySummariesAsync()
        {
            return Task.FromResult(_context.MonthlySummaries.ToArray());
        }
    }
}
