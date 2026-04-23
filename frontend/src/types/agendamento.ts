
export type StatusAgendamento = "Agendado" | "Realizado" | "Cancelado";
 
export interface Agendamento {
  idVacinaPaciente: number;
  idPaciente: number;
  idVacina: number;
  dataVacinacao: string;
  horaVacinacao: string;
  status: StatusAgendamento;
  localVacinacao: string;
  observacoes: string;
  dataCriacao: string;
  dataAtualizacao: string;
}
 
export interface CriarAgendamentoForm {
  idPaciente: number;
  idVacina: number;
  dataVacinacao: string;
  horaVacinacao: string;
  localVacinacao: string;
}
 