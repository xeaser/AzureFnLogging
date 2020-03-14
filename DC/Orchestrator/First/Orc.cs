using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using loggerInject.DC.Activity.First;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace loggerInject.DC.Orchestrator.First
{
    public class Orc
    {
        private readonly ILogger<Orc> log;
        public Orc(ILogger<Orc> log)
        {
            this.log = log;
        }

        [FunctionName("Orc")]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            log.LogInformation("Orc started");

            await context.CallActivityAsync<string>(nameof(Orc_Hello), "Tokyo");
            await context.CallActivityAsync<string>(nameof(Orc_Hello), "Seattle");
            await context.CallActivityAsync<string>(nameof(Orc_Hello), "London");
        }
        
    }
}