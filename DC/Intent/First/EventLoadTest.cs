// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using loggerInject.DC.Orchestrator.First;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace loggerInject.DC.Intent.First
{
    public class EventLoadTest
    {
        private readonly ILogger<EventLoadTest> log;
        public EventLoadTest(ILogger<EventLoadTest> log)
        {
            this.log = log;
        }

        [FunctionName("EventLoadTest")]
        public async Task Run([EventGridTrigger]EventGridEvent eventGridEvent 
            ,[DurableClient]IDurableOrchestrationClient starter)
        {
            log.LogInformation(eventGridEvent.Data.ToString());
            log.LogInformation("C# EventGrid trigger function processed a request.");
            log.LogDebug("ex");

            string name = JObject.Parse(eventGridEvent.Data.ToString()).SelectToken("name").ToString();
            await starter.StartNewAsync(nameof(Orc), name);
        }
    }
}
