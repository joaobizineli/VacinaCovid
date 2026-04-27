import { formatarDataExibicao } from "@/lib/utils/date";
import { BaseCard } from "@/components/shared/BaseCard";
import { useAgendamentoLista } from "@/hooks/useAgendamentoLista";
import { AgendamentoItem } from "./AgendamentoItem";
import { Skeleton } from "@/components/ui/skeleton";

export function AgendamentoLista() {
  const {
    diaAtual,
    agendamentos,
    diaAnterior,
    proximoDia,
    marcarRealizado,
    cancelarAgendamento,
    getNomePaciente,
    loading,
    erro,
  } = useAgendamentoLista();

const manha = agendamentos
  .filter((a) => Number(a.horaVacinacao.slice(0, 2)) < 12)
  .sort((a, b) => a.horaVacinacao.localeCompare(b.horaVacinacao));

const tarde = agendamentos
  .filter((a) => Number(a.horaVacinacao.slice(0, 2)) >= 12)
  .sort((a, b) => a.horaVacinacao.localeCompare(b.horaVacinacao));

  return (
    <BaseCard
      title="Agendamentos do dia"
      subtitle={formatarDataExibicao(diaAtual)}
      className="h-full"
    >
      <div className="flex items-center justify-between mb-3">
        <button
          onClick={diaAnterior}
          className="text-[12px] px-3 py-1 rounded-md border border-[#e5e8e3] text-[#666]"
        >
          ← anterior
        </button>
        <button
          onClick={proximoDia}
          className="text-[12px] px-3 py-1 rounded-md border border-[#e5e8e3] text-[#666]"
        >
          próximo →
        </button>
      </div>
              {erro && <p className="text-[12px] text-red-500 text-center">{erro}</p>}
      <div className="flex flex-col gap-3 overflow-y-auto max-h-[400px]">
        {loading ? (
          Array.from({ length: 3 }).map((_, i) => (
            <div
              key={i}
              className="flex items-center gap-[10px] px-3 py-[9px] rounded-[8px] border-[0.5px] border-[#e5e8e3]"
            >
              <Skeleton className="h-[28px] w-[52px] rounded-[6px]" />
              <div className="flex-1 flex flex-col gap-2">
                <Skeleton className="h-[13px] w-[120px]" />
                <Skeleton className="h-[11px] w-[80px]" />
              </div>
              <Skeleton className="h-[22px] w-[70px] rounded-full" />
            </div>
          ))
        ) : (
          <>
            {manha.length > 0 && (
              <>
                <p className="text-[11px] text-[#bbb] uppercase tracking-[0.6px]">
                  Manhã
                </p>
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
                <p className="text-[11px] text-[#bbb] uppercase tracking-[0.6px]">
                  Tarde
                </p>
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
              <p className="text-[13px] text-[#aaa] text-center mt-4">
                Nenhum agendamento para este dia.
              </p>
            )}
          </>
        )}
      </div>
    </BaseCard>
  );
}
