import { useState, useEffect } from "react";
import { useAgendamentoStore } from "@/store/agendamentoStore";
import { usePacienteStore } from "@/store/pacienteStore";
import { useShallow } from "zustand/react/shallow";
import { agendamentoService } from "@/services/api/agendamentoService";
import { pacienteService } from "@/services/api/pacienteService";

export function useAgendamentoLista() {
  const diaAtual = useAgendamentoStore((s) => s.diaAtual);
  const setAgendamentos = useAgendamentoStore((s) => s.setAgendamentos);
  const agendamentos = useAgendamentoStore(
    useShallow((s) =>
      (Array.isArray(s.agendamentos) ? s.agendamentos : []).filter(
        (a) => a.dataVacinacao.slice(0, 10) === diaAtual
      )
    )
  );
  const { diaAnterior, proximoDia } = useAgendamentoStore(
    useShallow((s) => ({ diaAnterior: s.diaAnterior, proximoDia: s.proximoDia }))
  );
  const pacientes = usePacienteStore((s) => s.pacientes);
  const setPacientes = usePacienteStore((s) => s.setPacientes);  // ← adiciona essa
  const [loading, setLoading] = useState(true);

   useEffect(() => {
    agendamentoService.listar()
      .then(setAgendamentos)
      .finally(() => setLoading(false));
    
    if (pacientes.length === 0) {
      pacienteService.listar().then(setPacientes);
    }
  }, []);

  

  async function marcarRealizado(id: number) {
    await agendamentoService.marcarRealizado(id);
    const lista = await agendamentoService.listar();
    setAgendamentos(lista);
  }

  async function cancelarAgendamento(id: number) {
    await agendamentoService.cancelar(id);
    const lista = await agendamentoService.listar();
    setAgendamentos(lista);
  }

  function getNomePaciente(id: number): string {
    return (pacientes ?? []).find((p) => p.idPaciente === id)?.nome ?? `Paciente #${id}`;
  }

  return {
    diaAtual, agendamentos, diaAnterior, proximoDia,
    marcarRealizado, cancelarAgendamento, getNomePaciente, loading,
  };
}
