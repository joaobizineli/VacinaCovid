using System;
using System.Collections.Generic;
using System.Data;
using VacinaCovid.Repositorio.Context;
using VacinaCovid.Repositorio.Interface;
using Microsoft.EntityFrameworkCore;

namespace VacinaCovid.Repositorio
{
    public class ControleTransacao
    {
        public class GerenciadorTransacao : IGerenciadorTransacao
        {
            private readonly VacinaCovidContext _contexto;

            public GerenciadorTransacao(VacinaCovidContext contexto)
            {
                _contexto = contexto;
            }

            public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
            {
                var activeTransaction = _contexto.Database.CurrentTransaction;
                if (activeTransaction == null)
                {
                    var connection = _contexto.Database.GetDbConnection();
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var transaction = await connection.BeginTransactionAsync(isolationLevel);
                    await _contexto.Database.UseTransactionAsync(transaction);
                }
            }

            public async Task CommitTransactionsAsync()
            {
                var contextHasChanges = _contexto.ChangeTracker.HasChanges();

                if (contextHasChanges)
                    await _contexto.SaveChangesAsync();

                var activeTransaction = _contexto.Database.CurrentTransaction;
                await activeTransaction.CommitAsync();
                await activeTransaction.DisposeAsync();
            }

            public async Task RollbackTransactionsAsync()
            {
                var activeTransaction = _contexto.Database.CurrentTransaction;
                if (activeTransaction != null)
                {
                    await activeTransaction.RollbackAsync();
                    await activeTransaction.DisposeAsync();
                }
            }
        }

    }
}
