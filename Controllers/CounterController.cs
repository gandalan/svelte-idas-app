using Microsoft.AspNetCore.Mvc;

namespace SvelteDotnetAppVite.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController : Controller
{
    private static int _count;

    [HttpGet("/api/counter")]
    public IActionResult Get()
    {
        _count++;
        if (_count % 3 == 0)
        {
            return Json("Hoppla");
        }

        return Json(_count);
    }
}
