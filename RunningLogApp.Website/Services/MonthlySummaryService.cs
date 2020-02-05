using RunningLogApp.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningLogApp.Website.Services
{
    public class MonthlySummaryService : IMonthlySummaryService
    {
        public List<MonthlySummary> Calculate(List<StravaActivity> activities)
        {
            var monthlySummaries = new MonthlySummary[12];

            // create new monthly summaries for every calendar month
            for (int i = 0; i < 12; i++)
            {
                // TO-DO: Add code to manage activities from different years
                var summary = new MonthlySummary(2019, i + 1);

                // create int Id from year and month
                string date = $"{ summary.Year }{ summary.Month.ToString("D2") } ";
                summary.Id = Int32.Parse(date);

                monthlySummaries[i] = summary;
            }

            foreach (var activity in activities)
            {
                if (activity.Type == "Run")
                {
                    switch (activity.StartDate.Month)
                    {
                        case 1:
                            monthlySummaries[0].AddActivity(activity);
                            break;
                        case 2:
                            monthlySummaries[1].AddActivity(activity);
                            break;
                        case 3:
                            monthlySummaries[2].AddActivity(activity);
                            break;
                        case 4:
                            monthlySummaries[3].AddActivity(activity);
                            break;
                        case 5:
                            monthlySummaries[4].AddActivity(activity);
                            break;
                        case 6:
                            monthlySummaries[5].AddActivity(activity);
                            break;
                        case 7:
                            monthlySummaries[6].AddActivity(activity);
                            break;
                        case 8:
                            monthlySummaries[7].AddActivity(activity);
                            break;
                        case 9:
                            monthlySummaries[8].AddActivity(activity);
                            break;
                        case 10:
                            monthlySummaries[9].AddActivity(activity);
                            break;
                        case 11:
                            monthlySummaries[10].AddActivity(activity);
                            break;
                        case 12:
                            monthlySummaries[11].AddActivity(activity);
                            break;
                        default:
                            break;
                    }
                }
            }

            return monthlySummaries.ToList();
        }
    }
}
