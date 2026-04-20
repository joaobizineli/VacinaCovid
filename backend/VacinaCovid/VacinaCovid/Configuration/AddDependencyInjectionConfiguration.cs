using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Negocios.Negocios;
using VacinaCovid.Repositorio.Interface.IRepositorios;
using VacinaCovid.Repositorio.Repositorio;

namespace VacinaCovid.Configuration
{
    public static class AddDependencyInjectionConfiguration
    {

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            InjectRepository(services);
            InjectBusiness(services);
        }

        private static void InjectBusiness(IServiceCollection services)
        {
            services.AddScoped<IPacienteNegocio, PacienteNegocio>();
            services.AddScoped<IVacinaPacienteNegocio, VacinaPacienteNegocio>();
        }

        private static void InjectRepository(IServiceCollection services)
        {
            services.AddScoped<IPacienteRepositorio, PacienteRepositorio>();
            services.AddScoped<IVacinaPacienteRepositorio, VacinaPacienteRepositorio>();
        }
    }
}
