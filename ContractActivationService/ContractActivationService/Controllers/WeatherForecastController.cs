using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ContractActivationService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://host.docker.internal:5002/WeatherForecast");
        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();

            if (contentStream != null)
            {
                var values = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(contentStream).ToArray();
                return values;
            }
        }
        return new WeatherForecast[1];
    }
}
