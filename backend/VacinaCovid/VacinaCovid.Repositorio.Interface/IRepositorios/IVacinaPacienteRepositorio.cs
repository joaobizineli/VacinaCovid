using VacinaCovid.Entidade.Entidades;

namespace VacinaCovid.Repositorio.Interface.IRepositorios
{
    public interface IVacinaPacienteRepositorio : IRepositorioBase<VacinaPaciente>
    {
        // Herda tudo de IRepositorioBase<VacinaPaciente>

        // Queries específicas
        Task<List<VacinaPaciente>> ListarPorPaciente(int idPaciente);
        Task<List<VacinaPaciente>> ListarPorData(DateTime data);
        Task<int> ContagemPorData(DateTime data);
        Task<int> ContagemPorDataEHora(DateTime data, TimeSpan hora);
        Task<bool> ExisteAgendamento(int idPaciente, DateTime data, TimeSpan hora);
    }
}