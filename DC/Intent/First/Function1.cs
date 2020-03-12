using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using loggerInject.DC.Orchestrator.First;

namespace loggerInject.DC.Intent.First
{
    public class Function1
    {
        private readonly ILogger<Function1> log;
        public Function1(ILogger<Function1> log)
        {
            this.log = log;
        }

        [FunctionName("Function1")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, methods: "get", Route = null)]HttpRequestMessage req,
            [DurableClient]IDurableOrchestrationClient starter, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = System.Web.HttpUtility.ParseQueryString(req.RequestUri.Query).Get("name");
            string response = $"Hello, {name}";
            await starter.StartNewAsync(nameof(Orc), null);
            return req.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
