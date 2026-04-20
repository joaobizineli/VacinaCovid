using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacinaCovid.Entidade.Model
{
    public class VacinaPacienteModel
    {
        public int IdPaciente { get; set; }
        public int IdVacina { get; set; }
        public DateTime DataVacinacao { get; set; }
        public TimeSpan HoraVacinacao { get; set; }
        public string LocalVacinacao { get; set; }
    }
}
