import { getIniciais } from "@/lib/utils/string";
import { calcularIdade, formatarDataExibicao } from "@/lib/utils/date";
import type { Paciente } from "@/types";

interface PacienteItemProps {
  paciente: Paciente;
  onRemover: () => void;
}

export function PacienteItem({ paciente, onRemover }: PacienteItemProps) {
  return (
    <div className="flex items-center gap-3 px-3 py-[10px] rounded-[8px] border-[0.5px] border-[#e5e8e3]">
      <div className="w-[34px] h-[34px] rounded-full bg-[#CDE9CA] text-[#575E55] text-[13px] font-semibold flex items-center justify-center flex-shrink-0">
        {getIniciais(paciente.nome)}
      </div>
      <div className="flex-1">
        <div className="text-[13px] font-medium text-[#575E55]">{paciente.nome}</div>
        <div className="text-[11px] text-[#aaa] mt-[1px]">
          Nascido em {formatarDataExibicao(paciente.dataNascimento)} · {calcularIdade(paciente.dataNascimento)} anos
        </div>
      </div>
      <div className="flex gap-2">
        <button className="text-[11px] px-[10px] py-[4px] rounded-[6px] border-[0.5px] border-[#e0e4dd] text-[#666] bg-white cursor-pointer">
          Editar
        </button>
        <button
          onClick={onRemover}
          className="text-[11px] px-[10px] py-[4px] rounded-[6px] border-[0.5px] border-[#f5c6c6] text-[#c0392b] bg-[#fff5f5] cursor-pointer"
        >
          Remover
        </button>
      </div>
    </div>
  );
}
