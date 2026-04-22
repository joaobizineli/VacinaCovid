import { BaseCard } from "@/components/shared/BaseCard";
import { Input } from "@/components/ui/input";

const mockPacientes = [
  { id: 1, nome: "João Silva", nascimento: "15/05/1990", idade: 35 },
  { id: 2, nome: "Maria Oliveira", nascimento: "22/08/1985", idade: 40 },
  { id: 3, nome: "Carlos Santos", nascimento: "10/03/1978", idade: 47 },
  { id: 4, nome: "Ana Souza", nascimento: "05/11/1995", idade: 30 },
  { id: 5, nome: "Pedro Lima", nascimento: "18/07/2000", idade: 25 },
  { id: 6, nome: "Fernanda Costa", nascimento: "30/01/1992", idade: 33 },
  { id: 7, nome: "Lucas Mendes", nascimento: "14/09/1988", idade: 37 },
  { id: 8, nome: "Juliana Ferreira", nascimento: "07/04/1997", idade: 28 },
  { id: 9, nome: "Ricardo Alves", nascimento: "25/12/1975", idade: 50 },
  { id: 10, nome: "Camila Rodrigues", nascimento: "11/06/2001", idade: 24 },
];

function getiniciais(nome: string) {
  return nome.split(" ").slice(0, 2).map(n => n[0]).join("").toUpperCase();
}

export function PacienteLista() {
  return (
<BaseCard title="Pacientes cadastrados" subtitle={`${mockPacientes.length} pacientes`}>    
  <div className="flex flex-col gap-2 overflow-y-auto max-h-[500px]">
        <Input placeholder="Buscar paciente..." className="h-10" />
        <div className="flex flex-col gap-2 overflow-y-auto max-h-[calc(100vh-280px)]">
          {mockPacientes.map(p => (
            <div key={p.id} className="flex items-center gap-3 px-3 py-[10px] rounded-[8px] border-[0.5px] border-[#e5e8e3]">
              <div className="w-[34px] h-[34px] rounded-full bg-[#CDE9CA] text-[#575E55] text-[13px] font-semibold flex items-center justify-center flex-shrink-0">
                {getiniciais(p.nome)}
              </div>
              <div className="flex-1">
                <div className="text-[13px] font-medium text-[#575E55]">{p.nome}</div>
                <div className="text-[11px] text-[#aaa] mt-[1px]">Nascido em {p.nascimento} · {p.idade} anos</div>
              </div>
              <div className="flex gap-2">
                <button className="text-[11px] px-[10px] py-[4px] rounded-[6px] border-[0.5px] border-[#e0e4dd] text-[#666] bg-white cursor-pointer">
                  Editar
                </button>
                <button className="text-[11px] px-[10px] py-[4px] rounded-[6px] border-[0.5px] border-[#f5c6c6] text-[#c0392b] bg-[#fff5f5] cursor-pointer">
                  Remover
                </button>
              </div>
            </div>
          ))}
        </div>
      </div>
    </BaseCard>
  );
}