using NUnit.Framework;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Repositorio.Repositorio;

namespace VacinaCovid.TestesUnitario.BancoEmMemoria
{
    public class PacienteTesteBD : TesteUnitarioBase
    {
        private PacienteRepositorio _repositorio;

        [SetUp]
        public void Setup()
        {
            Contexto.Pacientes.RemoveRange(Contexto.Pacientes);
            Contexto.SaveChanges();

            _repositorio = new PacienteRepositorio(Contexto);
        }

        [Test]
        public async Task Inserir_DeveSalvarNoBanco()
        {
            var paciente = new Paciente { Nome = "João", DataNascimento = new DateTime(1990, 1, 1) };

            await _repositorio.Inserir(paciente);

            var resultado = await _repositorio.ObterPorId(paciente.Id);

            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Nome, Is.EqualTo("João"));
        }

        [Test]
        public async Task ObterPorId_DeveRetornarNulo_QuandoNaoExiste()
        {
            var resultado = await _repositorio.ObterPorId(999);

            Assert.That(resultado, Is.Null);
        }

        [Test]
        public async Task Listar_DeveRetornarTodos()
        {
            Contexto.Pacientes.AddRange(
                new Paciente { Nome = "João", DataNascimento = new DateTime(1990, 1, 1) },
                new Paciente { Nome = "Maria", DataNascimento = new DateTime(1985, 6, 15) }
            );
            Contexto.SaveChanges();

            var lista = await _repositorio.Listar();

            Assert.That(lista.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Atualizar_DeveAlterarNoBanco()
        {
            var paciente = new Paciente { Nome = "João", DataNascimento = new DateTime(1990, 1, 1) };
            Contexto.Pacientes.Add(paciente);
            Contexto.SaveChanges();

            paciente.Nome = "João Atualizado";
            await _repositorio.Atualizar(paciente);

            var resultado = await _repositorio.ObterPorId(paciente.Id);

            Assert.That(resultado.Nome, Is.EqualTo("João Atualizado"));
        }

        [Test]
        public async Task Deletar_DeveRemoverDoBanco()
        {
            var paciente = new Paciente { Nome = "João", DataNascimento = new DateTime(1990, 1, 1) };
            Contexto.Pacientes.Add(paciente);
            Contexto.SaveChanges();

            await _repositorio.Deletar(paciente);

            var resultado = await _repositorio.ObterPorId(paciente.Id);

            Assert.That(resultado, Is.Null);
        }

        [Test]
        public async Task DeletarPorId_DeveRemoverDoBanco()
        {
            var paciente = new Paciente { Nome = "Maria", DataNascimento = new DateTime(1985, 6, 15) };
            Contexto.Pacientes.Add(paciente);
            Contexto.SaveChanges();

            await _repositorio.DeletarPorId(paciente.Id);

            var resultado = await _repositorio.ObterPorId(paciente.Id);

            Assert.That(resultado, Is.Null);
        }

        [Test]
        public async Task DeletarPorId_NaoDeveErrar_QuandoNaoExiste()
        {
            async Task action() => await _repositorio.DeletarPorId(999);

            Assert.DoesNotThrowAsync(action);
        }
    }
}