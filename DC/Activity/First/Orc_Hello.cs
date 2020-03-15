using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading;

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
            Thread.Sleep(300000);
            return $"Hello {name}!";
        }
    }
}
