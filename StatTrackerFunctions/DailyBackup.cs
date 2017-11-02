using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;

namespace StatTrackerFunctions
{
    public static class DailyBackup
    {
        [FunctionName("DailyBackup")]
        public static void Run([TimerTrigger("0 0 3 1/1 * ? *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            using (var client = new HttpClient())
            {
                client.PostAsync("http://stattrackerwebapi.azurewebsites.net", new StringContent(""));
            }
        }
    }
}
