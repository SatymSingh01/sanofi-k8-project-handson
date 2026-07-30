using Microsoft.AspNetCore.Mvc;

namespace AuthSite.Controllers;

[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet("/health")]
    public IActionResult GetHealth() =>
        Ok(new { status = "healthy", service = "AuthSite" });
}
