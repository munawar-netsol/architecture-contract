using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Publisher.Events.Publishers;

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
    private readonly IBus _bus;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory, IBus bus, IRequestClient<ContractCreateMessageEnvelop> requestClient)
    {
        _logger = logger;
        _bus = bus;
        _httpClientFactory = httpClientFactory;
    }
    [Route("MyIndex")]
    public async Task<IEnumerable<WeatherForecast>> GetForecast()
    {        
        await SubmitContractEvents.SubmitContract(_bus);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
    }
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        // Local debug
        //var url = "https://localhost:9002/WeatherForecast"; 

        // With Docker files and access ctm from Host machine.
        //var url = "http://host.docker.internal:5002/WeatherForecast";

        // With Docker compose file and access ctm from Host machine.
        var url = "http://contract-modification-service/WeatherForecast";

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
