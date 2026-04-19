using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Repositorio.Context;
using VacinaCovid.Repositorio.Interface.IRepositorios;

namespace VacinaCovid.Repositorio.Repositorio
{
    public class PacienteRepositorio : RepositorioBase<Paciente>, IPacienteRepositorio
    {
        public PacienteRepositorio(VacinaCovidContext context)
            : base(context)
        {
        }
    }
}