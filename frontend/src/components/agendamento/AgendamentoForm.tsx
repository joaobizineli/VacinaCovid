import { useAgendamentoForm } from "@/hooks/useAgendamentoForm";
import { BaseCard } from "@/components/shared/BaseCard";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";

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
      <div className="flex flex-col gap-7 h-full">
        <div className="flex flex-col gap-[4px]">
          <Label className="text-[13px] text-[#777]">Paciente</Label>
          <Select value={idPaciente} onValueChange={setIdPaciente}>
            <SelectTrigger className="!h-13 w-full pl-3 text-[13px] text-[#575E55] rounded-md">
              <SelectValue placeholder="Selecionar paciente..." />
            </SelectTrigger>
            <SelectContent position="popper">
              {pacientes.map((p) => (
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
            <Input
              type="date"
              className="h-13"
              value={data}
              onChange={(e) => setData(e.target.value)}
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
            <SelectContent position="popper">
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
          {erro && <p className="text-[12px] text-red-500 mb-2">{erro}</p>}
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
