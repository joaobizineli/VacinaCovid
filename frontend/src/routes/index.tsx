import { createRoute } from "@tanstack/react-router";
import { Route as rootRoute } from "./__root";
import { AgendamentosPage } from "@/pages/AgendamentosPage";

export const Route = createRoute({
  getParentRoute: () => rootRoute,
  path: "/",
  component: AgendamentosPage,
});