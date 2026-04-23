import { createRouter } from "@tanstack/react-router";
import { Route as rootRoute } from "@/routes/__root";
import { Route as agendamentosRoute } from "@/routes/index";
import { Route as pacientesRoute } from "@/routes/pacientes";

const routeTree = rootRoute.addChildren([agendamentosRoute, pacientesRoute]);

export const router = createRouter({ routeTree });

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}