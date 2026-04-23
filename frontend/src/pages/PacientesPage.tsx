import { PacienteForm } from "@/components/paciente/PacienteForm";
import { PacienteLista } from "@/components/paciente/PacienteLista";

export function PacientesPage() {
  return (
    <div className="p-6">
      <h1 className="text-[17px] font-semibold text-[#575E55] mb-1">
        Pacientes
      </h1>
      <p className="text-[13px] text-[#999] mb-[18px]">
        Gerencie os pacientes cadastrados
      </p>
      <div className="grid grid-cols-3 gap-[10px] items-stretch">
        <PacienteForm />
        <div className="col-span-2 h-full">
          <PacienteLista />
        </div>
      </div>
    </div>
  );
}
