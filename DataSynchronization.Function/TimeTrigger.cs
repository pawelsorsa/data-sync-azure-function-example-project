using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataSynchronization.Function
{
    public class TimeTrigger
    {
        [FunctionName("TimeTrigger")]
        public async Task Run([TimerTrigger("0 */0 * * * *")]TimerInfo myTimer, ILogger log)
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.PostAsync("https://localhost:7192/DataSynchronization", null);
            }
        }
    }
}
