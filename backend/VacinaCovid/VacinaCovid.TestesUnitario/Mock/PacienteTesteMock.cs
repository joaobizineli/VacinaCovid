using Moq;
using NUnit.Framework;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Negocios.Negocios;
using VacinaCovid.Repositorio.Interface.IRepositorios;
using VacinaCovid.Utilitarios.Exceptions;
using VacinaCovid.Utilitarios.Messages;

namespace VacinaCovid.TestesUnitario.Mock
{
    public class PacienteTesteMock : TesteUnitarioBase
    {
        private IPacienteNegocio _negocio;
        private Mock<IPacienteRepositorio> _mockRepositorio;

        [SetUp]
        public void Setup()
        {
            _mockRepositorio = RegistrarMock<IPacienteRepositorio>();

            Registrar<IPacienteNegocio, PacienteNegocio>();
            _negocio = ObterService<IPacienteNegocio>();
        }

        [Test]
        public void InserirPaciente_Sucesso()
        {
            _mockRepositorio
                .Setup(r => r.Inserir(It.IsAny<Paciente>()))
                .Callback<Paciente>(p => p.Id = 1) // seta o Id no objeto original
                .Returns(() => Task.FromResult(new Paciente { Id = 1, Nome = "João", DataNascimento = new DateTime(1990, 1, 1) }));

            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Id = 1, Nome = "João", DataNascimento = new DateTime(1990, 1, 1) }));

            async Task action() => await _negocio.InserirPaciente("João", new DateTime(1990, 1, 1));

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void InserirPaciente_NomeVazio()
        {
            async Task action() => await _negocio.InserirPaciente("", new DateTime(1990, 1, 1));

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.NomeOuDataNascimentoObrigatorios);
        }

        [Test]
        public void InserirPaciente_DataNascimentoDefault()
        {
            async Task action() => await _negocio.InserirPaciente("João", default);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.NomeOuDataNascimentoObrigatorios);
        }

        [Test]
        public void ObterPaciente_NaoEncontrado()
        {
            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult<Paciente>(null));

            async Task action() => await _negocio.ObterPaciente(1);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.PacienteNaoEncontrado);
        }

        [Test]
        public void ObterPaciente_IdNegativo()
        {
            async Task action() => await _negocio.ObterPaciente(-1);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.IdNegativo);
        }

        [Test]
        public void RemoverPaciente_Sucesso()
        {
            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Nome = "João" }));

            _mockRepositorio
                .Setup(r => r.Deletar(It.IsAny<Paciente>()))
                .Returns(() => Task.CompletedTask);

            async Task action() => await _negocio.RemoverPaciente(1);

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void RemoverPaciente_NaoEncontrado()
        {
            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult<Paciente>(null));

            async Task action() => await _negocio.RemoverPaciente(1);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.PacienteNaoEncontrado);
        }

        [Test]
        public void InserirPaciente_DataNascimentoFutura()
        {
            async Task action() => await _negocio.InserirPaciente("João", DateTime.Today.AddDays(1));

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.DataInvalida);
        }

        [Test]
        public void AtualizarPaciente_NomeVazio()
        {
            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Id = 1, Nome = "João", DataNascimento = new DateTime(1990, 1, 1) }));

            async Task action() => await _negocio.AtualizarPaciente(1, "", new DateTime(1990, 1, 1));

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.NomeOuDataNascimentoObrigatorios);
        }

        [Test]
        public void AtualizarPaciente_DataNascimentoFutura()
        {
            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Id = 1, Nome = "João", DataNascimento = new DateTime(1990, 1, 1) }));

            async Task action() => await _negocio.AtualizarPaciente(1, "João", DateTime.Today.AddDays(1));

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.DataInvalida);

        }
    }
}