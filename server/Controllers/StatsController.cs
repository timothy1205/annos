using Microsoft.AspNetCore.Mvc;
using annos_server.Services;

namespace annos_server.Controllers;

public class StatsController : AnnosControllerBase
{
    private readonly StatsService statsService;

    public StatsController()
    {
        statsService = new StatsService();
    }

    [HttpGet]
    public async Task<ActionResult<Stats>> Get()
    {
        return await statsService.GetStats();
    }
}

