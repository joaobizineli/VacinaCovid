using FluentValidation;
using VacinaCovid.Entidade.Model;
using VacinaCovid.Utilitarios.Messages;

namespace VacinaCovid.Validador.Fluent
{
    public class PacienteValidator : AbstractValidator<PacienteModel>
    {
        public PacienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(BusinessMessages.NomeOuDataNascimentoObrigatorios);

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage(BusinessMessages.NomeOuDataNascimentoObrigatorios)
                .LessThan(DateTime.Today)
                .WithMessage(BusinessMessages.DataInvalida);
        }
    }
}