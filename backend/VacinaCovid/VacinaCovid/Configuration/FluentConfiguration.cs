using FluentValidation;
using FluentValidation.AspNetCore;
using System.Runtime.CompilerServices;
using VacinaCovid.Validador.Fluent;

namespace VacinaCovid.Configuration
{
    public static class FluentConfiguration
    {
        public static void AddFluentConfiguration(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<VacinaPacienteValidator>();
            services.AddValidatorsFromAssemblyContaining<PacienteValidator>();

            services.AddFluentValidationAutoValidation();
        }
    }
}
