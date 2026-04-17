using Microsoft.AspNetCore.Mvc;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Negocios.Interface.INegocios;

namespace VacinaCovid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacinasController : ControllerBase
    {
        private readonly ILogger<VacinasController> _logger;
        private readonly IPacienteNegocio _pacienteNegocio;

        public VacinasController(ILogger<VacinasController> logger, IPacienteNegocio pacienteNegocio)
        {
            _logger = logger;
            _pacienteNegocio = pacienteNegocio;

        }

        [HttpGet("ListarPacientes")]
        public ActionResult<List<PacienteDTO>> Get()
        {
            return _pacienteNegocio.ListarPacientes();
        }

        [HttpGet("BuscarPacienteId")]
        public ActionResult<PacienteDTO> Get(int id)
        {
            return _pacienteNegocio.ObterPaciente(id);
        }

        [HttpDelete("RemoverPaciente")]
        public ActionResult Delete(int id)
        {
            _pacienteNegocio.RemoverPaciente(id);
            return NoContent();
        }
        [HttpPost("InserirPaciente")]
        public ActionResult<List<PacienteDTO>> Post(string nome, DateTime dataNascimento)
        {

            var pacienteInserido = _pacienteNegocio.InserirPaciente(nome, dataNascimento);
            return _pacienteNegocio.ListarPacientes();
        }

        [HttpPut("AlterarPaciente")]
        public ActionResult<List<PacienteDTO>> Put(int id, string nome, DateTime dataNascimento)
        {
            _pacienteNegocio.AtualizarPaciente(id, nome, dataNascimento);
            return _pacienteNegocio.ListarPacientes();
        }
    }
}
