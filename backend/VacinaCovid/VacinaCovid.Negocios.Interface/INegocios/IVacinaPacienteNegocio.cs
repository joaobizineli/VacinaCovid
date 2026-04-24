using VacinaCovid.Entidade.EntidadesDTO;

namespace VacinaCovid.Negocios.Interface.INegocios
{
    public interface IVacinaPacienteNegocio
    {
        Task<VacinaPacienteDTO> AgendarVacina(int idPaciente, int idVacina, DateTime data, TimeSpan hora, string local);

        Task<List<VacinaPacienteDTO>> ListarAgendamentos();
        Task<List<VacinaPacienteDTO>> ListarAgendamentosPaciente(int idPaciente);
        Task<List<VacinaPacienteDTO>> ListarAgendamentosPorData(DateTime data);

        Task<VacinaPacienteDTO> ObterAgendamento(int id);

        Task MarcarComoRealizado(int id);
        Task CancelarAgendamento(int id);
        Task RemoverAgendamento(int id);
    }
}