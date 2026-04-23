import { useState } from "react";
import { usePacienteStore } from "@/store/pacienteStore";
import { useModalService } from "@/services/modalService";

export function usePacienteForm() {
  const [nome, setNome] = useState("");
  const [dataNascimento, setDataNascimento] = useState("");
  const [erro, setErro] = useState("");

  const adicionarPaciente = usePacienteStore((s) => s.adicionarPaciente);
  const abrir = useModalService((s) => s.abrir);

  function handleSubmit() {
    if (!nome || !dataNascimento) {
      setErro("Preencha todos os campos.");
      return;
    }

    adicionarPaciente({ nome, dataNascimento });

    setNome("");
    setDataNascimento("");
    setErro("");

    abrir("Paciente cadastrado!", `${nome} foi adicionado com sucesso.`);
  }

  return {
    nome,
    setNome,
    dataNascimento,
    setDataNascimento,
    erro,
    handleSubmit,
  };
}
