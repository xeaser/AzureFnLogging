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
using Microsoft.Azure.EventGrid.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.EventGrid;

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
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                int events = Convert.ToInt32(System.Web.HttpUtility.ParseQueryString(req.RequestUri.Query).Get("events"));

                var azTopicEndpoint = new Uri("https://loadtest.eastus-1.eventgrid.azure.net/api/events").Host;
                TopicCredentials topicCredentials = new TopicCredentials("6n6qW0eps/G3s/a7bMLXRUMoHGa7cVXlJQB8NEJowMo=");
                var client = new EventGridClient(topicCredentials);

                List<int> iterations = Enumerable.Range(0, events).ToList();
                foreach(var i in iterations) { 
                    List<EventGridEvent> eventsList = new List<EventGridEvent>();
                    eventsList.Add(new EventGridEvent()
                    {
                        Id = Guid.NewGuid().ToString(),
                        EventType = "LoadTest",
                        Data = new Dictionary<string, string>{
                        { "name", "Parag" }
                               },
                        EventTime = DateTime.UtcNow,
                        Subject = "LoadTest",
                        DataVersion = "1.0"
                    });

                    await client.PublishEventsAsync(azTopicEndpoint, eventsList);
                };
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                log.LogError(ex, ex.Message);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
