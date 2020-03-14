using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace loggerInject.DC.Activity.First
{
    public class Orc_Hello
    {
        private readonly ILogger<Orc_Hello> log;

        public Orc_Hello(ILogger<Orc_Hello> log)
        {
            this.log = log;
        }

        [FunctionName("Orc_Hello")]
        public string SayHello([ActivityTrigger] string name)
        {
            using (log.BeginScope(new Dictionary<string, object>
            {
                ["name"] = name
            }))
            {
                try
                {
                    int data = 0;
                    int d = 0;
                    var x = data / d;
                }
                catch (Exception ex)
                {
                    log.LogError(ex, $"zero by zero from name: {name}");
                }

                return $"Hello {name}!";
            }
        }
    }
}
