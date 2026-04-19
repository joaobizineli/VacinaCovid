using Microsoft.AspNetCore.Mvc;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Negocios.Interface.INegocios;

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
        public async Task<List<PacienteDTO>> InserirPaciente(string nome, DateTime dataNascimento)
        {
            _logger.LogInformation($"Inserindo paciente: {nome}");
            return await _pacienteNegocio.InserirPaciente(nome, dataNascimento);
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
        public async Task<List<PacienteDTO>> AlterarPaciente(int id, string nome, DateTime dataNascimento)
        {
            _logger.LogInformation($"Alterando paciente ID: {id}");
            await _pacienteNegocio.AtualizarPaciente(id, nome, dataNascimento);
            return await _pacienteNegocio.ListarPacientes();
        }

        [HttpDelete("RemoverPaciente")]
        public async Task<List<PacienteDTO>> RemoverPaciente(int id)
        {
            _logger.LogInformation($"Removendo paciente ID: {id}");
            await _pacienteNegocio.RemoverPaciente(id);
            return await _pacienteNegocio.ListarPacientes();
        }
    }
}