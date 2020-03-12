using loggerInject;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Cloudneeti.Platform.DataCollection.Summarize.Startup))]
namespace Cloudneeti.Platform.DataCollection.Summarize
{
    public class Startup : FunctionsStartup
    {
        public Startup()
        {
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();
        }

    }
}
