import { Badge } from "../ui/badge";

type Page = "agendamentos" | "pacientes";

interface NavbarProps {
  activePage: Page;
  onNavigate: (page: Page) => void;
  notificacoes?: number;
}

export function Navbar({ activePage, onNavigate, notificacoes = 5 }: NavbarProps) {
  return (
    <nav className="bg-[#575E55] px-6 py-[22px] flex items-center justify-between sticky top-0 z-50">
      <div className="flex items-center gap-2">
        <svg width="24" height="24" viewBox="0 0 20 20" fill="none">
          <circle cx="10" cy="10" r="9" stroke="#CDE9CA" strokeWidth="1.5" />
          <path d="M6 10h8M10 6v8" stroke="#CDE9CA" strokeWidth="1.5" strokeLinecap="round" />
        </svg>
        <span className="text-white text-[17px] font-medium">Vacina Covid</span>
      </div>

      <div className="flex items-center gap-1.5">
        {(["agendamentos", "pacientes"] as Page[]).map((p) => (
          <button
            key={p}
            onClick={() => onNavigate(p)}
            className={`text-[15px] px-[18px] py-[8px] rounded-md capitalize transition-colors border-none cursor-pointer ${
              activePage === p
                ? "bg-[#CDE9CA] text-[#575E55] font-medium"
                : "bg-transparent text-white/65 hover:text-white"
            }`}
          >
            {p}
          </button>
        ))}

        <button className="bg-transparent border-none cursor-pointer text-[15px] text-white/65 hover:text-white px-[18px] py-[8px] rounded-md flex items-center gap-2 transition-colors">
          Notificações
          {notificacoes > 0 && (
            <Badge className="bg-[#E8A249] hover:bg-[#E8A249] text-white text-[12px] px-[8px] py-[2px] rounded-full pointer-events-none">
                {notificacoes}
            </Badge>
          )}
        </button>
      </div>
    </nav>
  );
}