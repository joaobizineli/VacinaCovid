using Moq;
using NUnit.Framework;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.Enum;
using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Negocios.Negocios;
using VacinaCovid.Repositorio.Interface.IRepositorios;
using VacinaCovid.Utilitarios.Exceptions;
using VacinaCovid.Utilitarios.Messages;

namespace VacinaCovid.TestesUnitario.Mock
{
    public class VacinaPacienteTesteMock : TesteUnitarioBase
    {
        private IVacinaPacienteNegocio _negocio;
        private Mock<IVacinaPacienteRepositorio> _mockRepositorio;
        private Mock<IPacienteRepositorio> _mockPacienteRepositorio;

        [SetUp]
        public void Setup()
        {
            _mockRepositorio = RegistrarMock<IVacinaPacienteRepositorio>();
            _mockPacienteRepositorio = RegistrarMock<IPacienteRepositorio>();

            Registrar<IVacinaPacienteNegocio, VacinaPacienteNegocio>();
            _negocio = ObterService<IVacinaPacienteNegocio>();
        }

        [Test]
        public void Inserir_Sucesso()
        {
            var data = DateTime.Today.AddDays(1);
            var hora = TimeSpan.FromHours(9);

            _mockPacienteRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Nome = "João", DataNascimento = new DateTime(1990, 1, 1) }));

            _mockRepositorio
                .Setup(r => r.ContagemPorData(data))
                .Returns(() => Task.FromResult(0));

            _mockRepositorio
                .Setup(r => r.ContagemPorDataEHora(data, hora))
                .Returns(() => Task.FromResult(0));

            _mockRepositorio
                .Setup(r => r.ListarPorData(data))
                .Returns(() => Task.FromResult(new List<VacinaPaciente>()));

            _mockRepositorio
                .Setup(r => r.Inserir(It.IsAny<VacinaPaciente>()))
                .Returns(() => Task.FromResult(new VacinaPaciente()));

            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new VacinaPaciente { Status = StatusEnum.Agendado }));

            async Task action() => await _negocio.AgendarVacina(1, 1, data, hora, "UBS Central");

            Assert.DoesNotThrowAsync(action);
        }

        [Test]
        public void Inserir_PacienteNaoEncontrado()
        {
            _mockPacienteRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult<Paciente>(null));

            async Task action() => await _negocio.AgendarVacina(1, 1, DateTime.Today.AddDays(1), TimeSpan.FromHours(9), "UBS");

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.PacienteNaoEncontrado);
        }

        [Test]
        public void Inserir_DataPassada()
        {
            _mockPacienteRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Nome = "Maria" }));

            async Task action() => await _negocio.AgendarVacina(1, 1, DateTime.Today.AddDays(-1), TimeSpan.FromHours(9), "UBS");

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.DataPassada);
        }

        [Test]
        public void Inserir_LimiteDiario20()
        {
            var data = DateTime.Today.AddDays(1);

            _mockPacienteRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Nome = "Carlos" }));

            _mockRepositorio
                .Setup(r => r.ContagemPorData(data))
                .Returns(() => Task.FromResult(20));

            async Task action() => await _negocio.AgendarVacina(1, 1, data, TimeSpan.FromHours(9), "UBS");

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.LimiteDiarioUltrapassado);
        }

        [Test]
        public void Inserir_LimiteHorario2()
        {
            var data = DateTime.Today.AddDays(1);
            var hora = TimeSpan.FromHours(10);

            _mockPacienteRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Nome = "Ana" }));

            _mockRepositorio
                .Setup(r => r.ContagemPorData(data))
                .Returns(() => Task.FromResult(5));

            _mockRepositorio
                .Setup(r => r.ContagemPorDataEHora(data, hora))
                .Returns(() => Task.FromResult(2));

            async Task action() => await _negocio.AgendarVacina(1, 1, data, hora, "UBS");

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.LimiteHorarioUltrapassado);
        }

        [Test]
        public void Inserir_IntervaloMenorQueUmaHora()
        {
            var data = DateTime.Today.AddDays(1);
            var hora = TimeSpan.FromHours(9);

            _mockPacienteRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new Paciente { Nome = "Pedro" }));

            _mockRepositorio
                .Setup(r => r.ContagemPorData(data))
                .Returns(() => Task.FromResult(3));

            _mockRepositorio
                .Setup(r => r.ContagemPorDataEHora(data, hora))
                .Returns(() => Task.FromResult(0));

            _mockRepositorio
                .Setup(r => r.ListarPorData(data))
                .Returns(() => Task.FromResult(new List<VacinaPaciente>
                {
                    new VacinaPaciente { HoraVacinacao = TimeSpan.FromMinutes(570) }
                }));

            async Task action() => await _negocio.AgendarVacina(1, 1, data, hora, "UBS");

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.IntervaloConflitante);
        }

        [Test]
        public void CancelarAgendamento_JaCancelado()
        {
            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new VacinaPaciente { Status = StatusEnum.Cancelado }));

            async Task action() => await _negocio.CancelarAgendamento(1);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.AgendamentoRealizadoCancel);
        }

        [Test]
        public void RemoverAgendamento_JaCancelado()
        {
            _mockRepositorio
                .Setup(r => r.ObterPorId(It.IsAny<object>()))
                .Returns(() => Task.FromResult(new VacinaPaciente { Status = StatusEnum.Cancelado }));

            async Task action() => await _negocio.RemoverAgendamento(1);

            var exception = Assert.ThrowsAsync<BusinessException>(action);

            Assert.IsTrue(exception.Message == BusinessMessages.AgendamentoRealizadoDelete);
        }
    }
}