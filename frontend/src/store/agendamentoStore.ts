import { create } from "zustand";
import { persist } from "zustand/middleware";
import type {
  Agendamento,
  CriarAgendamentoForm,
  StatusAgendamento,
} from "@/types";
import { hoje, horaParaBackend } from "@/lib/utils/date";

// Regras de negócio — definidas como constantes pra ficarem explícitas
const LIMITE_POR_DIA = 20;
const LIMITE_POR_HORARIO = 2;

interface AgendamentoStore {
  agendamentos: Agendamento[];
  diaAtual: string;

  diaAnterior: () => void;
  proximoDia: () => void;
  selecionarDia: (data: string) => void;

  adicionarAgendamento: (form: CriarAgendamentoForm) => {
    sucesso: boolean;
    erro?: string;
  };
  marcarRealizado: (id: number, observacoes?: string) => void;
  cancelarAgendamento: (id: number) => void;

  agendamentosDoDia: (data: string) => Agendamento[];
  totalDoDia: (data: string) => number;
  totalPorHorario: (data: string, hora: string) => number;
}

export const useAgendamentoStore = create<AgendamentoStore>()(
  persist(
    (set, get) => ({
      diaAtual: hoje(),

      agendamentos: [
        {
          idVacinaPaciente: 1,
          idPaciente: 1,
          idVacina: 1,
          dataVacinacao: "2026-04-20T00:00:00",
          horaVacinacao: "08:00:00",
          status: "Agendado",
          localVacinacao: "UBS Centro",
          observacoes: "",
          dataCriacao: "2026-04-20T00:00:00",
        },
        {
          idVacinaPaciente: 2,
          idPaciente: 2,
          idVacina: 2,
          dataVacinacao: "2026-04-21T00:00:00",
          horaVacinacao: "09:00:00",
          status: "Agendado",
          localVacinacao: "UBS Norte",
          observacoes: "",
          dataCriacao: "2026-04-20T00:00:00",
        },
        {
          idVacinaPaciente: 3,
          idPaciente: 3,
          idVacina: 1,
          dataVacinacao: "2026-04-22T00:00:00",
          horaVacinacao: "10:00:00",
          status: "Realizado",
          localVacinacao: "UBS Centro",
          observacoes: "",
          dataCriacao: "2026-04-20T00:00:00",
        },
        {
          idVacinaPaciente: 4,
          idPaciente: 4,
          idVacina: 3,
          dataVacinacao: "2026-04-22T00:00:00",
          horaVacinacao: "14:00:00",
          status: "Agendado",
          localVacinacao: "UBS Sul",
          observacoes: "",
          dataCriacao: "2026-04-20T00:00:00",
        },
        {
          idVacinaPaciente: 5,
          idPaciente: 5,
          idVacina: 2,
          dataVacinacao: "2026-04-22T00:00:00",
          horaVacinacao: "15:00:00",
          status: "Cancelado",
          localVacinacao: "UBS Centro",
          observacoes: "",
          dataCriacao: "2026-04-20T00:00:00",
        },
      ],

      diaAnterior: () =>
        set((state) => ({
          diaAtual: new Date(new Date(state.diaAtual).getTime() - 86400000)
            .toISOString()
            .slice(0, 10),
        })),

      proximoDia: () =>
        set((state) => ({
          diaAtual: new Date(new Date(state.diaAtual).getTime() + 86400000)
            .toISOString()
            .slice(0, 10),
        })),

      selecionarDia: (data) => set({ diaAtual: data }),

      adicionarAgendamento: (form) => {
        const state = get();
        const data = form.dataVacinacao;
        const hora = horaParaBackend(form.horaVacinacao);

        if (state.totalDoDia(data) >= LIMITE_POR_DIA) {
          return {
            sucesso: false,
            erro: "Limite de 20 agendamentos por dia atingido.",
          };
        }

        if (state.totalPorHorario(data, hora) >= LIMITE_POR_HORARIO) {
          return {
            sucesso: false,
            erro: "Limite de 2 agendamentos por horário atingido.",
          };
        }
        const horaEmMinutos = (h: string) => {
          const [hh, mm] = h.split(":").map(Number);
          return hh * 60 + mm;
        };

        const novoHorario = horaEmMinutos(hora);
        const conflito = state.agendamentosDoDia(data).some((a) => {
          if (a.status === "Cancelado") return false;
          const diff = Math.abs(horaEmMinutos(a.horaVacinacao) - novoHorario);
          return diff > 0 && diff < 60;
        });

        if (conflito) {
          return {
            sucesso: false,
            erro: "Intervalo mínimo de 1 hora entre agendamentos.",
          };
        }

        set((state) => ({
          agendamentos: [
            ...state.agendamentos,
            {
              idVacinaPaciente: state.agendamentos.length + 1,
              idPaciente: form.idPaciente,
              idVacina: form.idVacina,
              dataVacinacao: `${data}T00:00:00`,
              horaVacinacao: hora,
              status: "Agendado" as StatusAgendamento,
              localVacinacao: form.localVacinacao,
              observacoes: "",
              dataCriacao: new Date().toISOString(),
            },
          ],
        }));

        return { sucesso: true };
      },

      marcarRealizado: (id, observacoes = "") =>
        set((state) => ({
          agendamentos: state.agendamentos.map((a) =>
            a.idVacinaPaciente === id
              ? { ...a, status: "Realizado" as StatusAgendamento, observacoes }
              : a,
          ),
        })),

      cancelarAgendamento: (id) =>
        set((state) => ({
          agendamentos: state.agendamentos.map((a) =>
            a.idVacinaPaciente === id
              ? { ...a, status: "Cancelado" as StatusAgendamento }
              : a,
          ),
        })),

      agendamentosDoDia: (data) =>
        get().agendamentos.filter((a) => a.dataVacinacao.slice(0, 10) === data),

      totalDoDia: (data) => get().agendamentosDoDia(data).length,

      totalPorHorario: (data, hora) =>
        get()
          .agendamentosDoDia(data)
          .filter((a) => a.horaVacinacao === hora && a.status === "Agendado")
          .length,
    }),
    { name: "agendamentos" },
  ),
);
