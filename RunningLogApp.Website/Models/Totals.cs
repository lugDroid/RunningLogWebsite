using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Models
{
    public class Totals
    {
        [JsonPropertyName("recent_run_totals")]
        public TotalsData RecentRun { get; set; }

        [JsonPropertyName("ytd_run_totals")]
        public TotalsData YearToDateRun { get; set; }

        [JsonPropertyName("all_run_totals")]
        public TotalsData AllRun { get; set; }
    }
}
