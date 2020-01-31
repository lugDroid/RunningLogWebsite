using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Models
{
    public class Athlete
    {
        public long Id { get; set; }
        public string Username { get; set; }

        [JsonPropertyName("resource_state")]
        public int ResourceState { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public char Sex { get; set; }
        public bool Premium { get; set; }
        public bool Summit { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("badge_type_id")]
        public int BadgeTypeId { get; set; }

        [JsonPropertyName("profile_medium")]
        public string ProfileMedium { get; set; }
        public string Profile { get; set; }
        public Totals Totals { get; set; }
    }
}
