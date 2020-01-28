using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Models
{
    public class MonthlySummary
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public int NumberOfRuns { get; set; }
        public double TotalDistance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public TimeSpan AveragePace { get; set; }
        public Double AverageRunCadence { get; set; }
        public Double AverageStrideLength { get; set; }
        public Double AverageHeartrate { get; set; }
        public Double TotalCalories { get; set; }

        private readonly string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private TimeSpan _totalAveragePace;
        private double _totalAverageRunCadence;
        private double _totalAverageStrideLength;
        private double _totalAverageHeartrate;

        public MonthlySummary(int year, int month)
        {
            Year = year;
            Month = month;
            Name = months[month - 1];
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize<MonthlySummary>(this);
        }

        public void AddActivity(StravaActivity activity)
        {
            NumberOfRuns++;
            TotalDistance += activity.Distance / 1000;
            TotalTime += activity.MovingTime;

            _totalAveragePace += activity.AveragePace;
            AveragePace = _totalAveragePace / NumberOfRuns;

            _totalAverageRunCadence += activity.AverageCadence;
            AverageRunCadence = Math.Round(_totalAverageRunCadence / NumberOfRuns, 1);

            _totalAverageStrideLength += activity.StrideLength;
            AverageStrideLength = _totalAverageStrideLength / NumberOfRuns;

            _totalAverageHeartrate += activity.AverageHeartRate;
            AverageHeartrate = Math.Round(_totalAverageHeartrate / NumberOfRuns, 1);
        }
    }
}
