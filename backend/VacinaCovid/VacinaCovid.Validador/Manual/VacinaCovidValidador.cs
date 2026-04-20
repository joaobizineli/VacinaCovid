using VacinaCovid.Entidade.Model;
using VacinaCovid.Utilitarios.Excepetions;
using VacinaCovid.Utilitarios.Exceptions;
using VacinaCovid.Utilitarios.Messages;

namespace VacinaCovid.Validador.Manual
{
    public static class VacinaCovidValidador
    {
        public static void Validar(VacinaPacienteModel vacinaPaciente)
        {
            var erros = new List<string>();
            if (vacinaPaciente.IdPaciente <= 0)
                erros.Add(string.Format(BusinessMessages.IdPacienteObrigatorio));

            if (vacinaPaciente.IdVacina <= 0)
                erros.Add(string.Format(BusinessMessages.IdVacinaObrigatoria));

            if (vacinaPaciente.DataVacinacao == default)
                erros.Add(string.Format(BusinessMessages.DataVacinacaoObrigatoria));

            if (vacinaPaciente.HoraVacinacao == default)
                erros.Add(string.Format(BusinessMessages.HoraVacinacaoObrigatoria));

            if (string.IsNullOrWhiteSpace(vacinaPaciente.LocalVacinacao))
                erros.Add(string.Format(BusinessMessages.LocalVacinacaoObrigatorio));

            if (vacinaPaciente.LocalVacinacao.Length < 3)
                erros.Add(string.Format(BusinessMessages.LocalVacinacaoMinimo));

            if (vacinaPaciente.LocalVacinacao.Length > 100)
                erros.Add(string.Format(BusinessMessages.LocalVacinacaoMaximo));

            if (erros.Any())
            {
               throw new BusinessListException(erros);
            }
        }
    }
}
