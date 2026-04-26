using NUnit.Framework;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.Enum;
using VacinaCovid.Repositorio.Repositorio;

namespace VacinaCovid.TestesUnitario.BancoEmMemoria
{
    public class VacinaPacienteTesteBD : TesteUnitarioBase
    {
        private VacinaPacienteRepositorio _repositorio;

        [SetUp]
        public void Setup()
        {
            Contexto.VacinaPacientes.RemoveRange(Contexto.VacinaPacientes);
            Contexto.SaveChanges();

            _repositorio = new VacinaPacienteRepositorio(Contexto);
        }

        [Test]
        public async Task ContagemPorData_DeveContarSomenteAgendados()
        {
            var data = DateTime.Today.AddDays(1);

            Contexto.VacinaPacientes.AddRange(
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(9), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(10), Status = StatusEnum.Cancelado, LocalVacinacao = "UBS" },
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(11), Status = StatusEnum.Realizado, LocalVacinacao = "UBS" }
            );
            Contexto.SaveChanges();

            var contagem = await _repositorio.ContagemPorData(data);

            Assert.That(contagem, Is.EqualTo(1));
        }

        [Test]
        public async Task ContagemPorData_DeveIgnorarOutrasDatas()
        {
            var data = DateTime.Today.AddDays(1);
            var outraData = DateTime.Today.AddDays(2);

            Contexto.VacinaPacientes.AddRange(
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(9), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { DataVacinacao = outraData, HoraVacinacao = TimeSpan.FromHours(9), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" }
            );
            Contexto.SaveChanges();

            var contagem = await _repositorio.ContagemPorData(data);

            Assert.That(contagem, Is.EqualTo(1));
        }

        [Test]
        public async Task ContagemPorDataEHora_DeveContarSomenteHorarioExato()
        {
            var data = DateTime.Today.AddDays(1);
            var hora = TimeSpan.FromHours(9);

            Contexto.VacinaPacientes.AddRange(
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = hora, Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = hora, Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(10), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" }
            );
            Contexto.SaveChanges();

            var contagem = await _repositorio.ContagemPorDataEHora(data, hora);

            Assert.That(contagem, Is.EqualTo(2));
        }

        [Test]
        public async Task ListarPorData_DeveRetornarOrdenadoPorHora()
        {
            var data = DateTime.Today.AddDays(1);

            Contexto.VacinaPacientes.AddRange(
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(11), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(9), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(10), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" }
            );
            Contexto.SaveChanges();

            var lista = await _repositorio.ListarPorData(data);

            Assert.That(lista[0].HoraVacinacao, Is.EqualTo(TimeSpan.FromHours(9)));
            Assert.That(lista[1].HoraVacinacao, Is.EqualTo(TimeSpan.FromHours(10)));
            Assert.That(lista[2].HoraVacinacao, Is.EqualTo(TimeSpan.FromHours(11)));
        }

        [Test]
        public async Task ListarPorData_DeveIgnorarCancelados()
        {
            var data = DateTime.Today.AddDays(1);

            Contexto.VacinaPacientes.AddRange(
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(9), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(10), Status = StatusEnum.Cancelado, LocalVacinacao = "UBS" }
            );
            Contexto.SaveChanges();

            var lista = await _repositorio.ListarPorData(data);

            Assert.That(lista.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task ListarPorPaciente_DeveRetornarSomenteDosPaciente()
        {
            var data = DateTime.Today.AddDays(1);

            Contexto.VacinaPacientes.AddRange(
                new VacinaPaciente { IdPaciente = 1, DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(9), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { IdPaciente = 1, DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(10), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" },
                new VacinaPaciente { IdPaciente = 2, DataVacinacao = data, HoraVacinacao = TimeSpan.FromHours(11), Status = StatusEnum.Agendado, LocalVacinacao = "UBS" }
            );
            Contexto.SaveChanges();

            var lista = await _repositorio.ListarPorPaciente(1);

            Assert.That(lista.Count, Is.EqualTo(2));
            Assert.That(lista.All(v => v.IdPaciente == 1), Is.True);
        }
    }
}