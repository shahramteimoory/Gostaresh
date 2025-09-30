using System.Diagnostics;

namespace MachineReporting.Api.Utilities
{
    public class RequestTimingMiddleware(RequestDelegate next, ActivitySource activitySource)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string operationName = $"{context.Request.Method} {context.Request.Path}";

            using var activity = activitySource.StartActivity(operationName, ActivityKind.Server);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                await next(context);
            }
                catch (Exception ex)
                {
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    activity?.SetTag("exception.message", ex.Message);
                    activity?.SetTag("exception.stacktrace", ex.StackTrace);
                }
            finally
            {
                stopwatch.Stop();

                long elapsedMs = stopwatch.ElapsedMilliseconds;

                activity?.SetTag("http.method", context.Request.Method);
                activity?.SetTag("http.path", context.Request.Path);
                activity?.SetTag("http.status_code", context.Response.StatusCode);
                activity?.SetTag("duration_ms", elapsedMs);

                Console.WriteLine($"[RequestTiming] {operationName} took {elapsedMs} ms");
            }
        }
    }
}