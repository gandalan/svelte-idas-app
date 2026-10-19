namespace IdasApp.Data;

/// <summary>Example endpoint - replace with your own. Requires a valid IDAS token like every endpoint under /api.</summary>
public static class CounterEndpoint
{
    private static int _count;

    public static void MapCounterEndpoint(this IEndpointRouteBuilder api) =>
        api.MapGet("/counter", () =>
        {
            var count = Interlocked.Increment(ref _count);
            return count % 3 == 0
                ? Results.Json("Hoppla")
                : Results.Json(count);
        });
}
