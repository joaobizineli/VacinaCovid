using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacinaCovid.Entidade.Entidades;

namespace VacinaCovid.Repositorio.Interface.IRepositorios
{
    public interface IRepositorioBase<TEntidade> where TEntidade : class, IEntidade
    {
        Task<TEntidade> ObterPorId(object id);
        Task<List<TEntidade>> Listar();
        Task<TEntidade> Inserir(TEntidade entidade);
        Task<TEntidade> Atualizar(TEntidade entidade);
        Task Deletar(TEntidade entidade);
        Task DeletarPorId(object id);
    }
}
