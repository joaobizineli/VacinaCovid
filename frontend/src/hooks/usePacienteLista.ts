import { useState, useEffect } from "react";
import { usePacienteStore } from "@/store/pacienteStore";
import { pacienteService } from "@/services/api/pacienteService";

export function usePacienteLista() {
  const pacientes = usePacienteStore((s) => s.pacientes);
  const setPacientes = usePacienteStore((s) => s.setPacientes);
  const [busca, setBusca] = useState("");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    pacienteService.listar()
      .then(setPacientes)
      .finally(() => setLoading(false));
  }, []);

  async function removerPaciente(id: number) {
    await pacienteService.remover(id);
    setPacientes(pacientes.filter((p) => p.idPaciente !== id));
  }

  const filtrados = pacientes.filter((p) =>
    p.nome.toLowerCase().includes(busca.toLowerCase())
  );

  return { pacientes, filtrados, busca, setBusca, loading, removerPaciente };
}
