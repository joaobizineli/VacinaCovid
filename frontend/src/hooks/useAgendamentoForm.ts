import { useState } from "react";
import { useAgendamentoStore } from "@/store/agendamentoStore";
import { usePacienteStore } from "@/store/pacienteStore";
import { useModalService } from "@/services/modalService";

export function useAgendamentoForm() {
  const [idPaciente, setIdPaciente] = useState("");
  const [data, setData] = useState("");
  const [hora, setHora] = useState("");
  const [idVacina, setIdVacina] = useState("");
  const [local, setLocal] = useState("");
  const [erro, setErro] = useState("");

  const pacientes = usePacienteStore((s) => s.pacientes);
  const adicionarAgendamento = useAgendamentoStore(
    (s) => s.adicionarAgendamento,
  );
  const abrir = useModalService((s) => s.abrir);

  function handleSubmit() {
    if (!idPaciente || !data || !hora || !idVacina || !local) {
      setErro("Preencha todos os campos.");
      return;
    }

    const resultado = adicionarAgendamento({
      idPaciente: Number(idPaciente),
      idVacina: Number(idVacina),
      dataVacinacao: data,
      horaVacinacao: hora,
      localVacinacao: local,
    });

    if (!resultado.sucesso) {
      setErro(resultado.erro ?? "Erro ao agendar.");
      return;
    }

    setIdPaciente("");
    setData("");
    setHora("");
    setIdVacina("");
    setLocal("");
    setErro("");

    abrir("Agendamento realizado!", "Paciente agendado com sucesso.");
  }

  return {
    idPaciente,
    setIdPaciente,
    data,
    setData,
    hora,
    setHora,
    idVacina,
    setIdVacina,
    local,
    setLocal,
    erro,
    pacientes,
    handleSubmit,
  };
}
