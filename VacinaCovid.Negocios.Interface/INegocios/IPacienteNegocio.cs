using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.EntidadesDTO;

namespace VacinaCovid.Negocios.Interface.INegocios
{
    public interface IPacienteNegocio
    {
        List<PacienteDTO> InserirPaciente(string nome, DateTime dataNascimento);

        List<PacienteDTO> ListarPacientes();

        PacienteDTO ObterPaciente(int id);

        void AtualizarPaciente(int id, string nome, DateTime dataNascimento);

        void RemoverPaciente(int id);

    }
}
