import { createRootRoute, Outlet } from "@tanstack/react-router";
import { Navbar } from "@/components/layout/NavBar";
import { SuccessModal } from "@/components/shared/SuccessModal";


export const Route = createRootRoute({
  component: () => (
    <div className="min-h-screen bg-[#f5f7f4]">
      <Navbar />
      <Outlet />
      <SuccessModal />
    </div>
  ),
});