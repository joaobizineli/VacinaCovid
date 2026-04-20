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

        public PacienteNegocio(IPacienteRepositorio pacienteRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
        }

        public async Task<List<PacienteDTO>> InserirPaciente(string nome, DateTime dataNascimento)
        {
            if (string.IsNullOrWhiteSpace(nome) || dataNascimento == default)
                throw new BusinessException(BusinessMessages.NomeOuDataNascimentoObrigatorios);

            var paciente = new Paciente { Nome = nome, DataNascimento = dataNascimento };
            await _pacienteRepositorio.Inserir(paciente);

            return await ListarPacientes();
        }

        public async Task<List<PacienteDTO>> ListarPacientes()
        {
            var lista = await _pacienteRepositorio.Listar();

            return lista.Select(p => new PacienteDTO
            {
                IdPaciente = p.Id,
                Nome = p.Nome,
                DataNascimento = p.DataNascimento
            }).ToList();
        }

        public async Task<PacienteDTO> ObterPaciente(int id)
        {
            var paciente = await ObterEValidar(id);

            return new PacienteDTO
            {
                IdPaciente = paciente.Id,
                Nome = paciente.Nome,
                DataNascimento = paciente.DataNascimento
            };
        }

        public async Task AtualizarPaciente(int id, string nome, DateTime dataNascimento)
        {
            var paciente = await ObterEValidar(id);

            paciente.Nome = nome;
            paciente.DataNascimento = dataNascimento;

            await _pacienteRepositorio.Atualizar(paciente);
        }

        public async Task RemoverPaciente(int id)
        {
            var paciente = await ObterEValidar(id);
            await _pacienteRepositorio.Deletar(paciente);
        }

        private async Task<Paciente> ObterEValidar(int id)
        {
            if (id <= 0)
                throw new BusinessException(BusinessMessages.IdNegativo);

            var paciente = await _pacienteRepositorio.ObterPorId(id);
            if (paciente == null)
                throw new BusinessException(BusinessMessages.PacienteNaoEncontrado);

            return paciente;
        }
    }
}