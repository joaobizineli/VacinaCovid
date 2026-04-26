import { API_URL } from "./api";
import type { Paciente, CriarPacienteForm } from "@/types";

export const pacienteService = {
  async listar(): Promise<Paciente[]> {
    const res = await fetch(`${API_URL}/Pacientes/ListarPacientes`);
    if (!res.ok) throw new Error("Erro ao listar pacientes");
    const json = await res.json();
    return json ?? [];
  },

  async criar(form: CriarPacienteForm): Promise<Paciente> {
  const res = await fetch(`${API_URL}/Pacientes/InserirPaciente`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      nome: form.nome,
      dataNascimento: `${form.dataNascimento}T00:00:00`,
    }),
  });
  if (!res.ok) {
  const err = await res.json();
  throw new Error(err.Messages?.[0] ?? "Erro ao cadastrar paciente");
}
  return res.json();
},

  async remover(id: number): Promise<void> {
    const res = await fetch(`${API_URL}/Pacientes/RemoverPaciente?id=${id}`, {
      method: "DELETE",
    });
    if (!res.ok) throw new Error("Erro ao remover paciente");
  },
};
