
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Repositorio.Interface.IRepositorios;
using VacinaCovid.Utilitarios.Exceptions;
using VacinaCovid.Utilitarios.Messages;

namespace VacinaCovid.Negocios.Negocios
{
    public class PacienteNegocio : IPacienteNegocio
    {
        private readonly IPacienteRepositorio _pacienteRepositorio;


        private Paciente ValidarEObterPaciente(int id)
        {
            if (id <= 0)
                throw new BusinessException(BusinessMessages.IdNegativo);

            var paciente = _pacienteRepositorio.ObterId(id);

            if (paciente == null)
                throw new BusinessException(string.Format(BusinessMessages.PacienteNaoEncontrado, id));
            return paciente;
        }

        public PacienteNegocio(IPacienteRepositorio pacienteRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
        }
        public List<PacienteDTO> InserirPaciente(string nome, DateTime dataNascimento)
        {
            if (string.IsNullOrWhiteSpace(nome) || dataNascimento == default)
                throw new BusinessException(BusinessMessages.NomeOuDataNascimentoObrigatorios);
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
            var paciente = ValidarEObterPaciente(id);

            return new PacienteDTO
            {
                IdPaciente = paciente.IdPaciente,
                Nome = paciente.Nome,
                DataNascimento = paciente.DataNascimento
            };
        }

        public void AtualizarPaciente(int id, string nome, DateTime dataNascimento)
        {
            var paciente = ValidarEObterPaciente(id);

            _pacienteRepositorio.Atualizar(id, nome, dataNascimento);
        }

        public void RemoverPaciente(int id)
        {
            var paciente = ValidarEObterPaciente(id);

            _pacienteRepositorio.Remover(id);
        }

    }
}
