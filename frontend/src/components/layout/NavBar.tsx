import { Link } from "@tanstack/react-router";
import { Badge } from "@/components/ui/badge";

export function Navbar() {
  return (
    <nav className="bg-[#575E55] px-6 py-[22px] flex items-center justify-between sticky top-0 z-50">
      <div className="flex items-center gap-2">
        <svg width="24" height="24" viewBox="0 0 20 20" fill="none">
          <circle cx="10" cy="10" r="9" stroke="#CDE9CA" strokeWidth="1.5" />
          <path
            d="M6 10h8M10 6v8"
            stroke="#CDE9CA"
            strokeWidth="1.5"
            strokeLinecap="round"
          />
        </svg>
        <span className="text-white text-[17px] font-medium">Vacina Covid</span>
      </div>

      <div className="flex items-center gap-1.5">
        <Link
          to="/"
          className="text-[15px] px-[18px] py-[8px] rounded-md capitalize transition-colors border-none cursor-pointer bg-transparent text-white/65 hover:text-white [&.active]:bg-[#CDE9CA] [&.active]:text-[#575E55] [&.active]:font-medium"
        >
          Agendamentos
        </Link>
        <Link
          to="/pacientes"
          className="text-[15px] px-[18px] py-[8px] rounded-md capitalize transition-colors border-none cursor-pointer bg-transparent text-white/65 hover:text-white [&.active]:bg-[#CDE9CA] [&.active]:text-[#575E55] [&.active]:font-medium"
        >
          Pacientes
        </Link>
        <button className="bg-transparent border-none cursor-pointer text-[15px] text-white/65 hover:text-white px-[18px] py-[8px] rounded-md flex items-center gap-2 transition-colors">
          Notificações
          <Badge className="bg-[#E8A249] hover:bg-[#E8A249] text-white text-[12px] px-[8px] py-[2px] rounded-full pointer-events-none">
            5
          </Badge>
        </button>
      </div>
    </nav>
  );
}
