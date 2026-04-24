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
      setIdPaciente(""); setData(""); setHora("");
      setIdVacina(""); setLocal(""); setErro("");
      abrir("Agendamento realizado!", "Paciente agendado com sucesso.");
    } catch (e) {
      setErro(e instanceof Error ? e.message : "Erro ao agendar.");
    } finally {
      setLoading(false);
    }
  }

  return {
    idPaciente, setIdPaciente,
    data, setData,
    hora, setHora,
    idVacina, setIdVacina,
    local, setLocal,
    erro, loading, pacientes,
    handleSubmit,
  };
}
