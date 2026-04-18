using FluentValidation;
using VacinaCovid.Entidade.Model;
using VacinaCovid.Utilitarios.Messages;

namespace VacinaCovid.Validador.Fluent
{
    public class VacinaPacienteValidator : AbstractValidator<VacinaPacienteModel>
    {
        public VacinaPacienteValidator()
        {
            RuleFor(x => x.IdPaciente)
                .GreaterThan(0)
                .WithMessage(BusinessMessages.IdPacienteObrigatorio);

            RuleFor(x => x.IdVacina)
                .GreaterThan(0)
                .WithMessage(BusinessMessages.IdVacinaObrigatoria);

            RuleFor(x => x.DataVacinacao)
                .NotEmpty()
                .WithMessage(BusinessMessages.DataVacinacaoObrigatoria);

            RuleFor(x => x.HoraVacinacao)
                .NotEmpty()
                .WithMessage(BusinessMessages.HoraVacinacaoObrigatoria);

            RuleFor(x => x.LocalVacinacao)
                .NotEmpty()
                .WithMessage(BusinessMessages.LocalVacinacaoObrigatorio)
                .MinimumLength(3)
                .WithMessage(BusinessMessages.LocalVacinacaoMinimo)
                .MaximumLength(100)
                .WithMessage(BusinessMessages.LocalVacinacaoMaximo);
        }
    }
}