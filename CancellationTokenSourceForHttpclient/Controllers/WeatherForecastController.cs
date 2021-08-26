using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CancellationTokenSourceForHttpclient.Controllers
{
    [ApiController]

    //[Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly HttpClient _httpClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("cts");
        }

        [HttpGet]
        [Route("test1")]
        public async Task<string> Get1(string target = "")
        {
            var cts1S = new CancellationTokenSource(1000);
            var result = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(target) ? "test1" : target, cts1S.Token);

            return await result.Content.ReadAsStringAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("test2")]
        public async Task<string> Get2()
        {
            var cts2S = new CancellationTokenSource(2000);
            var result = await _httpClient.GetAsync("test2", cts2S.Token);

            return await result.Content.ReadAsStringAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("test3")]
        public async Task<string> Get3()
        {
            var cts3S = new CancellationTokenSource(3000);
            var result = await _httpClient.GetAsync("test3", cts3S.Token);

            return await result.Content.ReadAsStringAsync(new CancellationToken());
        }
    }
}