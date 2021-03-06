﻿using Microsoft.AspNetCore.Hosting;
using RunningLogApp.Website.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Services
{
    public class JsonFileStravaAPIService : IStravaAPIService
    {
        public IWebHostEnvironment WebHostEnvironment;

        public JsonFileStravaAPIService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string ActivityListFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", "json", "activity_list_2019.json"); }
        }

        private string AthleteFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", "json", "auth_athlete.json"); }
        }

        private string AthleteDataFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", "json", "athlete_data.json"); }
        }

        public Task<List<StravaActivity>> GetActivitiesAsync()
        {
            // TO-DO: Modify this method to retrieve actual data from Strava
            using (var jsonFileReader = File.OpenText(ActivityListFileName))
            {
                var activities = JsonSerializer.Deserialize<StravaActivity[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                foreach (var activity in activities)
                {
                    activity.CalculateAveragePace();
                    activity.CalculateMaxPace();
                    activity.CalculateStrideLength();
                }

                return Task.FromResult(activities.ToList());
            }
        }

        public Task<Athlete> GetAthleteDataAsync()
        {
            var athlete = new Athlete();

            // TO-DO: Modify this method to retrieve actual data from Strava
            using (var jsonFileReader = File.OpenText(AthleteFileName))
            {
                athlete = JsonSerializer.Deserialize<Athlete>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

            using (var jsonFileReader = File.OpenText(AthleteDataFileName))
            {
                athlete.RecentRunTotals = JsonSerializer.Deserialize<Athlete>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).RecentRunTotals;
            }

            using (var jsonFileReader = File.OpenText(AthleteDataFileName))
            {
                athlete.YearToDateRunTotals = JsonSerializer.Deserialize<Athlete>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).YearToDateRunTotals;
            }

            using (var jsonFileReader = File.OpenText(AthleteDataFileName))
            {
                athlete.AllRunTotals = JsonSerializer.Deserialize<Athlete>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).AllRunTotals;
            }

            return Task.FromResult(athlete);
        }
    }
}
