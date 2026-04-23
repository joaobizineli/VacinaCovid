import { create } from "zustand";

interface ModalState {
  isOpen: boolean;
  titulo: string;
  mensagem: string;
  abrir: (titulo: string, mensagem: string) => void;
  fechar: () => void;
}

export const useModalService = create<ModalState>((set) => ({
  isOpen: false,
  titulo: "",
  mensagem: "",
  abrir: (titulo, mensagem) => set({ isOpen: true, titulo, mensagem }),
  fechar: () => set({ isOpen: false }),
}));