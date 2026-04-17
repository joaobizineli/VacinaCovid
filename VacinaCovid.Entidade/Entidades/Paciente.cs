namespace VacinaCovid.Entidade.Entidades
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataVacina { get; set; }
    }
}
