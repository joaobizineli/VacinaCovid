import { useAgendamentoForm } from "@/hooks/useAgendamentoForm";
import { BaseCard } from "@/components/shared/BaseCard";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import ReactDatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

export function AgendamentoForm() {
  const {
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
  } = useAgendamentoForm();

  return (
    <BaseCard title="Novo agendamento" className="h-full">
      <div className="flex flex-col gap-6 h-full">
        <div className="flex flex-col gap-[4px]">
          <Label className="text-[13px] text-[#777]">Paciente</Label>
          <Select value={idPaciente} onValueChange={setIdPaciente}>
            <SelectTrigger className="!h-13 w-full pl-3 text-[13px] text-[#575E55] rounded-md">
              <SelectValue placeholder="Selecionar paciente..." />
            </SelectTrigger>
            <SelectContent
              position="popper"
              side="bottom"
              avoidCollisions={false}
            >
              {[...pacientes]
                .sort((a, b) => a.nome.localeCompare(b.nome))
                .map((p) => (
                  <SelectItem key={p.idPaciente} value={String(p.idPaciente)}>
                    {p.nome}
                  </SelectItem>
                ))}
            </SelectContent>
          </Select>
        </div>

        <div className="grid grid-cols-2 gap-[10px]">
          <div className="flex flex-col gap-[4px]">
            <Label className="text-[13px] text-[#777]">Data</Label>
            <ReactDatePicker
              selected={data ? new Date(`${data}T12:00:00`) : null}
              onChange={(date: Date | null) => {
                if (date) setData(date.toISOString().slice(0, 10));
              }}
              dateFormat="dd/MM/yyyy"
              placeholderText="Selecionar data"
              showMonthDropdown
              showYearDropdown
              dropdownMode="select"
              minDate={new Date()}
              className="w-full h-13 px-3 text-[13px] text-[#575E55] border border-[#e5e8e3] rounded-md outline-none"
              wrapperClassName="w-full"
            />
          </div>
          <div className="flex flex-col gap-[4px]">
            <Label className="text-[13px] text-[#777]">Horário</Label>
            <Input
              type="time"
              className="h-13"
              value={hora}
              onChange={(e) => setHora(e.target.value)}
            />
          </div>
        </div>

        <div className="flex flex-col gap-[4px]">
          <Label className="text-[13px] text-[#777]">Vacina</Label>
          <Select value={idVacina} onValueChange={setIdVacina}>
            <SelectTrigger className="!h-13 w-full pl-3 text-[13px] text-[#575E55] rounded-md">
              <SelectValue placeholder="Selecione a vacina" />
            </SelectTrigger>
            <SelectContent
              position="popper"
              side="bottom"
              avoidCollisions={false}
            >
              <SelectItem value="1">COVID-19 - Pfizer</SelectItem>
              <SelectItem value="2">COVID-19 - AstraZeneca</SelectItem>
              <SelectItem value="3">COVID-19 - Coronavac</SelectItem>
            </SelectContent>
          </Select>
        </div>

        <div className="flex flex-col gap-[4px]">
          <Label className="text-[13px] text-[#777]">Local de vacinação</Label>
          <Input
            type="text"
            placeholder="Ex: UBS Centro"
            className="h-13"
            value={local}
            onChange={(e) => setLocal(e.target.value)}
          />
        </div>

        <div className="mt-auto">
          {erro && <p className="text-[12px] text-red-500">{erro}</p>}
          <Button
            className="w-full h-13 bg-[#575E55] hover:bg-[#474e45] text-white text-[14px] cursor-pointer"
            onClick={handleSubmit}
          >
            Agendar
          </Button>
        </div>
      </div>
    </BaseCard>
  );
}
