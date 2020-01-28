using RunningLogApp.Website.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Models
{
    public class StravaActivity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double Distance { get; set; }

        [JsonPropertyName("moving_time")]
        [JsonConverter(typeof(TimeSpanFromSeconds))]
        public TimeSpan MovingTime { get; set; }

        [JsonPropertyName("elapsed_time")]
        [JsonConverter(typeof(TimeSpanFromSeconds))]
        public TimeSpan ElapsedTime { get; set; }

        [JsonPropertyName("total_elevation_gain")]
        public double TotalElevationGain { get; set; }

        public string Type { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("start_date_local")]
        public DateTime StartDateLocal { get; set; }

        [JsonPropertyName("start_latlng")]
        [NotMapped]
        public double[] StartLatLong { get; set; }

        [JsonPropertyName("end_latlng")]
        [NotMapped]
        public double[] EndLatLong { get; set; }

        [JsonPropertyName("location_city")]
        public string LocationCity { get; set; }

        [JsonPropertyName("location_state")]
        public string LocationState { get; set; }

        [JsonPropertyName("location_country")]
        public string LocationCountry { get; set; }

        //[JsonPropertyName("start_latitude")]
        //public double StartLatitude { get; set; }

        //[JsonPropertyName("start_longitude")]
        //public double StartLongitude { get; set; }

        [JsonPropertyName("average_speed")]
        public double AverageSpeed { get; set; }

        [JsonPropertyName("max_speed")]
        public double MaxSpeed { get; set; }

        [JsonPropertyName("average_cadence")]
        public double AverageCadence { get; set; }

        [JsonPropertyName("has_heartrate")]
        public bool HasHeartRate { get; set; }

        [JsonPropertyName("average_heartrate")]
        public double AverageHeartRate { get; set; }

        [JsonPropertyName("max_heartrate")]
        public double MaxHeartRate { get; set; }

        [JsonPropertyName("elev_high")]
        public double ElevationHigh { get; set; }

        [JsonPropertyName("elev_low")]
        public double ElevationLow { get; set; }

        // Calculated properties
        public TimeSpan AveragePace { get; set; }
        public TimeSpan MaxPace { get; set; }
        public double StrideLength { get; set; }


        public override string ToString()
        {
            return JsonSerializer.Serialize<StravaActivity>(this);
        }

        public void CalculateAveragePace()
        {
            if (AverageSpeed != 0)
            {
                AveragePace = TimeSpan.FromSeconds(1000 / AverageSpeed);
            }
            else
            {
                AveragePace = TimeSpan.FromSeconds(0);
            }

        }

        public void CalculateMaxPace()
        {
            if (MaxSpeed != 0)
            {
                MaxPace = TimeSpan.FromSeconds(1000 / MaxSpeed);
            }
            else
            {
                MaxPace = TimeSpan.FromSeconds(0);
            }
        }

        public void CalculateStrideLength()
        {
            StrideLength = Distance / (AverageCadence * MovingTime.TotalMinutes * 2);
        }
    }
}
