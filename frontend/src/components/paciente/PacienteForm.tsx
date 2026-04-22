import { BaseCard } from "@/components/shared/BaseCard";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

export function PacienteForm() {
  return (
    <BaseCard title="Novo paciente">
  <div className="flex flex-col gap-7 h-full">
  <div className="flex flex-col gap-[4px]">
    <Label htmlFor="nome" className="text-[13px] text-[#777]">Nome completo</Label>
    <Input id="nome" type="text" placeholder="Ex: João Silva" className="h-16" />
  </div>
  <div className="flex flex-col gap-[4px]">
    <Label htmlFor="nascimento" className="text-[13px] text-[#777]">Data de nascimento</Label>
    <Input id="nascimento" type="date" className="h-16" />
  </div>
  <Button className="w-full h-16 bg-[#575E55] hover:bg-[#474e45] text-white text-[14px] mt-auto">
    Cadastrar paciente
  </Button>
</div>
</BaseCard>
  );
}