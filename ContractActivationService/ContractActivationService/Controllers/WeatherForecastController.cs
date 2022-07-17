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
        // Local debug
        //var url = "https://localhost:9002/WeatherForecast"; 

        // With Docker files and access ctm from Host machine.
        //var url = "http://host.docker.internal:5002/WeatherForecast";

        // With Docker compose file and access ctm from Host machine.
        var url = "http://start_contract-modification-service_1/WeatherForecast";

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
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
