namespace VacinaCovid.Entidade.EntidadesDTO
{
    public class VacinaDTO
    {
        public int IdVacina { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string Lote { get; set; }
        public DateTime DataValidade { get; set; }
        public int Quantidade { get; set; }
    }
}
