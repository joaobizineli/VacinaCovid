import { create } from "zustand";
import { persist } from "zustand/middleware";
import type { Paciente, CriarPacienteForm } from "@/types";

interface PacienteStore {
  pacientes: Paciente[];
  adicionarPaciente: (form: CriarPacienteForm) => void;
  removerPaciente: (id: number) => void;
}

export const usePacienteStore = create<PacienteStore>()(
  persist(
    (set) => ({
      pacientes: [
        { idPaciente: 1, nome: "João Silva", dataNascimento: "1990-05-15T00:00:00" },
        { idPaciente: 2, nome: "Maria Oliveira", dataNascimento: "1985-08-22T00:00:00" },
        { idPaciente: 3, nome: "Carlos Santos", dataNascimento: "1978-03-10T00:00:00" },
        { idPaciente: 4, nome: "Ana Souza", dataNascimento: "1995-11-05T00:00:00" },
        { idPaciente: 5, nome: "Pedro Lima", dataNascimento: "2000-07-18T00:00:00" },
      ],

      adicionarPaciente: (form) =>
        set((state) => ({
          pacientes: [
            ...state.pacientes,
            {
              idPaciente: state.pacientes.length + 1,
              nome: form.nome,
              dataNascimento: `${form.dataNascimento}T00:00:00`,
            },
          ],
        })),

      removerPaciente: (id) =>
        set((state) => ({
          pacientes: state.pacientes.filter((p) => p.idPaciente !== id),
        })),
    }),
    { name: "pacientes" }
  )
);