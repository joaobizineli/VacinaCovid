
using Microsoft.EntityFrameworkCore;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Repositorio.Context;
using VacinaCovid.Repositorio.Interface.IRepositorios;

namespace VacinaCovid.Repositorio.Repositorio
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : class, IEntidade
    {
        protected readonly VacinaCovidContext _contexto;
        protected virtual DbSet<TEntidade> EntitySet { get; set; }

        public RepositorioBase(VacinaCovidContext contexto)
        {
            _contexto = contexto;
            EntitySet = _contexto.Set<TEntidade>();
        }

        public async Task<TEntidade> ObterPorId(object id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task<List<TEntidade>> Listar()
        {
            return await EntitySet.ToListAsync();
        }

        public async Task<TEntidade> Inserir(TEntidade entidade)
        {
            var entityEntry = await EntitySet.AddAsync(entidade);
            await _contexto.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task Inserir(IEnumerable<TEntidade> entidades)
        {
            await EntitySet.AddRangeAsync(entidades);
            await _contexto.SaveChangesAsync();
        }

        public async Task<TEntidade> Atualizar(TEntidade entidade)
        {
            var entityEntry = EntitySet.Update(entidade);
            await _contexto.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task Deletar(TEntidade entidade)
        {
            EntitySet.Remove(entidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task Deletar(IEnumerable<TEntidade> entidades)
        {
            EntitySet.RemoveRange(entidades);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarPorId(object id)
        {
            var entity = await EntitySet.FindAsync(id);
            if (entity != null)
                await Deletar(entity);
        }
    }
}