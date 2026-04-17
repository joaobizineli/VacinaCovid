
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Repositorio.Interface.IRepositorios;

namespace VacinaCovid.Negocios.Negocios
{
    public class PacienteNegocio : IPacienteNegocio
    {
        private readonly IPacienteRepositorio _pacienteRepositorio;

        public PacienteNegocio(IPacienteRepositorio pacienteRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
        }
        public List<PacienteDTO> InserirPaciente(string nome, DateTime dataNascimento)
            {

            var paciente = new Paciente { Nome = nome, DataNascimento = dataNascimento };
            _pacienteRepositorio.Adicionar(paciente);
            return ListarPacientes();
        }
    
            public List<PacienteDTO> ListarPacientes()
            {
                return _pacienteRepositorio.Listar().Select(p => new PacienteDTO
                {
                    IdPaciente = p.IdPaciente,
                    Nome = p.Nome,
                    DataNascimento = p.DataNascimento
                }).ToList();
        }
    
            public PacienteDTO ObterPaciente(int id)
            {
                return _pacienteRepositorio.ObterId(id) is Paciente paciente ? new PacienteDTO
                {
                    IdPaciente = paciente.IdPaciente,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento
                } : null;
        }
    
            public void AtualizarPaciente(int id, string nome, DateTime dataNascimento)
            {
                 _pacienteRepositorio.Atualizar(_pacienteRepositorio.ObterId(id)?.Nome, nome);
            }
    
            public void RemoverPaciente(int id)
            {
                _pacienteRepositorio.Remover(id);
        }
    }
}
