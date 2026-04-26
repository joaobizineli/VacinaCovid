using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Entidade.Enum;
using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Repositorio.Interface.IRepositorios;
using VacinaCovid.Utilitarios.Exceptions;
using VacinaCovid.Utilitarios.Messages;

namespace VacinaCovid.Negocios.Negocios
{
    public class VacinaPacienteNegocio : IVacinaPacienteNegocio
    {
        private readonly IVacinaPacienteRepositorio _vacinaPacienteRepositorio;
        private readonly IPacienteRepositorio _pacienteRepositorio;

        public VacinaPacienteNegocio(
            IVacinaPacienteRepositorio vacinaPacienteRepositorio,
            IPacienteRepositorio pacienteRepositorio)
        {
            _vacinaPacienteRepositorio = vacinaPacienteRepositorio;
            _pacienteRepositorio = pacienteRepositorio;
        }

        public async Task<VacinaPacienteDTO> AgendarVacina(
            int idPaciente, int idVacina, DateTime data, TimeSpan hora, string local)
        {
            var paciente = await _pacienteRepositorio.ObterPorId(idPaciente);
            if (paciente == null)
                throw new BusinessException(BusinessMessages.PacienteNaoEncontrado);

            if (data.Date < DateTime.Today)
                throw new BusinessException(BusinessMessages.DataPassada);

            var contagemDia = await _vacinaPacienteRepositorio.ContagemPorData(data);
            if (contagemDia >= 20)
                throw new BusinessException(BusinessMessages.LimiteDiarioUltrapassado);

            var contagemHora = await _vacinaPacienteRepositorio.ContagemPorDataEHora(data, hora);
            if (contagemHora >= 2)
                throw new BusinessException(BusinessMessages.LimiteHorarioUltrapassado);

            var agendamentosDoDia = await _vacinaPacienteRepositorio.ListarPorData(data);
            var conflito = agendamentosDoDia.Any(a =>
                Math.Abs((a.HoraVacinacao - hora).TotalHours) < 1);

            if (conflito)
                throw new BusinessException(BusinessMessages.IntervaloConflitante);

            var novoAgendamento = new VacinaPaciente
            {
                IdPaciente = idPaciente,
                IdVacina = idVacina,
                DataVacinacao = data,
                HoraVacinacao = hora,
                Status = StatusEnum.Agendado,
                LocalVacinacao = local,
            };

            await _vacinaPacienteRepositorio.Inserir(novoAgendamento);

            return await ObterAgendamento(novoAgendamento.Id);
        }

        public async Task<List<VacinaPacienteDTO>> ListarAgendamentos()
        {
            var lista = await _vacinaPacienteRepositorio.Listar();

            return lista
                .OrderBy(v => v.DataVacinacao)
                .ThenBy(v => v.HoraVacinacao)
                .Select(ConverterParaDTO)
                .ToList();
        }

        public async Task<List<VacinaPacienteDTO>> ListarAgendamentosPaciente(int idPaciente)
        {
            var paciente = await _pacienteRepositorio.ObterPorId(idPaciente);
            if (paciente == null)
                throw new BusinessException(BusinessMessages.PacienteNaoEncontrado);

            var lista = await _vacinaPacienteRepositorio.ListarPorPaciente(idPaciente);
            return lista.Select(ConverterParaDTO).ToList();
        }

        public async Task<List<VacinaPacienteDTO>> ListarAgendamentosPorData(DateTime data)
        {
            var lista = await _vacinaPacienteRepositorio.ListarPorData(data);
            return lista.Select(ConverterParaDTO).ToList();
        }

        public async Task<VacinaPacienteDTO> ObterAgendamento(int id)
        {
            var agendamento = await _vacinaPacienteRepositorio.ObterPorId(id);
            if (agendamento == null)
                throw new BusinessException(BusinessMessages.AgendamentoNaoEncontrado);

            return ConverterParaDTO(agendamento);
        }

        public async Task MarcarComoRealizado(int id)
        {
            var agendamento = await _vacinaPacienteRepositorio.ObterPorId(id);
            if (agendamento == null)
                throw new BusinessException(BusinessMessages.AgendamentoNaoEncontrado);

            if (agendamento.Status != StatusEnum.Agendado)
                throw new BusinessException(BusinessMessages.AgendamentoNaoAgendado);

            agendamento.Status = StatusEnum.Realizado;

            await _vacinaPacienteRepositorio.Atualizar(agendamento);
        }

        public async Task CancelarAgendamento(int id)
        {
            var agendamento = await _vacinaPacienteRepositorio.ObterPorId(id);
            
            if (agendamento == null)
                throw new BusinessException(BusinessMessages.AgendamentoNaoEncontrado);

            if (agendamento.Status != StatusEnum.Agendado)
                throw new BusinessException(BusinessMessages.AgendamentoRealizadoCancel);

            agendamento.Status = StatusEnum.Cancelado;
            await _vacinaPacienteRepositorio.Atualizar(agendamento);
        }

        public async Task RemoverAgendamento(int id)
        {
            var agendamento = await _vacinaPacienteRepositorio.ObterPorId(id);
            if (agendamento == null)
                throw new BusinessException(BusinessMessages.AgendamentoNaoEncontrado);

            if (agendamento.Status == StatusEnum.Realizado || agendamento.Status == StatusEnum.Cancelado)
                throw new BusinessException(BusinessMessages.AgendamentoRealizadoDelete);

            await _vacinaPacienteRepositorio.DeletarPorId(id);
        }

        private VacinaPacienteDTO ConverterParaDTO(VacinaPaciente v) => new()
        {
            IdVacinaPaciente = v.Id,
            IdPaciente = v.IdPaciente,
            IdVacina = v.IdVacina,
            DataVacinacao = v.DataVacinacao,
            HoraVacinacao = v.HoraVacinacao,
            Status = v.Status.ToString(),
            LocalVacinacao = v.LocalVacinacao,
        };
    }
}