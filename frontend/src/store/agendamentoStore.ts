import { create } from "zustand";
import { persist } from "zustand/middleware";
import type { Agendamento } from "@/types";
import { hoje } from "@/lib/utils/date";

interface AgendamentoStore {
  agendamentos: Agendamento[];
  diaAtual: string;

  diaAnterior: () => void;
  proximoDia: () => void;

  setAgendamentos: (agendamentos: Agendamento[]) => void;
}

export const useAgendamentoStore = create<AgendamentoStore>()(
  persist(
    (set) => ({
      agendamentos: [],
      diaAtual: hoje(),

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

      setAgendamentos: (agendamentos) =>
        set({ agendamentos: Array.isArray(agendamentos) ? agendamentos : [] }),
    }),
    {
      name: "agendamentos",
      partialize: (state) => ({ agendamentos: state.agendamentos }),
    },
  ),
);
