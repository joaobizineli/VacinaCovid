namespace VacinaCovid.Entidade.EntidadesDTO
{
    public class VacinaPacienteDTO
    {
        public int IdVacinaPaciente { get; set; }
        public int IdPaciente { get; set; }
        public int IdVacina { get; set; }

        public DateTime DataVacinacao { get; set; }
        public TimeSpan HoraVacinacao { get; set; }
        public string Status { get; set; }

        public string LocalVacinacao { get; set; }

    }
}
