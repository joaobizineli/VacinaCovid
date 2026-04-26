import { createRouter } from "@tanstack/react-router";
import { Route as rootRoute } from "@/routes/__root";
import { Route as agendamentosRoute } from "@/routes/index";
import { Route as pacientesRoute } from "@/routes/pacientes";
import { Route as consultaRoute } from "@/routes/consulta";


const routeTree = rootRoute.addChildren([agendamentosRoute, pacientesRoute, consultaRoute]);

export const router = createRouter({ routeTree });

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}