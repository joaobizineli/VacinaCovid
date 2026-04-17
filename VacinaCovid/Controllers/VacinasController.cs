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

        [HttpGet("Listar Pacientes")]
        public ActionResult<List<PacienteDTO>> Get()
        {
            return _pacienteNegocio.ListarPacientes();
        }

        [HttpDelete("Remover Paciente")]
        public ActionResult Delete(int id) {
            _pacienteNegocio.RemoverPaciente(id);
            return NoContent();
        }
        [HttpPost("Inserir Paciente")]
       public ActionResult<PacienteDTO> Post(string nome, DateTime dataNascimento)
        {
                
            var pacienteInserido = _pacienteNegocio.InserirPaciente(nome,dataNascimento);
            return NoContent();
        }
    }
}
