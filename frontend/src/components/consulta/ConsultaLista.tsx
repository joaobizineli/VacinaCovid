import { AgendamentoItem } from "@/components/agendamento/AgendamentoItem";
import type { Agendamento } from "@/types";

interface Props {
  agendamentos: Agendamento[];
  getNomePaciente: (id: number) => string;
}

export function ConsultaLista({ agendamentos, getNomePaciente }: Props) {
  if (agendamentos.length === 0)
    return <p className="text-[13px] text-[#aaa] text-center mt-6">Nenhum agendamento encontrado.</p>;

  return (
    <div className="flex flex-col gap-2 mt-4 overflow-y-auto max-h-[400px]">
      {agendamentos.map((a) => (
        <AgendamentoItem
          key={a.idVacinaPaciente}
          agendamento={a}
          nomePaciente={getNomePaciente(a.idPaciente)}
          onRealizado={() => {}}
          onCancelar={() => {}}
        />
      ))}
    </div>
  );
}
