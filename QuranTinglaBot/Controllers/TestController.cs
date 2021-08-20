using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuranTinglaBot.ApiClients;

namespace QuranTinglaBot.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> mLogger;
        private readonly OyatClient mOyatClient;

        public TestController(ILogger<TestController> logger, OyatClient client)
        {
            mLogger = logger;
            mOyatClient = client;
        }

        [HttpGet]
        [Route("qorilar")]
        public async Task<IActionResult> GetOyatEditions([FromQuery]string name)
        {
            var result = await mOyatClient.GetOyatEditions();

            if(string.IsNullOrWhiteSpace(name))
            {
                return Ok(result.Editions);
            }

            var matches = result.Editions
            .Where(qori => string.Compare(qori.EnglishName, name, true) == 0)
            .ToList();

            return Ok(matches);
        }
    }
}