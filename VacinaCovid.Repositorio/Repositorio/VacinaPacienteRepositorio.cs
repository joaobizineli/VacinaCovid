
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.Enum;
using VacinaCovid.Repositorio.Interface.IRepositorios;

namespace VacinaCovid.Repositorio.Repositorio
{
    public class VacinaPacienteRepositorio : IVacinaPacienteRepositorio
    {
        public static readonly List<VacinaPaciente> VacinaPacientes = new()
{
    new VacinaPaciente
    {
        IdVacinaPaciente = 1,
        IdPaciente = 1,
        IdVacina = 1,
        DataVacinacao = new DateTime(2024, 1, 10),
        HoraVacinacao = new TimeSpan(8, 0, 0),
        Status = StatusEnum.Realizado,
        LocalVacinacao = "Posto Central",
        Observacoes = "Sem reações",
        DataCriacao = DateTime.Now,
        DataAtualizacao = null
    },
    new VacinaPaciente
    {
        IdVacinaPaciente = 2,
        IdPaciente = 2,
        IdVacina = 2,
        DataVacinacao = new DateTime(2024, 1, 10),
        HoraVacinacao = new TimeSpan(9, 0, 0),
        Status = StatusEnum.Realizado,
        LocalVacinacao = "Posto Central",
        Observacoes = "",
        DataCriacao = DateTime.Now,
        DataAtualizacao = null
    },
    new VacinaPaciente
    {
        IdVacinaPaciente = 3,
        IdPaciente = 3,
        IdVacina = 1,
        DataVacinacao = new DateTime(2024, 1, 10),
        HoraVacinacao = new TimeSpan(10, 0, 0),
        Status = StatusEnum.Agendado,
        LocalVacinacao = "UBS Norte",
        Observacoes = "",
        DataCriacao = DateTime.Now,
        DataAtualizacao = null
    },
    new VacinaPaciente
    {
        IdVacinaPaciente = 4,
        IdPaciente = 4,
        IdVacina = 3,
        DataVacinacao = new DateTime(2024, 1, 11),
        HoraVacinacao = new TimeSpan(8, 0, 0),
        Status = StatusEnum.Realizado,
        LocalVacinacao = "UBS Sul",
        Observacoes = "Leve dor no braço",
        DataCriacao = DateTime.Now,
        DataAtualizacao = null
    },
    new VacinaPaciente
    {
        IdVacinaPaciente = 5,
        IdPaciente = 1,
        IdVacina = 2,
        DataVacinacao = new DateTime(2024, 1, 11),
        HoraVacinacao = new TimeSpan(9, 0, 0),
        Status = StatusEnum.Agendado,
        LocalVacinacao = "UBS Sul",
        Observacoes = "",
        DataCriacao = DateTime.Now,
        DataAtualizacao = null
    }
};

        private static int _id = 5;

        public void Adicionar(VacinaPaciente vacinaPaciente)
        {
            var novo = new VacinaPaciente
            {
                IdVacinaPaciente = _id++,
                IdPaciente = vacinaPaciente.IdPaciente,
                IdVacina = vacinaPaciente.IdVacina,
                DataVacinacao = vacinaPaciente.DataVacinacao,
                HoraVacinacao = vacinaPaciente.HoraVacinacao,
                Status = vacinaPaciente.Status,
                LocalVacinacao = vacinaPaciente.LocalVacinacao,
                Observacoes = vacinaPaciente.Observacoes,
                DataCriacao = DateTime.Now,
                DataAtualizacao = null
            };

            VacinaPacientes.Add(novo);
        }

        public List<VacinaPaciente> Listar()
        {
            return VacinaPacientes.ToList();
        }

        public VacinaPaciente ObterId(int id)
        {
            return VacinaPacientes.FirstOrDefault(v => v.IdVacinaPaciente == id);
        }

        public void Atualizar(int id, VacinaPaciente vacinaPaciente)
        {
            var idx = VacinaPacientes.FindIndex(v => v.IdVacinaPaciente == id);
            if (idx < 0) return;

            VacinaPacientes[idx].Status = vacinaPaciente.Status;
            VacinaPacientes[idx].Observacoes = vacinaPaciente.Observacoes;
            VacinaPacientes[idx].DataAtualizacao = DateTime.Now;
        }

        public void Remover(int id)
        {
            var vacinaPaciente = VacinaPacientes.FirstOrDefault(v => v.IdVacinaPaciente == id);
            if (vacinaPaciente != null)
            {
                VacinaPacientes.Remove(vacinaPaciente);
            }
        }


        public List<VacinaPaciente> ListarPorPaciente(int idPaciente)
        {
            return VacinaPacientes
                .Where(v => v.IdPaciente == idPaciente)
                .OrderByDescending(v => v.DataVacinacao)
                .ToList();
        }

        public List<VacinaPaciente> ListarPorData(DateTime data)
        {
            return VacinaPacientes
                .Where(v => v.DataVacinacao.Date == data.Date && v.Status == StatusEnum.Realizado)
                .OrderBy(v => v.HoraVacinacao)
                .ToList();
        }

        public int ContagemPorData(DateTime data)
        {
            return VacinaPacientes
                .Count(v => v.DataVacinacao.Date == data.Date && v.Status == StatusEnum.Agendado);
        }

        public int ContagemPorDataEHora(DateTime data, TimeSpan hora)
        {
            return VacinaPacientes
                .Count(v => v.DataVacinacao.Date == data.Date
                         && v.HoraVacinacao == hora
                         && v.Status == StatusEnum.Agendado);
        }

        public bool ExisteAgendamento(int idPaciente, DateTime data, TimeSpan hora)
        {
            return VacinaPacientes
                .Any(v => v.IdPaciente == idPaciente
                       && v.DataVacinacao.Date == data.Date
                       && v.HoraVacinacao == hora
                       && v.Status == StatusEnum.Agendado);
        }
    }
}
