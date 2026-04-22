import { useState } from "react";
import { Navbar } from "./components/layout/NavBar";
import { StatsBar } from "./components/agendamento/StatsBar";
import { AgendamentoForm } from "./components/agendamento/AgendamentoForm";
import { AgendamentoLista } from "./components/agendamento/AgendamentoLista";
import "./App.css";

type Page = "agendamentos" | "pacientes";

export default function App() {
  const [page, setPage] = useState<Page>("agendamentos");

  return (
    <div className="min-h-screen bg-[#f5f7f4]">
      <Navbar activePage={page} onNavigate={setPage} />
      {page === "agendamentos" && (
        <div className="p-6">
          <h1 className="text-[17px] font-semibold text-[#575E55] mb-1">Agendamentos</h1>
          <p className="text-[13px] text-[#999] mb-[18px]">Gerencie os agendamentos de vacinação</p>
          <StatsBar total={5} agendados={3} realizados={1} cancelados={1} />
          <div className="grid grid-cols-2 gap-[14px]">
            <AgendamentoForm />
            <AgendamentoLista />
          </div>
        </div>
      )}
      {page === "pacientes" && <div className="p-6 text-[#575E55]">Pacientes (em breve)</div>}
    </div>
  );
}
