using System.Diagnostics;

namespace MachineReporting.Api.Utilities
{
    public class LoggingHandler:DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var response = await base.SendAsync(request, cancellationToken);

            stopwatch.Stop();

            Console.WriteLine($"Request to {request.RequestUri} took {stopwatch.ElapsedMilliseconds} ms and returned {response.StatusCode}");

            return response;
        }
    }
}