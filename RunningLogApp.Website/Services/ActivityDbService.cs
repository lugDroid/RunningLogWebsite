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

            if (existingAthletes.Any(x => x.Id == athlete.Id))
            {
                var existingAthlete = existingAthletes.First<Athlete>(x => x.Id == athlete.Id);

                if (existingAthlete != null)
                {
                    CopyProperties(athlete, existingAthlete);

                    _context.Athlete.Update(existingAthlete);
                }
            }
            else
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
            var athletes = _context.Athlete.ToArray();

            foreach (var athlete in athletes)
            {
                Athlete athleteDbData = _context.Athlete.First(x => x.Id == athlete.Id);

                /*athlete.AllTimeRunTotals = _context.RunTotals.First(x => x.Id == athleteDbData.AllTimeRunTotals);
                athlete.YearToDateRunTotals = athleteDbData.YearToDateRunTotals;
                athlete.RecentRunTotals = athleteDbData.RecentRunTotals;*/
            }
            
            return Task.FromResult(athletes);
        }

        public Task<MonthlySummary[]> ReadMonthlySummariesAsync()
        {
            return Task.FromResult(_context.MonthlySummaries.ToArray());
        }

        private void CopyProperties<T,TU>(T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties().Where(x => x.CanWrite).ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var destProp = destProps.First(x => x.Name == sourceProp.Name);

                    // if the property is of type run totals we want to updat its values, not create a new one
                    if (sourceProp.PropertyType == typeof(TotalsData))
                    {
                        CopyProperties(sourceProp.GetValue(source), destProp.GetValue(dest));
                    }
                    else
                    {
                        destProp.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }
    }
}
