using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppOne.Controllers;

[ApiController]
public class DataController : ControllerBase
{
    [Authorize]
    [HttpGet("/api/data")]
    public IActionResult GetProtectedData() =>
        Ok(new
        {
            message = "Protected data",
            timestamp = DateTime.UtcNow.ToString("o"),
            user = User.Identity?.Name
        });
}
