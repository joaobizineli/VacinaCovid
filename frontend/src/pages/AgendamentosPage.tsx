import { StatsBar } from "@/components/agendamento/StatsBar";
import { AgendamentoForm } from "@/components/agendamento/AgendamentoForm";
import { AgendamentoLista } from "@/components/agendamento/AgendamentoLista";
import { useAgendamentosPage } from "@/hooks/useAgendamentosPage";

export function AgendamentosPage() {
  const { total, agendados, realizados, cancelados } = useAgendamentosPage();

  return (
    <div className="p-4 pt-6">
      <h1 className="text-[17px] font-semibold text-[#575E55] mb-1">Agendamentos</h1>
      <p className="text-[13px] text-[#999] mb-[10px]">Gerencie os agendamentos de vacinação</p>
      <StatsBar total={total} agendados={agendados} realizados={realizados} cancelados={cancelados} />
    <div className="grid grid-cols-2 gap-[14px] h-[600px]">
      <AgendamentoForm />
      <AgendamentoLista />
    </div>
    </div>
  );
}
