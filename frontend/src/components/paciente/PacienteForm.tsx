import { BaseCard } from "@/components/shared/BaseCard";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { usePacienteForm } from "@/hooks/usePacienteForm";

export function PacienteForm() {
  const {
    nome,
    setNome,
    dataNascimento,
    setDataNascimento,
    erro,
    loading,
    handleSubmit,
  } = usePacienteForm();

  return (
    <BaseCard title="Novo paciente">
      <div className="flex flex-col gap-7 h-full">
        <div className="flex flex-col gap-[4px]">
          <Label htmlFor="nome" className="text-[13px] text-[#777]">
            Nome completo
          </Label>
          <Input
            id="nome"
            type="text"
            placeholder="Ex: João Silva"
            className="h-16"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />
        </div>
        <div className="flex flex-col gap-[4px]">
          <Label htmlFor="nascimento" className="text-[13px] text-[#777]">
            Data de nascimento
          </Label>
          <Input
            id="nascimento"
            type="date"
            className="h-16"
            value={dataNascimento}
            onChange={(e) => setDataNascimento(e.target.value)}
          />
        </div>
        <div className="mt-auto">
          {erro && <p className="text-[12px] text-red-500 mb-2">{erro}</p>}
          <Button
            className="w-full h-16 bg-[#575E55] hover:bg-[#474e45] text-white text-[14px] cursor-pointer disabled:opacity-50"
            onClick={handleSubmit}
            disabled={loading}
          >
            {loading ? "Cadastrando..." : "Cadastrar paciente"}
          </Button>
        </div>
      </div>
    </BaseCard>
  );
}
