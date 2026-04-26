import { useState, useEffect } from "react";
import { useAgendamentoStore } from "@/store/agendamentoStore";
import { usePacienteStore } from "@/store/pacienteStore";
import { useModalService } from "@/services/modalService";
import { agendamentoService } from "@/services/api/agendamentoService";
import { pacienteService } from "@/services/api/pacienteService";

export function useAgendamentoForm() {
  const [idPaciente, setIdPaciente] = useState("");
  const [data, setData] = useState("");
  const [hora, setHora] = useState("");
  const [idVacina, setIdVacina] = useState("");
  const [local, setLocal] = useState("");
  const [erro, setErro] = useState("");
  const [loading, setLoading] = useState(false);

  const pacientes = usePacienteStore((s) => s.pacientes);
  const setPacientes = usePacienteStore((s) => s.setPacientes);
  const agendamentos = useAgendamentoStore((s) => s.agendamentos);
  const setAgendamentos = useAgendamentoStore((s) => s.setAgendamentos);
  const abrir = useModalService((s) => s.abrir);

  useEffect(() => {
    if (pacientes.length === 0) {
      pacienteService.listar().then(setPacientes);
    }
  }, []);

  async function handleSubmit() {
    if (!idPaciente || !data || !hora || !idVacina || !local) {
      setErro("Preencha todos os campos.");
      return;
    }
    
    const agendamentosNoDia = agendamentos.filter(
      (a) => a.dataVacinacao.slice(0, 10) === data,
    );

    if (agendamentosNoDia.length >= 20) {
      setErro("Limite de 20 agendamentos por dia já atingido.");
      return;
    }

    const agendamentosNoHorario = agendamentosNoDia.filter(
      (a) =>
        a.horaVacinacao.slice(0, 5) === hora &&
        (a.status === "Agendado" || a.status === "Realizado"),
    );

    if (agendamentosNoHorario.length >= 2) {
      setErro("Máximo de 2 agendamentos para este horário.");
      return;
    }

    const conflito = agendamentosNoDia.some((a) => {
      if (a.status === "Cancelado") return false;
      if (a.horaVacinacao.slice(0, 5) === hora) return false;
      const [h1, m1] = hora.split(":").map(Number);
      const [h2, m2] = a.horaVacinacao.slice(0, 5).split(":").map(Number);
      return Math.abs(h1 * 60 + m1 - (h2 * 60 + m2)) < 60;
    });

    if (conflito) {
      setErro("Deve haver intervalo de 1 hora entre agendamentos.");
      return;
    }

    setLoading(true);
    try {
      const novo = await agendamentoService.agendar({
        idPaciente: Number(idPaciente),
        idVacina: Number(idVacina),
        dataVacinacao: data,
        horaVacinacao: hora,
        localVacinacao: local,
      });
      setAgendamentos([...agendamentos, novo]);
      setIdPaciente("");
      setData("");
      setHora("");
      setIdVacina("");
      setLocal("");
      setErro("");
      abrir("Agendamento realizado!", "Paciente agendado com sucesso.");
    } catch (e) {
      setErro(e instanceof Error ? e.message : "Erro ao agendar.");
    } finally {
      setLoading(false);
    }
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
    loading,
    pacientes,
    handleSubmit,
  };
}
