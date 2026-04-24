using Microsoft.AspNetCore.Mvc;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Entidade.Model;
using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Validador.Manual;

namespace VacinaCovid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacinaPacientesController : ControllerBase
    {
        private readonly ILogger<VacinaPacientesController> _logger;
        private readonly IVacinaPacienteNegocio _vacinaPacienteNegocio;

        public VacinaPacientesController(
            ILogger<VacinaPacientesController> logger,
            IVacinaPacienteNegocio vacinaPacienteNegocio)
        {
            _logger = logger;
            _vacinaPacienteNegocio = vacinaPacienteNegocio;
        }

        [HttpPost("AgendarVacina")]
        public async Task<VacinaPacienteDTO> AgendarVacina([FromBody] VacinaPacienteModel model)
        {
            _logger.LogInformation($"Agendando vacina para paciente ID: {model.IdPaciente}");
            VacinaCovidValidador.Validar(model);
            return await _vacinaPacienteNegocio.AgendarVacina(
                model.IdPaciente,
                model.IdVacina,
                model.DataVacinacao,
                model.HoraVacinacao,
                model.LocalVacinacao);
        }

        [HttpGet("ListarAgendamentos")]
        public async Task<List<VacinaPacienteDTO>> ListarAgendamentos()
        {
            _logger.LogInformation("Listando todos os agendamentos.");
            return await _vacinaPacienteNegocio.ListarAgendamentos();
        }

        [HttpGet("ListarAgendamentosPaciente")]
        public async Task<List<VacinaPacienteDTO>> ListarAgendamentosPaciente(int idPaciente)
        {
            _logger.LogInformation($"Listando agendamentos do paciente ID: {idPaciente}");
            return await _vacinaPacienteNegocio.ListarAgendamentosPaciente(idPaciente);
        }

        [HttpGet("ListarAgendamentosPorData")]
        public async Task<List<VacinaPacienteDTO>> ListarAgendamentosPorData(DateTime data)
        {
            _logger.LogInformation($"Listando agendamentos para data: {data}");
            return await _vacinaPacienteNegocio.ListarAgendamentosPorData(data);
        }

        [HttpGet("ObterAgendamento")]
        public async Task<VacinaPacienteDTO> ObterAgendamento(int id)
        {
            _logger.LogInformation($"Obtendo agendamento ID: {id}");
            return await _vacinaPacienteNegocio.ObterAgendamento(id);
        }

        [HttpPut("MarcarComoRealizado")]
        public async Task MarcarComoRealizado(int id)
        {
            _logger.LogInformation($"Marcando agendamento ID: {id} como realizado.");
            await _vacinaPacienteNegocio.MarcarComoRealizado(id);
        }

        [HttpPut("CancelarAgendamento")]
        public async Task CancelarAgendamento(int id)
        {
            _logger.LogInformation($"Cancelando agendamento ID: {id}");
            await _vacinaPacienteNegocio.CancelarAgendamento(id);
        }

        [HttpDelete("RemoverAgendamento")]
        public async Task RemoverAgendamento(int id)
        {
            _logger.LogInformation($"Removendo agendamento ID: {id}");
            await _vacinaPacienteNegocio.RemoverAgendamento(id);
        }
    }
}