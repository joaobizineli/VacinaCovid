using VacinaCovid.Entidade.Enum;

namespace VacinaCovid.Entidade.Entidades
{
    public class VacinaPaciente : IdEntidade<int>
    {
        public int IdPaciente { get; set; }
        public int IdVacina { get; set; }

        public DateTime DataVacinacao { get; set; }
        public TimeSpan HoraVacinacao { get; set; }
        public StatusEnum Status { get; set; }

        public string? LocalVacinacao { get; set; }
        public string? Observacoes { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }
    }
}