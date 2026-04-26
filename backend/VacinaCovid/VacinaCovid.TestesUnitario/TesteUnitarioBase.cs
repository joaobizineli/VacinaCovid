using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using VacinaCovid.Repositorio.Context;

namespace VacinaCovid.TestesUnitario
{
    public class TesteUnitarioBase
    {
        private readonly IServiceCollection ServiceCollection = new ServiceCollection();
        protected VacinaCovidContext Contexto;

        protected Mock<T> RegistrarMock<T>() where T : class
        {
            var mock = new Mock<T>();
            ServiceCollection.AddSingleton(typeof(T), mock.Object);
            return mock;
        }

        protected void Registrar<I, T>() where I : class where T : class, I
        {
            ServiceCollection.AddSingleton<I, T>();
        }

        protected I ObterService<I>() where I : class
        => ServiceCollection.BuildServiceProvider().GetService<I>();

        protected void RegistrarObjeto(Type tipo, object objeto)
        {
            ServiceCollection.AddSingleton(tipo, objeto);
        }

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            ConfigureInMemoryDataBase();
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            Contexto.Dispose();
        }

        private void ConfigureInMemoryDataBase()
        {
            var options = new DbContextOptionsBuilder<VacinaCovidContext>()
                              .UseInMemoryDatabase("InMemoryDatabase")
            .Options;

            Contexto = new VacinaCovidContext(options);
            if (Contexto.Database.IsInMemory())
                Contexto.Database.EnsureDeleted();
        }

    }
}