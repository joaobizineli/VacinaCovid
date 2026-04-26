import { useState, useEffect } from "react";
import { agendamentoService } from "@/services/api/agendamentoService";
import { pacienteService } from "@/services/api/pacienteService";
import type { Agendamento, Paciente } from "@/types";

export function useConsultaPage() {
  const [agendamentos, setAgendamentos] = useState<Agendamento[]>([]);
  const [pacientes, setPacientes] = useState<Paciente[]>([]);
  const [filtroPaciente, setFiltroPaciente] = useState("");
  const [filtroVacina, setFiltroVacina] = useState("");
  const [filtroLocal, setFiltroLocal] = useState("");
  const [filtroData, setFiltroData] = useState("");
  const [filtroStatus, setFiltroStatus] = useState("");

  useEffect(() => {
    agendamentoService.listar().then(setAgendamentos);
    pacienteService.listar().then(setPacientes);
  }, []);

  function getNomePaciente(id: number) {
    return pacientes.find((p) => p.idPaciente === id)?.nome ?? "-";
  }
  async function marcarRealizado(id: number) {
    await agendamentoService.marcarRealizado(id);
    agendamentoService.listar().then(setAgendamentos);
  }

  async function cancelarAgendamento(id: number) {
    await agendamentoService.cancelar(id);
    agendamentoService.listar().then(setAgendamentos);
  }

  const vacinaMap: Record<number, string> = {
    1: "pfizer",
    2: "astrazeneca",
    3: "coronavac",
  };

  const filtrados = agendamentos.filter((a) => {
    const nome = getNomePaciente(a.idPaciente).toLowerCase();

    if (filtroPaciente && !nome.includes(filtroPaciente.toLowerCase()))
      return false;
    if (
      filtroVacina &&
      filtroVacina !== "todas" &&
      vacinaMap[a.idVacina] !== filtroVacina
    )
      return false;
    if (
      filtroLocal &&
      !a.localVacinacao.toLowerCase().includes(filtroLocal.toLowerCase())
    )
      return false;
    if (filtroData && a.dataVacinacao.slice(0, 10) !== filtroData) return false;
    if (filtroStatus && filtroStatus !== "todos" && a.status !== filtroStatus)
      return false;
    return true;
  });

  return {
    filtrados,
    getNomePaciente,
    marcarRealizado,
    cancelarAgendamento,
    filtroPaciente,
    setFiltroPaciente,
    filtroVacina,
    setFiltroVacina,
    filtroLocal,
    setFiltroLocal,
    filtroData,
    setFiltroData,
    filtroStatus,
    setFiltroStatus,
  };
}
