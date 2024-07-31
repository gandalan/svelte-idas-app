namespace SvelteDotnetAppVite.Data;

public static class CounterEndpoint
{
    private static int _count;
    public static void MapCounterEndpoint(this IEndpointRouteBuilder app) =>
        app.MapGet("/api/counter", () =>
        {
            return ++_count % 3 == 0
                ? Results.Json("Hoppla")
                : Results.Json(_count);
        });
}
