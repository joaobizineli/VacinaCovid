import dayjs from "dayjs";
import "dayjs/locale/pt-br";

dayjs.locale("pt-br");

export function formatarDataExibicao(data: string): string {
  return dayjs(data).format("DD/MM/YYYY");
}

export function formatarHora(hora: string): string {
  return hora.slice(0, 5);
}

export function inputParaISO(data: string): string {
  return dayjs(data).toISOString();
}

export function horaParaBackend(hora: string): string {
  return `${hora}:00`;
}


export function calcularIdade(dataNascimento: string): number {
  return dayjs().diff(dayjs(dataNascimento), "year");
}


export function hoje(): string {
  return dayjs().format("YYYY-MM-DD");
}