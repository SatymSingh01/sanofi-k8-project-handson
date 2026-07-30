using Microsoft.AspNetCore.Mvc;

namespace Hub.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult GetWelcome() =>
        Ok(new { message = "Welcome to DemoPlatform Hub", service = "Hub" });
}
