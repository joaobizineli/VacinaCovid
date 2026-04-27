import { AgendamentoItem } from "@/components/agendamento/AgendamentoItem";
import type { Agendamento } from "@/types";
import { Skeleton } from "@/components/ui/skeleton";

interface Props {
  agendamentos: Agendamento[];
  getNomePaciente: (id: number) => string;
  onRealizado: (id: number) => void;
  onCancelar: (id: number) => void;
  loading?: boolean;
}

export function ConsultaLista({
  agendamentos,
  getNomePaciente,
  onRealizado,
  onCancelar,
  loading,
}: Props) {
  if (loading)
    return (
      <div className="flex flex-col gap-2 mt-4">
        {Array.from({ length: 3 }).map((_, i) => (
          <Skeleton key={i} className="h-[46px] w-full rounded-[8px]" />
        ))}
      </div>
    );
  if (agendamentos.length === 0)
    return (
      <p className="text-[13px] text-[#aaa] text-center mt-6">
        Nenhum agendamento encontrado.
      </p>
    );

  return (
    <div className="flex flex-col gap-2 mt-4 overflow-y-auto max-h-[400px]">
      {agendamentos.map((a) => (
        <AgendamentoItem
          key={a.idVacinaPaciente}
          agendamento={a}
          nomePaciente={getNomePaciente(a.idPaciente)}
          onRealizado={() => onRealizado(a.idVacinaPaciente)}
          onCancelar={() => onCancelar(a.idVacinaPaciente)}
          mostrarData
        />
      ))}
    </div>
  );
}
