using Microsoft.AspNetCore.Mvc;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Negocios.Interface.INegocios;

namespace VacinaCovid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacientesController : ControllerBase
    {

        private readonly ILogger<PacientesController> _logger;
        private readonly IPacienteNegocio _pacienteNegocio;

        public PacientesController(ILogger<PacientesController> logger, IPacienteNegocio pacienteNegocio)
        {
            _logger = logger;
            _pacienteNegocio = pacienteNegocio;

        }
        [HttpGet("ListarPacientes")]
        public ActionResult<List<PacienteDTO>> ListarPacientes()
        {
            _logger.LogInformation("Listando pacientes");
            return _pacienteNegocio.ListarPacientes();
        }

        [HttpGet("BuscarPacienteId")]
        public ActionResult<PacienteDTO> BuscarPacienteId(int id)
        {
            _logger.LogInformation($"Buscando paciente com ID: {id}");
            return _pacienteNegocio.ObterPaciente(id);
        }
        [HttpDelete("RemoverPaciente")]
        public ActionResult<List<PacienteDTO>> RemoverPaciente(int id)
        {
            _logger.LogInformation($"Removendo paciente com ID: {id}");
            _pacienteNegocio.RemoverPaciente(id);
            return _pacienteNegocio.ListarPacientes();
        }
        [HttpPost("InserirPaciente")]
        public ActionResult<List<PacienteDTO>> InserirPaciente(string nome, DateTime dataNascimento)
        {
            _logger.LogInformation($"Inserindo paciente: {nome}, Data de Nascimento: {dataNascimento}");
            var pacienteInserido = _pacienteNegocio.InserirPaciente(nome, dataNascimento);
            return _pacienteNegocio.ListarPacientes();
        }

        [HttpPut("AlterarPaciente")]
        public ActionResult<List<PacienteDTO>> AlterarPaciente(int id, string nome, DateTime dataNascimento)
        {
            _logger.LogInformation($"Alterando paciente com ID: {id}, Nome: {nome}, Data de Nascimento: {dataNascimento}");
            _pacienteNegocio.AtualizarPaciente(id, nome, dataNascimento);
            return _pacienteNegocio.ListarPacientes();
        }
    }
}
