import { useAgendamentoStore } from "@/store/agendamentoStore";
import { usePacienteStore } from "@/store/pacienteStore";
import { useShallow } from "zustand/react/shallow";

export function useAgendamentoLista() {
  const diaAtual = useAgendamentoStore((s) => s.diaAtual);
  const agendamentos = useAgendamentoStore(
    useShallow((s) =>
      s.agendamentos.filter((a) => a.dataVacinacao.slice(0, 10) === diaAtual)
    )
  );
  const { diaAnterior, proximoDia, marcarRealizado, cancelarAgendamento } =
    useAgendamentoStore(
      useShallow((s) => ({
        diaAnterior: s.diaAnterior,
        proximoDia: s.proximoDia,
        marcarRealizado: s.marcarRealizado,
        cancelarAgendamento: s.cancelarAgendamento,
      }))
    );
  const pacientes = usePacienteStore((s) => s.pacientes);

  function getNomePaciente(id: number): string {
    return pacientes.find((p) => p.idPaciente === id)?.nome ?? `Paciente #${id}`;
  }

  return {
    diaAtual,
    agendamentos,
    diaAnterior,
    proximoDia,
    marcarRealizado,
    cancelarAgendamento,
    getNomePaciente,
  };
}