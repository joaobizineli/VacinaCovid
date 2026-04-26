import { BaseCard } from "@/components/shared/BaseCard";
import { Input } from "@/components/ui/input";
import { PacienteItem } from "./PacienteItem";
import { Skeleton } from "@/components/ui/skeleton";
import { usePacienteLista } from "@/hooks/usePacienteLista";

export function PacienteLista() {
  const { pacientes, filtrados, busca, setBusca, loading, removerPaciente } = usePacienteLista();

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
          {loading ? (
            Array.from({ length: 4 }).map((_, i) => (
              <div
                key={i}
                className="flex items-center gap-3 px-3 py-[10px] rounded-[8px] border-[0.5px] border-[#e5e8e3]"
              >
                <Skeleton className="w-[34px] h-[34px] rounded-full" />
                <div className="flex-1 flex flex-col gap-2">
                  <Skeleton className="h-[13px] w-[140px]" />
                  <Skeleton className="h-[11px] w-[100px]" />
                </div>
                <Skeleton className="h-[26px] w-[50px] rounded-[6px]" />
                <Skeleton className="h-[26px] w-[60px] rounded-[6px]" />
              </div>
            ))
          ) : (
            <>
              {filtrados.map((p) => (
                <PacienteItem
                  key={p.idPaciente}
                  paciente={p}
                  onRemover={() => removerPaciente(p.idPaciente)}
                />
              ))}
              {filtrados.length === 0 && (
                <p className="text-[13px] text-[#aaa] text-center mt-4">
                  Nenhum paciente encontrado.
                </p>
              )}
            </>
          )}
        </div>
      </div>
    </BaseCard>
  );
}
