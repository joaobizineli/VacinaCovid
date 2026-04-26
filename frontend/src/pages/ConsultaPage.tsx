import { BaseCard } from "@/components/shared/BaseCard";
import { ConsultaFiltros } from "@/components/consulta/ConsultaFiltros";
import { ConsultaLista } from "@/components/consulta/ConsultaLista";
import { useConsultaPage } from "@/hooks/useConsultaPage";

export function ConsultaPage() {
  const { filtrados, getNomePaciente, ...filtros } = useConsultaPage();

  return (
    <div className="p-6">
      <h1 className="text-[17px] font-semibold text-[#575E55] mb-1">
        Consulta de agendamentos
      </h1>
      <p className="text-[13px] text-[#999] mb-4">
        Pesquise e filtre todos os agendamentos
      </p>
      <BaseCard
        title="Consulta de agendamentos"
        subtitle={`${filtrados.length} resultado(s)`}
      >
        <ConsultaFiltros {...filtros} />
        <ConsultaLista
          agendamentos={filtrados}
          getNomePaciente={getNomePaciente}
        />
      </BaseCard>
    </div>
  );
}
