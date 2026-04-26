
using System.Data;

namespace VacinaCovid.Repositorio.Interface
{
        public interface IGerenciadorTransacao
        {
            Task BeginTransactionAsync(IsolationLevel isolationLevel);
            Task CommitTransactionsAsync();
            Task RollbackTransactionsAsync();
        }
}
