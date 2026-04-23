import { BaseCard } from "@/components/shared/BaseCard";
import { Input } from "@/components/ui/input";
import { usePacienteStore } from "@/store/pacienteStore";
import { calcularIdade, formatarDataExibicao } from "@/lib/utils/date";
import { useState } from "react";

function getIniciais(nome: string) {
  return nome
    .split(" ")
    .slice(0, 2)
    .map((n) => n[0])
    .join("")
    .toUpperCase();
}

export function PacienteLista() {
  const pacientes = usePacienteStore((s) => s.pacientes);
  const removerPaciente = usePacienteStore((s) => s.removerPaciente);
  const [busca, setBusca] = useState("");

  const filtrados = pacientes.filter((p) =>
    p.nome.toLowerCase().includes(busca.toLowerCase()),
  );

  return (
    <BaseCard
      title="Pacientes cadastrados"
      subtitle={`${pacientes.length} pacientes`}
    >
      <div className="flex flex-col gap-2 overflow-y-auto max-h-[500px]">
        <Input
          placeholder="Buscar paciente..."
          className="h-10"
          value={busca}
          onChange={(e) => setBusca(e.target.value)}
        />
        <div className="flex flex-col gap-2 overflow-y-auto max-h-[calc(100vh-280px)]">
          {filtrados.map((p) => (
            <div
              key={p.idPaciente}
              className="flex items-center gap-3 px-3 py-[10px] rounded-[8px] border-[0.5px] border-[#e5e8e3]"
            >
              <div className="w-[34px] h-[34px] rounded-full bg-[#CDE9CA] text-[#575E55] text-[13px] font-semibold flex items-center justify-center flex-shrink-0">
                {getIniciais(p.nome)}
              </div>
              <div className="flex-1">
                <div className="text-[13px] font-medium text-[#575E55]">
                  {p.nome}
                </div>
                <div className="text-[11px] text-[#aaa] mt-[1px]">
                  Nascido em {formatarDataExibicao(p.dataNascimento)} ·{" "}
                  {calcularIdade(p.dataNascimento)} anos
                </div>
              </div>
              <div className="flex gap-2">
                <button className="text-[11px] px-[10px] py-[4px] rounded-[6px] border-[0.5px] border-[#e0e4dd] text-[#666] bg-white cursor-pointer">
                  Editar
                </button>
                <button
                  onClick={() => removerPaciente(p.idPaciente)}
                  className="text-[11px] px-[10px] py-[4px] rounded-[6px] border-[0.5px] border-[#f5c6c6] text-[#c0392b] bg-[#fff5f5] cursor-pointer"
                >
                  Remover
                </button>
              </div>
            </div>
          ))}
          {filtrados.length === 0 && (
            <p className="text-[13px] text-[#aaa] text-center mt-4">
              Nenhum paciente encontrado.
            </p>
          )}
        </div>
      </div>
    </BaseCard>
  );
}
