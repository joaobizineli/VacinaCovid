import { BaseCard } from "../shared/BaseCard";

export function AgendamentoLista() {
  return (
    <BaseCard title="Agendamentos do dia" subtitle="25/04/2026" className="h-full">
      <div className="flex flex-col gap-3">
        <p className="text-[11px] text-[#bbb] uppercase tracking-[0.6px]">Manhã</p>
        <div className="flex flex-col gap-[7px]">
          <Item hora="08:00" nome="João Silva" local="UBS Centro" status="Agendado" />
          <Item hora="09:00" nome="Maria Oliveira" local="UBS Norte" status="Agendado" />
          <Item hora="10:00" nome="Carlos Santos" local="UBS Centro" status="Realizado" />
        </div>
        <p className="text-[11px] text-[#bbb] uppercase tracking-[0.6px]">Tarde</p>
        <div className="flex flex-col gap-[7px]">
          <Item hora="14:00" nome="Ana Souza" local="UBS Sul" status="Agendado" />
          <Item hora="15:00" nome="Pedro Lima" local="UBS Centro" status="Cancelado" />
        </div>
      </div>
    </BaseCard>
  );
}

const statusCls: Record<string, string> = {
  Agendado: "bg-[#e6f4e4] text-[#3a6e38]",
  Realizado: "bg-[#fdebd4] text-[#8a4510]",
  Cancelado: "bg-[#eee] text-[#777]",
};

function Item({ hora, nome, local, status }: { hora: string; nome: string; local: string; status: string }) {
  return (
    <div className="flex items-center gap-[10px] px-3 py-[9px] rounded-[8px] border-[0.5px] border-[#e5e8e3]">
      <span className="bg-[#CDE9CA] text-[#575E55] text-[12px] font-semibold px-[10px] py-1 rounded-[6px] min-w-[52px] text-center">
        {hora}
      </span>
      <div className="flex-1">
        <div className="text-[13px] font-medium text-[#575E55]">{nome}</div>
        <div className="text-[11px] text-[#aaa] mt-[1px]">{local}</div>
      </div>
      <span className={`text-[11px] px-[9px] py-[3px] rounded-full ${statusCls[status]}`}>
        {status}
      </span>
    </div>
  );
}