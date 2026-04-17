
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Repositorio.Interface.IRepositorios;

namespace VacinaCovid.Repositorio.Repositorio
{

    public class PacienteRepositorio : IPacienteRepositorio
    {

        public static readonly List<Paciente> Pacientes = new()
        {
            new Paciente { IdPaciente = 1, Nome = "Laura", DataNascimento = new DateTime(1995, 3, 12) },
            new Paciente { IdPaciente = 2, Nome = "João", DataNascimento = new DateTime(1988, 7, 25) },
            new Paciente { IdPaciente = 3, Nome = "Maria", DataNascimento = new DateTime(2000, 11, 5) },
            new Paciente { IdPaciente = 4, Nome = "Jennifer", DataNascimento = new DateTime(1992, 9, 18) }
        };
        private static int _id = 5;

        public void Adicionar(Paciente paciente)
        {
            Pacientes.Add(new Paciente { Nome = paciente.Nome,
                                         DataNascimento = paciente.DataNascimento,
                                         DataVacina = paciente.DataVacina,
                                         IdPaciente = _id++});
        }


        public void Atualizar(int id, string nomeAtualizado, DateTime dataNascimento)
        {
            var idx = Pacientes.FindIndex(p => p.IdPaciente == id);

            if (idx < 0)
                return;

            Pacientes[idx].Nome = nomeAtualizado;
            Pacientes[idx].DataNascimento = dataNascimento;
            Listar();
        }

        public List<Paciente> Listar()
        {
            return Pacientes.Select(p => new Paciente
            {
                IdPaciente = p.IdPaciente,
                Nome = p.Nome,
                DataNascimento = p.DataNascimento
            }).ToList();
        }

        public Paciente ObterId(int id)
        {
            return Pacientes.Where(p => p.IdPaciente == id).Select(p => new Paciente
            {
                IdPaciente = p.IdPaciente,
                Nome = p.Nome,
                DataNascimento = p.DataNascimento
            }).FirstOrDefault();
        }

        public void Remover(int id)
        {
           var paciente = Pacientes.FirstOrDefault(p => p.IdPaciente == id);
           if (paciente != null)
           {
               Pacientes.Remove(paciente);
           }
        }
    }
}
