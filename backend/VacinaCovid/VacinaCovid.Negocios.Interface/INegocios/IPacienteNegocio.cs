using VacinaCovid.Entidade.EntidadesDTO;

namespace VacinaCovid.Negocios.Interface.INegocios
{
    public interface IPacienteNegocio
    {
        Task<List<PacienteDTO>> InserirPaciente(string nome, DateTime dataNascimento);

        Task<List<PacienteDTO>> ListarPacientes();

        Task<PacienteDTO> ObterPaciente(int id);

        Task AtualizarPaciente(int id, string nome, DateTime dataNascimento);

        Task RemoverPaciente(int id);
    }
}