using Microsoft.EntityFrameworkCore;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.Enum;
using VacinaCovid.Repositorio.Context;
using VacinaCovid.Repositorio.Interface.IRepositorios;

namespace VacinaCovid.Repositorio.Repositorio
{
        public class VacinaPacienteRepositorio : RepositorioBase<VacinaPaciente>, IVacinaPacienteRepositorio
        {
            public VacinaPacienteRepositorio(VacinaCovidContext context) : base(context) { }

            public Task<List<VacinaPaciente>> ListarPorPaciente(int idPaciente)
            {
                var query = EntitySet
                    .Where(v => v.IdPaciente == idPaciente)
                    .OrderByDescending(v => v.DataVacinacao);

                return query.ToListAsync();
            }

            public Task<List<VacinaPaciente>> ListarPorData(DateTime data)
            {
                var query = EntitySet
                    .Where(v => v.DataVacinacao.Date == data.Date && (v.Status == StatusEnum.Agendado || v.Status == StatusEnum.Realizado))
                    .OrderBy(v => v.HoraVacinacao);

                return query.ToListAsync();
            }

            public Task<int> ContagemPorData(DateTime data)
            {
                var query = EntitySet
                    .Where(v => v.DataVacinacao.Date == data.Date);
                        

                return query.CountAsync();
            }

            public Task<int> ContagemPorDataEHora(DateTime data, TimeSpan hora)
            {
                var query = EntitySet
                    .Where(v => v.DataVacinacao.Date == data.Date
                             && v.HoraVacinacao == hora
                             && (v.Status == StatusEnum.Agendado || v.Status == StatusEnum.Realizado));

                return query.CountAsync();
            }

            public Task<bool> ExisteAgendamento(int idPaciente, DateTime data, TimeSpan hora)
            {
                var query = EntitySet
                    .Where(v => v.IdPaciente == idPaciente
                             && v.DataVacinacao.Date == data.Date
                             && v.HoraVacinacao == hora
                             && v.Status == StatusEnum.Agendado);

                return query.AnyAsync();
            }
        }
    }