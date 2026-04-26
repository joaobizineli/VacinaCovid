import { API_URL } from "./api";
import type { Agendamento, CriarAgendamentoForm } from "@/types";

export const agendamentoService = {
  async listar(): Promise<Agendamento[]> {
    const res = await fetch(`${API_URL}/VacinaPacientes/ListarAgendamentos`);
     if (!res.ok) throw new Error("Erro ao listar agendamentos");
    const json = await res.json();
    return json ?? [];
  },

  async agendar(form: CriarAgendamentoForm): Promise<Agendamento> {
    const res = await fetch(`${API_URL}/VacinaPacientes/AgendarVacina`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        idPaciente: form.idPaciente,
        idVacina: form.idVacina,
        dataVacinacao: new Date(form.dataVacinacao).toISOString(),
        horaVacinacao: `${form.horaVacinacao}:00`,
        localVacinacao: form.localVacinacao,
      }),
    });
    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.Messages?.[0] ?? "Erro ao agendar");
    }
    return res.json();
  },

  async marcarRealizado(id: number): Promise<void> {
    const res = await fetch(`${API_URL}/VacinaPacientes/MarcarComoRealizado?id=${id}`, {
      method: "PUT",
    });
    if (!res.ok) throw new Error("Erro ao marcar como realizado");
  },

  async cancelar(id: number): Promise<void> {
    const res = await fetch(`${API_URL}/VacinaPacientes/CancelarAgendamento?id=${id}`, {
      method: "PUT",
    });
    if (!res.ok) throw new Error("Erro ao cancelar agendamento");
  },
};
