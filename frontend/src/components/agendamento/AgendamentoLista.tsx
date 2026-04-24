import { formatarDataExibicao } from "@/lib/utils/date";
import { BaseCard } from "@/components/shared/BaseCard";
import { useAgendamentoLista } from "@/hooks/useAgendamentoLista";
import { AgendamentoItem } from "./AgendamentoItem";

export function AgendamentoLista() {
  const {
    diaAtual, agendamentos, diaAnterior, proximoDia,
    marcarRealizado, cancelarAgendamento, getNomePaciente,
  } = useAgendamentoLista();

  const manha = agendamentos
    .filter((a) => parseInt(a.horaVacinacao) < 12)
    .sort((a, b) => a.horaVacinacao.localeCompare(b.horaVacinacao));

  const tarde = agendamentos
    .filter((a) => parseInt(a.horaVacinacao) >= 12)
    .sort((a, b) => a.horaVacinacao.localeCompare(b.horaVacinacao));

  return (
    <BaseCard title="Agendamentos do dia" subtitle={formatarDataExibicao(diaAtual)} className="h-full">
      <div className="flex items-center justify-between mb-3">
        <button onClick={diaAnterior} className="text-[12px] px-3 py-1 rounded-md border border-[#e5e8e3] text-[#666]">
          ← anterior
        </button>
        <button onClick={proximoDia} className="text-[12px] px-3 py-1 rounded-md border border-[#e5e8e3] text-[#666]">
          próximo →
        </button>
      </div>

      <div className="flex flex-col gap-3 overflow-y-auto max-h-[430px]">
        {manha.length > 0 && (
          <>
            <p className="text-[11px] text-[#bbb] uppercase tracking-[0.6px]">Manhã</p>
            <div className="flex flex-col gap-[7px]">
              {manha.map((a) => (
                <AgendamentoItem
                  key={a.idVacinaPaciente}
                  agendamento={a}
                  nomePaciente={getNomePaciente(a.idPaciente)}
                  onRealizado={() => marcarRealizado(a.idVacinaPaciente)}
                  onCancelar={() => cancelarAgendamento(a.idVacinaPaciente)}
                />
              ))}
            </div>
          </>
        )}

        {tarde.length > 0 && (
          <>
            <p className="text-[11px] text-[#bbb] uppercase tracking-[0.6px]">Tarde</p>
            <div className="flex flex-col gap-[7px]">
              {tarde.map((a) => (
                <AgendamentoItem
                  key={a.idVacinaPaciente}
                  agendamento={a}
                  nomePaciente={getNomePaciente(a.idPaciente)}
                  onRealizado={() => marcarRealizado(a.idVacinaPaciente)}
                  onCancelar={() => cancelarAgendamento(a.idVacinaPaciente)}
                />
              ))}
            </div>
          </>
        )}

        {agendamentos.length === 0 && (
          <p className="text-[13px] text-[#aaa] text-center mt-4">Nenhum agendamento para este dia.</p>
        )}
      </div>
    </BaseCard>
  );
}
