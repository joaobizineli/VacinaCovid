import { create } from "zustand";
import { persist } from "zustand/middleware";
import type { Paciente } from "@/types";

interface PacienteStore {
  pacientes: Paciente[];
  setPacientes: (pacientes: Paciente[]) => void;
}

export const usePacienteStore = create<PacienteStore>()(
  persist(
    (set) => ({
      pacientes: [],
      setPacientes: (pacientes) => set({ pacientes }),
    }),
    { name: "pacientes" }
  )
);
