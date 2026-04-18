namespace VacinaCovid.Entidade.Entidades
{
    public class Vacina
    {
        public int IdVacina { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string Lote { get; set; }
        public DateTime DataValidade { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
