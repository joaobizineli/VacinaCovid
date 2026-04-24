import { formatarHora } from "@/lib/utils/date";
import type { Agendamento } from "@/types";

const statusCls: Record<string, string> = {
  Agendado: "bg-[#e6f4e4] text-[#3a6e38]",
  Realizado: "bg-[#fdebd4] text-[#8a4510]",
  Cancelado: "bg-[#eee] text-[#777]",
};

interface AgendamentoItemProps {
  agendamento: Agendamento;
  nomePaciente: string;
  onRealizado: () => void;
  onCancelar: () => void;
}

export function AgendamentoItem({ agendamento, nomePaciente, onRealizado, onCancelar }: AgendamentoItemProps) {
  return (
    <div className="flex items-center gap-[10px] px-3 py-[9px] rounded-[8px] border-[0.5px] border-[#e5e8e3]">
      <span className="bg-[#CDE9CA] text-[#575E55] text-[12px] font-semibold px-[10px] py-1 rounded-[6px] min-w-[52px] text-center">
        {formatarHora(agendamento.horaVacinacao)}
      </span>
      <div className="flex-1">
        <div className="text-[13px] font-medium text-[#575E55]">{nomePaciente}</div>
        <div className="text-[11px] text-[#aaa] mt-[1px]">{agendamento.localVacinacao}</div>
      </div>
      <span className={`text-[11px] px-[9px] py-[3px] rounded-full ${statusCls[agendamento.status]}`}>
        {agendamento.status}
      </span>
      {agendamento.status === "Agendado" && (
        <div className="flex gap-1">
          <button onClick={onRealizado} className="text-[10px] px-[8px] py-[3px] rounded-[5px] border border-[#c3e6cb] text-[#3a6e38] bg-[#e6f4e4] cursor-pointer">
            ✓
          </button>
          <button onClick={onCancelar} className="text-[10px] px-[8px] py-[3px] rounded-[5px] border border-[#f5c6c6] text-[#c0392b] bg-[#fff5f5] cursor-pointer">
            ✕
          </button>
        </div>
      )}
    </div>
  );
}