using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Negocios.Interface.INegocios;
using Microsoft.AspNetCore.Mvc;

namespace VacinaCovid.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public ActionResult<List<VacinaPacienteDTO>> AgendarVacina(
            int idPaciente,
            int idVacina,
            DateTime data,
            TimeSpan hora,
            string local)
        {
            _logger.LogInformation($"Agendando vacina para paciente ID: {idPaciente}, Vacina ID: {idVacina}, Data: {data}, Hora: {hora}, Local: {local}");
            return _vacinaPacienteNegocio.AgendarVacina(idPaciente, idVacina, data, hora, local);
        }

        [HttpGet("ListarAgendamentos")]
        public ActionResult<List<VacinaPacienteDTO>> ListarAgendamentos()
        {
            _logger.LogInformation("Listando todos os agendamentos de vacina.");
            return _vacinaPacienteNegocio.ListarAgendamentos();
        }

        [HttpGet("ListarAgendamentosPaciente")]
        public ActionResult<List<VacinaPacienteDTO>> ListarAgendamentosPaciente(int idPaciente)
        {
            _logger.LogInformation($"Listando agendamentos para paciente ID: {idPaciente}");
            return _vacinaPacienteNegocio.ListarAgendamentosPaciente(idPaciente);
        }

        [HttpGet("ListarAgendamentosPorData")]
        public ActionResult<List<VacinaPacienteDTO>> ListarAgendamentosPorData(DateTime data)
        {
            _logger.LogInformation($"Listando agendamentos para data: {data}");
            return _vacinaPacienteNegocio.ListarAgendamentosPorData(data);
        }

        [HttpGet("ObterAgendamento")]
        public ActionResult<VacinaPacienteDTO> ObterAgendamento(int id)
        {
            _logger.LogInformation($"Obtendo agendamento com ID: {id}");
            return _vacinaPacienteNegocio.ObterAgendamento(id);
        }

        [HttpPut("MarcarComoRealizado")]
        public ActionResult MarcarComoRealizado(int id, string observacoes = "")
        {
            _logger.LogInformation($"Marcando agendamento ID: {id} como realizado. Observações: {observacoes}");
            _vacinaPacienteNegocio.MarcarComoRealizado(id, observacoes);
            return Ok("Agendamento marcado como realizado.");
        }

        [HttpPut("CancelarAgendamento")]
        public ActionResult CancelarAgendamento(int id)
        {
            _logger.LogInformation($"Cancelando agendamento ID: {id}");
            _vacinaPacienteNegocio.CancelarAgendamento(id);
            return Ok("Agendamento cancelado.");
        }

        [HttpDelete("RemoverAgendamento")]
        public ActionResult RemoverAgendamento(int id)
        {
            _logger.LogInformation($"Removendo agendamento ID: {id}");
            _vacinaPacienteNegocio.RemoverAgendamento(id);
            return NoContent();
        }
    }
}