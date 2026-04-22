import { StatCard } from "../shared/StatCard";

interface StatsBarProps {
  total: number;
  agendados: number;
  realizados: number;
  cancelados: number;
}

export function StatsBar({ total, agendados, realizados, cancelados }: StatsBarProps) {
  return (
    <div className="grid grid-cols-4 gap-[10px] mb-[10px]">
      <StatCard label="Total hoje" value={total} tag="de 20 vagas" tagClassName="bg-[#eee] text-[#666]" />
      <StatCard label="Agendados" value={agendados} tag="aguardando" tagClassName="bg-[#e6f4e4] text-[#3a6e38]" />
      <StatCard label="Realizados" value={realizados} tag="concluídos" tagClassName="bg-[#fdebd4] text-[#8a4510]" />
      <StatCard label="Cancelados" value={cancelados} tag="cancelados" tagClassName="bg-[#eee] text-[#666]" />
    </div>
  );
}