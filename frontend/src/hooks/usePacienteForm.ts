import { useState } from "react";
import { usePacienteStore } from "@/store/pacienteStore";
import { useModalService } from "@/services/modalService";
import { pacienteService } from "@/services/api/pacienteService";

export function usePacienteForm() {
  const [nome, setNome] = useState("");
  const [dataNascimento, setDataNascimento] = useState("");
  const [erro, setErro] = useState("");
  const [loading, setLoading] = useState(false);

  const pacientes = usePacienteStore((s) => s.pacientes);
  const setPacientes = usePacienteStore((s) => s.setPacientes);
  const abrir = useModalService((s) => s.abrir);

  async function handleSubmit() {
    if (!nome || !dataNascimento) {
      setErro("Preencha todos os campos.");
      return;
    }

    setLoading(true);
    try {
      const novo = await pacienteService.criar({ nome, dataNascimento });
      setPacientes([...pacientes, novo]);
      setNome(""); setDataNascimento(""); setErro("");
      abrir("Paciente cadastrado!", `${nome} foi adicionado com sucesso.`);
    } catch {
      setErro("Erro ao cadastrar paciente.");
    } finally {
      setLoading(false);
    }
  }

  return { nome, setNome, dataNascimento, setDataNascimento, erro, loading, handleSubmit };
}
