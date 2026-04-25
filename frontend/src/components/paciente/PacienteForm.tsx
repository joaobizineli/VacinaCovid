import { BaseCard } from "@/components/shared/BaseCard";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { usePacienteForm } from "@/hooks/usePacienteForm";
import ReactDatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

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
          <ReactDatePicker
            selected={
              dataNascimento ? new Date(`${dataNascimento}T12:00:00`) : null
            }
            onChange={(date: Date | null) => {
              if (date) setDataNascimento(date.toISOString().slice(0, 10));
            }}
            dateFormat="dd/MM/yyyy"
            placeholderText="Selecionar data de nascimento"
            showMonthDropdown
            showYearDropdown
            dropdownMode="select"
            className="w-full h-16 px-3 text-[13px] text-[#575E55] border border-[#e5e8e3] rounded-md outline-none"
            wrapperClassName="w-full"
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
