import { useAgendamentoStore } from "@/store/agendamentoStore";
import { usePacienteStore } from "@/store/pacienteStore";
import { formatarHora, formatarDataExibicao } from "@/lib/utils/date";
import { BaseCard } from "@/components/shared/BaseCard";
import type { Agendamento } from "@/types";
import { useShallow } from "zustand/react/shallow";

export function AgendamentoLista() {
  const diaAtual = useAgendamentoStore((s) => s.diaAtual);
  const agendamentos = useAgendamentoStore(
    useShallow((s) =>
      s.agendamentos.filter((a) => a.dataVacinacao.slice(0, 10) === diaAtual),
    ),
  );
  const { diaAnterior, proximoDia, marcarRealizado, cancelarAgendamento } =
    useAgendamentoStore(
      useShallow((s) => ({
        diaAnterior: s.diaAnterior,
        proximoDia: s.proximoDia,
        marcarRealizado: s.marcarRealizado,
        cancelarAgendamento: s.cancelarAgendamento,
      })),
    );
  const pacientes = usePacienteStore((s) => s.pacientes);

  const manha = agendamentos.filter((a) => parseInt(a.horaVacinacao) < 12);
  const tarde = agendamentos.filter((a) => parseInt(a.horaVacinacao) >= 12);

  function getNomePaciente(id: number): string {
    return (
      pacientes.find((p) => p.idPaciente === id)?.nome ?? `Paciente #${id}`
    );
  }

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

      <div className="flex flex-col gap-3 overflow-y-auto max-h-[430px]">
        {manha.length > 0 && (
          <>
            <p className="text-[11px] text-[#bbb] uppercase tracking-[0.6px]">
              Manhã
            </p>
            <div className="flex flex-col gap-[7px]">
              {manha.map((a) => (
                <Item
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
                <Item
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
      </div>
    </BaseCard>
  );
}

const statusCls: Record<string, string> = {
  Agendado: "bg-[#e6f4e4] text-[#3a6e38]",
  Realizado: "bg-[#fdebd4] text-[#8a4510]",
  Cancelado: "bg-[#eee] text-[#777]",
};

function Item({
  agendamento,
  nomePaciente,
  onRealizado,
  onCancelar,
}: {
  agendamento: Agendamento;
  nomePaciente: string;
  onRealizado: () => void;
  onCancelar: () => void;
}) {
  return (
    <div className="flex items-center gap-[10px] px-3 py-[9px] rounded-[8px] border-[0.5px] border-[#e5e8e3]">
      <span className="bg-[#CDE9CA] text-[#575E55] text-[12px] font-semibold px-[10px] py-1 rounded-[6px] min-w-[52px] text-center">
        {formatarHora(agendamento.horaVacinacao)}
      </span>
      <div className="flex-1">
        <div className="text-[13px] font-medium text-[#575E55]">
          {nomePaciente}
        </div>
        <div className="text-[11px] text-[#aaa] mt-[1px]">
          {agendamento.localVacinacao}
        </div>
      </div>
      <span
        className={`text-[11px] px-[9px] py-[3px] rounded-full ${statusCls[agendamento.status]}`}
      >
        {agendamento.status}
      </span>
      {agendamento.status === "Agendado" && (
        <div className="flex gap-1">
          <button
            onClick={onRealizado}
            className="text-[10px] px-[8px] py-[3px] rounded-[5px] border border-[#c3e6cb] text-[#3a6e38] bg-[#e6f4e4] cursor-pointer"
          >
            ✓
          </button>
          <button
            onClick={onCancelar}
            className="text-[10px] px-[8px] py-[3px] rounded-[5px] border border-[#f5c6c6] text-[#c0392b] bg-[#fff5f5] cursor-pointer"
          >
            ✕
          </button>
        </div>
      )}
    </div>
  );
}
