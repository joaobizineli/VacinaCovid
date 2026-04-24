import { BaseCard } from "@/components/shared/BaseCard";
import { Input } from "@/components/ui/input";
import { PacienteItem } from "./PacienteItem";
import { usePacienteLista } from "@/hooks/usePacienteLista";

export function PacienteLista() {
  const { pacientes, filtrados, busca, setBusca, removerPaciente } = usePacienteLista();

  return (
    <BaseCard title="Pacientes cadastrados" subtitle={`${pacientes.length} pacientes`}>
      <div className="flex flex-col gap-2 overflow-y-auto max-h-[500px]">
        <Input
          placeholder="Buscar paciente..."
          className="h-10"
          value={busca}
          onChange={(e) => setBusca(e.target.value)}
        />
        <div className="flex flex-col gap-2 overflow-y-auto max-h-[calc(100vh-280px)]">
          {filtrados.map((p) => (
            <PacienteItem
              key={p.idPaciente}
              paciente={p}
              onRemover={() => removerPaciente(p.idPaciente)}
            />
          ))}
          {filtrados.length === 0 && (
            <p className="text-[13px] text-[#aaa] text-center mt-4">Nenhum paciente encontrado.</p>
          )}
        </div>
      </div>
    </BaseCard>
  );
}
