import { API_URL } from "./api";
import type { Paciente, CriarPacienteForm } from "@/types";

export const pacienteService = {
  async listar(): Promise<Paciente[]> {
    const res = await fetch(`${API_URL}/Pacientes/ListarPacientes`);
    const json = await res.json();
    return json ?? [];
  },

  async criar(form: CriarPacienteForm): Promise<Paciente> {
    const params = new URLSearchParams({
      nome: form.nome,
      dataNascimento: `${form.dataNascimento}T00:00:00`,
    });
    const res = await fetch(`${API_URL}/Pacientes/InserirPaciente?${params}`, {
      method: "POST",
    });
    if (!res.ok) throw new Error("Erro ao cadastrar paciente");
    return res.json();
  },

  async remover(id: number): Promise<void> {
    await fetch(`${API_URL}/Pacientes/RemoverPaciente?id=${id}`, {
      method: "DELETE",
    });
  },
};
