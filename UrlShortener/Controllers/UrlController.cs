using Microsoft.AspNetCore.Mvc;
using shortid;
using shortid.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace UrlShortener.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlController : ControllerBase
{
    private readonly ApplicationDbContext ctx;
    private readonly ILogger<UrlController> _logger;
    public UrlController(ApplicationDbContext ctx, ILogger<UrlController> logger)
    {
        this.ctx = ctx;
        _logger = logger;
    }

    [HttpPost]
    public ActionResult<string> Post([FromBody]RegisterUrl registerRequest)
    {
        if (!registerRequest.Validate())
        {
            return BadRequest();
        }

        try
        {
            var options = new GenerationOptions
            {
                Length = 8,
                UseNumbers = true,
            };
            ShortId.SetCharacters("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var shortUrl = ShortId.Generate(options);

            var url = new Url
            {
                LongUrl = registerRequest.Url,
                ShortUrl = shortUrl
            };

            ctx.Url.Add(url);
            ctx.SaveChanges();

            return Ok(new UrlVM
            {
                ShortUrl = shortUrl,
                LongUrl = WebUtility.UrlEncode(registerRequest.Url)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{shortUrl}")]
    public ActionResult Get([FromRoute] string shortUrl)
    {
        try
        {
            var url = ctx.Url.FirstOrDefault(x => x.ShortUrl.Equals(shortUrl));
            if (url == null)
            {
                return NotFound();
            }
            return Ok(url.LongUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
