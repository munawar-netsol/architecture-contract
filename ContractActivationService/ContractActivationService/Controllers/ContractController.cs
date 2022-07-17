
using ContractDataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ContractActivationService.Controllers;

[ApiController]
[Route("[controller]")]
public class ContractController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ContractActivationContext _contractDbContext;
    public ContractController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory, ContractActivationContext contractDbContext)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _contractDbContext = contractDbContext;
    }
    // GET /Contract
    [HttpGet(Name = "GetContract")]
    public async Task<IEnumerable<Cont>> Get()
    {
        return _contractDbContext.Conts
            .Include(x => x.ContAsets)
            .Select(x => x);
    }
    // GET /Contract/{id}
    [HttpGet("{id}")]
    public ActionResult<ContDto> GetContract(int id)
    {
        var item = _contractDbContext.Conts.FirstOrDefault(x => x.ContId == id);
        if (item == null)
            return NotFound();
        return item.AsDto();
    }

    // POST /Contract
    [HttpPost]
    public async Task<ContDto> PostContract(ContDto cont)
    {
        _contractDbContext.Conts.Add(cont.AsEntity());
        await _contractDbContext.SaveChangesAsync();
        return cont;
    }
}
