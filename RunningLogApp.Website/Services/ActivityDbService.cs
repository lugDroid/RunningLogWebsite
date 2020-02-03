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
                    existingAthlete.Username = athlete.Username;
                    existingAthlete.ResourceState = athlete.ResourceState;
                    existingAthlete.FirstName = athlete.FirstName;
                    existingAthlete.LastName = athlete.LastName;
                    existingAthlete.City = athlete.City;
                    existingAthlete.State = athlete.State;
                    existingAthlete.Country = athlete.Country;
                    existingAthlete.Sex = athlete.Sex;
                    existingAthlete.Premium = athlete.Premium;
                    existingAthlete.Summit = athlete.Summit;
                    existingAthlete.CreatedAt = athlete.CreatedAt;
                    existingAthlete.UpdatedAt = athlete.UpdatedAt;
                    existingAthlete.BadgeTypeId = athlete.BadgeTypeId;
                    existingAthlete.ProfileMedium = athlete.ProfileMedium;
                    existingAthlete.Profile = athlete.Profile;

                    existingAthlete.RecentRunTotals.Count = athlete.RecentRunTotals.Count;
                    existingAthlete.RecentRunTotals.Distance = athlete.RecentRunTotals.Distance;
                    existingAthlete.RecentRunTotals.MovingTime = athlete.RecentRunTotals.MovingTime;
                    existingAthlete.RecentRunTotals.ElapsedTime = athlete.RecentRunTotals.ElapsedTime;
                    existingAthlete.RecentRunTotals.ElevationGain = athlete.RecentRunTotals.ElevationGain;
                    existingAthlete.RecentRunTotals.AchievementCount = athlete.RecentRunTotals.AchievementCount;

                    existingAthlete.YearToDateRunTotals.Count = athlete.YearToDateRunTotals.Count;
                    existingAthlete.YearToDateRunTotals.Distance = athlete.YearToDateRunTotals.Distance;
                    existingAthlete.YearToDateRunTotals.MovingTime = athlete.YearToDateRunTotals.MovingTime;
                    existingAthlete.YearToDateRunTotals.ElapsedTime = athlete.YearToDateRunTotals.ElapsedTime;
                    existingAthlete.YearToDateRunTotals.ElevationGain = athlete.YearToDateRunTotals.ElevationGain;
                    existingAthlete.YearToDateRunTotals.AchievementCount = athlete.YearToDateRunTotals.AchievementCount;

                    existingAthlete.AllRunTotals.Count = athlete.AllRunTotals.Count;
                    existingAthlete.AllRunTotals.Distance = athlete.AllRunTotals.Distance;
                    existingAthlete.AllRunTotals.MovingTime = athlete.AllRunTotals.MovingTime;
                    existingAthlete.AllRunTotals.ElapsedTime = athlete.AllRunTotals.ElapsedTime;
                    existingAthlete.AllRunTotals.ElevationGain = athlete.AllRunTotals.ElevationGain;
                    existingAthlete.AllRunTotals.AchievementCount = athlete.AllRunTotals.AchievementCount;

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

                // Read totals data from different table
                athlete.AllRunTotals = _context.TotalsData.First(x => x.Id == athleteDbData.AllRunTotalsId);
                athlete.YearToDateRunTotals = _context.TotalsData.First(x => x.Id == athleteDbData.YearToDateRunTotalsId);
                athlete.RecentRunTotals = _context.TotalsData.First(x => x.Id == athleteDbData.RecentRunTotalsId);
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
                        //CopyProperties(sourceProp.GetValue(source), destProp.GetValue(dest));
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
