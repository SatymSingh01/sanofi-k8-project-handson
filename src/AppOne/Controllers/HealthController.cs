using Microsoft.AspNetCore.Mvc;

namespace AppOne.Controllers;

[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet("/health")]
    public IActionResult GetHealth() =>
        Ok(new { status = "healthy", service = "AppOne" });
}
