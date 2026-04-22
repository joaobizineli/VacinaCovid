import { BaseCard } from "../shared/BaseCard";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { Label } from "../ui/label";

export function AgendamentoForm() {
  return (
    <BaseCard title="Novo agendamento" className="h-full">
      <div className="flex flex-col gap-3 h-full">
        <div className="flex flex-col gap-[5px]">
          <Label htmlFor="paciente" className="text-[12px] text-[#777]">Paciente</Label>
          <Input id="paciente" type="text" placeholder="Buscar paciente..." />
        </div>
        <div className="grid grid-cols-2 gap-[10px]">
          <div className="flex flex-col gap-[5px]">
            <Label htmlFor="data" className="text-[12px] text-[#777]">Data</Label>
            <Input id="data" type="date" />
          </div>
          <div className="flex flex-col gap-[5px]">
            <Label htmlFor="horario" className="text-[12px] text-[#777]">Horário</Label>
            <Input id="horario" type="time" />
          </div>
        </div>
        <div className="flex flex-col gap-[5px]">
          <Label htmlFor="local" className="text-[12px] text-[#777]">Local de vacinação</Label>
          <Input id="local" type="text" placeholder="Ex: UBS Centro" />
        </div>
        <Button className="w-full bg-[#575E55] hover:bg-[#474e45] text-white text-[13px] mt-auto">
          Agendar
        </Button>
      </div>
    </BaseCard>
  );
}