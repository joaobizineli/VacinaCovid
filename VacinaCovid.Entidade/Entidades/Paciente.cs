namespace VacinaCovid.Entidade.Entidades
{
    public class Paciente : IdEntidade<int>
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}