import { useState } from "react";
import { usePacienteStore } from "@/store/pacienteStore";

export function usePacienteLista() {
  const pacientes = usePacienteStore((s) => s.pacientes);
  const removerPaciente = usePacienteStore((s) => s.removerPaciente);
  const [busca, setBusca] = useState("");

  const filtrados = pacientes.filter((p) =>
    p.nome.toLowerCase().includes(busca.toLowerCase())
  );

  return { pacientes, filtrados, busca, setBusca, removerPaciente };
}
