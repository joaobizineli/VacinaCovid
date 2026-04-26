using Microsoft.AspNetCore.Mvc;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Entidade.Model;

namespace VacinaCovid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly ILogger<PacientesController> _logger;
        private readonly IPacienteNegocio _pacienteNegocio;

        public PacientesController(ILogger<PacientesController> logger, IPacienteNegocio pacienteNegocio)
        {
            _logger = logger;
            _pacienteNegocio = pacienteNegocio;
        }

        [HttpPost("InserirPaciente")]
        public async Task<PacienteDTO> InserirPaciente([FromBody] PacienteModel model)
        {
            _logger.LogInformation($"Inserindo paciente: {model.Nome}");
            return await _pacienteNegocio.InserirPaciente(model.Nome, model.DataNascimento);
        }

        [HttpGet("ListarPacientes")]
        public async Task<List<PacienteDTO>> ListarPacientes()
        {
            _logger.LogInformation("Listando pacientes.");
            return await _pacienteNegocio.ListarPacientes();
        }

        [HttpGet("ObterPaciente")]
        public async Task<PacienteDTO> ObterPaciente(int id)
        {
            _logger.LogInformation($"Obtendo paciente ID: {id}");
            return await _pacienteNegocio.ObterPaciente(id);
        }

        [HttpPut("AlterarPaciente")]
        public async Task<PacienteDTO> AlterarPaciente(int id, [FromBody] PacienteModel model)
        {
            _logger.LogInformation($"Alterando paciente ID: {id}");
            return await _pacienteNegocio.AtualizarPaciente(id, model.Nome, model.DataNascimento);
        }

        [HttpDelete("RemoverPaciente")]
        public async Task RemoverPaciente(int id)
        {
            _logger.LogInformation($"Removendo paciente ID: {id}");
            await _pacienteNegocio.RemoverPaciente(id);
        }
    }
}