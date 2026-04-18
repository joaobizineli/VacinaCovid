
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.EntidadesDTO;
using VacinaCovid.Entidade.Enum;
using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Repositorio.Interface.IRepositorios;
using VacinaCovid.Utilitarios.Exceptions;

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

        public List<VacinaPacienteDTO> AgendarVacina(
            int idPaciente,
            int idVacina,
            DateTime data,
            TimeSpan hora,
            string local)
        {
            var paciente = _pacienteRepositorio.ObterId(idPaciente);
            if (paciente == null)
                throw new BusinessException($"Paciente {idPaciente} não encontrado.");

            if (data.Date < DateTime.Today)
                throw new BusinessException("Não é possível agendar para datas passadas.");

            if (hora < new TimeSpan(8, 0, 0) || hora > new TimeSpan(17, 0, 0))
                throw new BusinessException("Horário deve estar entre 08:00 e 17:00.");

            var contagemDia = _vacinaPacienteRepositorio.ContagemPorData(data);
            if (contagemDia >= 20)
                throw new BusinessException("Limite de 20 agendamentos por dia já atingido.");

            var contagemHora = _vacinaPacienteRepositorio.ContagemPorDataEHora(data, hora);
            if (contagemHora >= 2)
                throw new BusinessException("Máximo de 2 agendamentos para este horário já atingido.");

            var todosAgendamentosNoDia = _vacinaPacienteRepositorio.ListarPorData(data);

            var conflito = todosAgendamentosNoDia.Any(a =>
                a.Status == StatusEnum.Agendado &&
                Math.Abs((a.HoraVacinacao - hora).TotalHours) < 1);  // ← Qualquer um

            if (conflito)
                throw new BusinessException("Deve haver intervalo de 1 hora entre agendamentos.");

            var novoAgendamento = new VacinaPaciente
            {
                IdPaciente = idPaciente,
                IdVacina = idVacina,
                DataVacinacao = data,
                HoraVacinacao = hora,
                Status = StatusEnum.Agendado,
                LocalVacinacao = local,
                DataCriacao = DateTime.Now
            };

            _vacinaPacienteRepositorio.Adicionar(novoAgendamento);

            return ListarAgendamentos();
        }

        public List<VacinaPacienteDTO> ListarAgendamentos()
        {
            return _vacinaPacienteRepositorio.Listar()
                .OrderBy(v => v.DataVacinacao)
                .ThenBy(v => v.HoraVacinacao)
                .Select(v => ConverterParaDTO(v))
                .ToList();
        }

        public List<VacinaPacienteDTO> ListarAgendamentosPaciente(int idPaciente)
        {
            var paciente = _pacienteRepositorio.ObterId(idPaciente);
            if (paciente == null)
                throw new BusinessException($"Paciente {idPaciente} não encontrado.");

            return _vacinaPacienteRepositorio.ListarPorPaciente(idPaciente)
                .Select(v => ConverterParaDTO(v))
                .ToList();
        }

        public List<VacinaPacienteDTO> ListarAgendamentosPorData(DateTime data)
        {
            return _vacinaPacienteRepositorio.ListarPorData(data)
                .Select(v => ConverterParaDTO(v))
                .ToList();
        }

        public VacinaPacienteDTO ObterAgendamento(int id)
        {
            var agendamento = _vacinaPacienteRepositorio.ObterId(id);
            if (agendamento == null)
                throw new BusinessException($"Agendamento {id} não encontrado.");

            return ConverterParaDTO(agendamento);
        }

        public void MarcarComoRealizado(int id, string observacoes = "")
        {
            var agendamento = _vacinaPacienteRepositorio.ObterId(id);
            if (agendamento == null)
                throw new BusinessException($"Agendamento {id} não encontrado.");

            if (agendamento.Status != StatusEnum.Agendado)
                throw new BusinessException("Apenas agendamentos com status 'Agendado' podem ser marcados como realizados.");

            agendamento.Status = StatusEnum.Realizado;
            agendamento.Observacoes = observacoes;

            _vacinaPacienteRepositorio.Atualizar(id, agendamento);
        }

        public void CancelarAgendamento(int id)
        {
            var agendamento = _vacinaPacienteRepositorio.ObterId(id);
            if (agendamento == null)
                throw new BusinessException($"Agendamento {id} não encontrado.");

            if (agendamento.Status == StatusEnum.Realizado)
                throw new BusinessException("Não é possível cancelar agendamentos já realizados.");

            agendamento.Status = StatusEnum.Cancelado;
            _vacinaPacienteRepositorio.Atualizar(id, agendamento);
        }

        public void RemoverAgendamento(int id)
        {
            var agendamento = _vacinaPacienteRepositorio.ObterId(id);
            if (agendamento == null)
                throw new BusinessException($"Agendamento {id} não encontrado.");

            if (agendamento.Status == StatusEnum.Realizado)
                throw new BusinessException("Não é possível remover agendamentos já realizados.");

            _vacinaPacienteRepositorio.Remover(id);
        }

        private VacinaPacienteDTO ConverterParaDTO(VacinaPaciente vacinaPaciente)
        {
            return new VacinaPacienteDTO
            {
                IdVacinaPaciente = vacinaPaciente.IdVacinaPaciente,
                IdPaciente = vacinaPaciente.IdPaciente,
                IdVacina = vacinaPaciente.IdVacina,
                DataVacinacao = vacinaPaciente.DataVacinacao,
                HoraVacinacao = vacinaPaciente.HoraVacinacao,
                Status = vacinaPaciente.Status.ToString(),
                LocalVacinacao = vacinaPaciente.LocalVacinacao,
                Observacoes = vacinaPaciente.Observacoes,
                DataCriacao = vacinaPaciente.DataCriacao
            };
        }
    }
}
