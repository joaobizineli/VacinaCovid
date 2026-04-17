using Microsoft.AspNetCore.Mvc;

namespace VacinaCovid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacinasController : ControllerBase
    {
        public static readonly List<string> vacinas = new () { "CoronaVac", "AstraZeneca", "Pfizer", "Janssen" };

        private readonly ILogger<VacinasController> _logger;

        public VacinasController(ILogger<VacinasController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return vacinas;
        }
    }
}
