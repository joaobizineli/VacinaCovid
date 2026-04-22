import { BaseCard } from "../shared/BaseCard";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { Label } from "../ui/label";

export function AgendamentoForm() {
  return (
    <BaseCard title="Novo agendamento" className="h-full">
      <div className="flex flex-col gap-7 h-full">
        <div className="flex flex-col gap-[6px]">
          <div className="flex flex-col gap-[4px]">
            <Label htmlFor="paciente" className="text-[13px] text-[#777]">
              Paciente
            </Label>
            <Input
              id="paciente"
              type="text"
              placeholder="Buscar paciente..."
              className="h-13"
            />
          </div>
        </div>
        <div className="grid grid-cols-2 gap-[10px]">
          <div className="flex flex-col gap-[4px] h-full">
            <Label htmlFor="data" className="text-[13px] text-[#777]">
              Data
            </Label>
            <Input id="data" type="date" className="h-13" />
          </div>
          <div className="flex flex-col gap-[4px] h-full">
            <Label htmlFor="horario" className="text-[13px] text-[#777]">
              Horário
            </Label>
            <Input id="horario" type="time" className="h-13" />
          </div>
        </div>
        <div className="flex flex-col gap-[4px]">
          <Label htmlFor="vacina" className="text-[13px] text-[#777]">
            Vacina
          </Label>
          <select title="lista" id="vacina" className="h-13 w-full pl-3 pr-6 border border-input rounded-md text-[13px] text-[#575E55] bg-background">
            <option value="1">COVID-19 - Pfizer</option>
            <option value="2">COVID-19 - AstraZeneca</option>
            <option value="3">COVID-19 - Coronavac</option>
          </select>
        </div>
        <div className="flex flex-col gap-[4px] ">
          <Label htmlFor="local" className="text-[13px] text-[#777]">
            Local de vacinação
          </Label>
          <Input
            id="local"
            type="text"
            placeholder="Ex: UBS Centro"
            className="h-13"
          />
        </div>
        <Button className="w-full h-13 bg-[#575E55] hover:bg-[#474e45] text-white text-[14px] mt-auto">
          Agendar
        </Button>
      </div>
    </BaseCard>
  );
}
