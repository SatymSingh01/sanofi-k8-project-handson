using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Controllers;

[ApiController]
public class DataController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DataController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("/api/data")]
    public async Task<IActionResult> ProxyData(CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("AppOne");
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/data");

        if (Request.Headers.Authorization.Count > 0)
        {
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(
                Request.Headers.Authorization.ToString());
        }

        var response = await client.SendAsync(request, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return new ContentResult
        {
            Content = content,
            ContentType = "application/json",
            StatusCode = (int)response.StatusCode
        };
    }
}
