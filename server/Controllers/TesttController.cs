using System;
using Microsoft.AspNetCore.Mvc;
using annos_server.Models;

namespace annos_server.Controllers;

[Route("[controller]")]
[ApiController]
public class TesttController : ControllerBase
{
    [HttpGet]
    public ActionResult<Subscription> Get() => new Subscription { Id = 1, Name = "Test Sub", Price = 4.99, NextPaymentDate = DateTime.Now };
}

