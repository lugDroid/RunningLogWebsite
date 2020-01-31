using RunningLogApp.Website.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Models
{
    public class TotalsData
    {
        public long Id { get; set; }
        public long AthleteId { get; set; }
        public Athlete Athlete { get; set; }
        public int Count { get; set; }
        public double Distance { get; set; }

        [JsonPropertyName("moving_time")]
        [JsonConverter(typeof(TimeSpanFromSeconds))]
        public TimeSpan MovingTime { get; set; }

        [JsonPropertyName("elapsed_time")]
        [JsonConverter(typeof(TimeSpanFromSeconds))]
        public TimeSpan ElapsedTime { get; set; }

        [JsonPropertyName("elevation_gain")]
        public double ElevationGain { get; set; }

        [JsonPropertyName("achievement_count")]
        public int AchievementCount { get; set; }
    }
}
