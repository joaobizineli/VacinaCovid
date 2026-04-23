import { useModalService } from "@/services/modalService";

export function SuccessModal() {
  const { isOpen, titulo, mensagem, fechar } = useModalService();

  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/30"
      onClick={fechar}
    >
      <div
        className="bg-white rounded-[14px] p-8 w-full max-w-sm flex flex-col items-center gap-4 shadow-xl"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="w-14 h-14 rounded-full bg-[#CDE9CA] flex items-center justify-center text-[#575E55] text-2xl">
          ✓
        </div>
        <div className="text-center">
          <p className="text-[15px] font-semibold text-[#575E55]">{titulo}</p>
          <p className="text-[13px] text-[#aaa] mt-1">{mensagem}</p>
        </div>
        <button
          onClick={fechar}
          className="w-full h-11 rounded-[8px] bg-[#575E55] hover:bg-[#474e45] text-white text-[13px]"
        >
          Fechar
        </button>
      </div>
    </div>
  );
}