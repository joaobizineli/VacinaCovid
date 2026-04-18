using VacinaCovid.Entidade.EntidadesDTO;

namespace VacinaCovid.Negocios.Interface.INegocios
{
    public interface IVacinaPacienteNegocio
    {
        List<VacinaPacienteDTO> AgendarVacina(int idPaciente, int idVacina, DateTime data, TimeSpan hora, string local);

        List<VacinaPacienteDTO> ListarAgendamentos();
        List<VacinaPacienteDTO> ListarAgendamentosPaciente(int idPaciente);
        List<VacinaPacienteDTO> ListarAgendamentosPorData(DateTime data);

        VacinaPacienteDTO ObterAgendamento(int id);

        void MarcarComoRealizado(int id, string observacoes = "");
        void CancelarAgendamento(int id);

        void RemoverAgendamento(int id);
    }
}
