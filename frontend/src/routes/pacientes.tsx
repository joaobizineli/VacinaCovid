import { createRoute } from "@tanstack/react-router";
import { Route as rootRoute } from "./__root";
import { PacientesPage } from "../pages/PacientesPage";

export const Route = createRoute({
  getParentRoute: () => rootRoute,
  path: "/pacientes",
  component: PacientesPage,
});