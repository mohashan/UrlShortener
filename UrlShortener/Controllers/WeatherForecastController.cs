using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{


    private readonly ILogger<WeatherForecastController> _logger;


    [HttpGet(Name = "Get")]
    public ActionResult<UrlVM> Get()
    {
        return Ok(new UrlVM
        {
            LongUrl = "https://www.google.com",
            ShortUrl = "https://www.google.com"
        });
    }
}
