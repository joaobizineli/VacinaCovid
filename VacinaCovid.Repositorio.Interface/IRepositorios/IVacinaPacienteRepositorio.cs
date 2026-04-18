
using VacinaCovid.Entidade.Entidades;

namespace VacinaCovid.Repositorio.Interface.IRepositorios
{
    public interface IVacinaPacienteRepositorio
    {
        void Adicionar(VacinaPaciente vacinaPaciente);
        List<VacinaPaciente> Listar();
        VacinaPaciente ObterId(int id);
        void Atualizar(int id, VacinaPaciente vacinaPaciente);
        void Remover(int id);
        List<VacinaPaciente> ListarPorPaciente(int idPaciente);
        List<VacinaPaciente> ListarPorData(DateTime data);
        int ContagemPorData(DateTime data);
        int ContagemPorDataEHora(DateTime data, TimeSpan hora);
        bool ExisteAgendamento(int idPaciente, DateTime data, TimeSpan hora);
    }
}
