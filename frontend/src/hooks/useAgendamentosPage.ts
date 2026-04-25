import { useAgendamentoStore } from "@/store/agendamentoStore";
import { useShallow } from "zustand/react/shallow";

export function useAgendamentosPage() {
  const diaAtual = useAgendamentoStore((s) => s.diaAtual);
  const agendamentos = useAgendamentoStore(
    useShallow((s) =>
      (Array.isArray(s.agendamentos) ? s.agendamentos : []).filter(
        (a) => a.dataVacinacao.slice(0, 10) === diaAtual
      )
    )
  );

  return {
    total: agendamentos.length,
    agendados: agendamentos.filter((a) => a.status === "Agendado").length,
    realizados: agendamentos.filter((a) => a.status === "Realizado").length,
    cancelados: agendamentos.filter((a) => a.status === "Cancelado").length,
  };
}
