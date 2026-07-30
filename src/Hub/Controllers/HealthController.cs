using Microsoft.AspNetCore.Mvc;

namespace Hub.Controllers;

[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet("/health")]
    public IActionResult GetHealth() =>
        Ok(new { status = "healthy", service = "Hub" });
}
