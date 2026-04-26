import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import ReactDatePicker from "react-datepicker";

interface Props {
  filtroPaciente: string; setFiltroPaciente: (v: string) => void;
  filtroVacina: string; setFiltroVacina: (v: string) => void;
  filtroLocal: string; setFiltroLocal: (v: string) => void;
  filtroData: string; setFiltroData: (v: string) => void;
  filtroStatus: string; setFiltroStatus: (v: string) => void;
}



export function ConsultaFiltros({ filtroPaciente, setFiltroPaciente, filtroVacina, setFiltroVacina, filtroLocal, setFiltroLocal, filtroData, setFiltroData, filtroStatus, setFiltroStatus }: Props) {
  return (
    <div className="grid grid-cols-5 gap-3">
      <div className="flex flex-col gap-[4px]">
        <Label className="text-[13px] text-[#777]">Paciente</Label>
        <Input placeholder="Buscar paciente..." value={filtroPaciente} onChange={(e) => setFiltroPaciente(e.target.value)} />
      </div>
      <div className="flex flex-col gap-[4px]">
        <Label className="text-[13px] text-[#777]">Vacina</Label>
        <Select value={filtroVacina} onValueChange={setFiltroVacina}>
          <SelectTrigger className="w-full"><SelectValue placeholder="Todas" /></SelectTrigger>
          <SelectContent position="popper" side="bottom" avoidCollisions={false}>
            <SelectItem value="todas">Todas</SelectItem>
            <SelectItem value="pfizer">Pfizer</SelectItem>
            <SelectItem value="astrazeneca">AstraZeneca</SelectItem>
            <SelectItem value="coronavac">Coronavac</SelectItem>
          </SelectContent>
        </Select>
      </div>
      <div className="flex flex-col gap-[4px]">
        <Label className="text-[13px] text-[#777]">Local</Label>
        <Input placeholder="Buscar local..." value={filtroLocal} onChange={(e) => setFiltroLocal(e.target.value)} />
      </div>
      <div className="flex flex-col gap-[4px]">
        <Label className="text-[13px] text-[#777]">Data</Label>
        <ReactDatePicker
          popperPlacement="bottom-start"
          selected={filtroData ? new Date(`${filtroData}T12:00:00`) : null}
          onChange={(date: Date | null) => setFiltroData(date ? date.toISOString().slice(0, 10) : "")}
          dateFormat="dd/MM/yyyy"
          placeholderText="Todas as datas"
          isClearable
          className="w-full h-9 px-3 text-[13px] text-[#575E55] border border-[#e5e8e3] rounded-md outline-none"
          wrapperClassName="w-full"
        />
      </div>
      <div className="flex flex-col gap-[4px]">
        <Label className="text-[13px] text-[#777]">Status</Label>
        <Select value={filtroStatus} onValueChange={setFiltroStatus}>
          <SelectTrigger className="w-[345px]"><SelectValue placeholder="Todos" /></SelectTrigger>
            <SelectContent position="popper" side="bottom" avoidCollisions={false}>
            <SelectItem value="todos">Todos</SelectItem>
            <SelectItem value="Agendado">Agendado</SelectItem>
            <SelectItem value="Realizado">Realizado</SelectItem>
            <SelectItem value="Cancelado">Cancelado</SelectItem>
          </SelectContent>
        </Select>
      </div>
    </div>
  );
}
