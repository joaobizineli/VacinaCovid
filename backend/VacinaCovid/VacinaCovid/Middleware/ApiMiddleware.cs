using log4net;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;
using VacinaCovid.Repositorio.Interface;
using VacinaCovid.Utilitarios.Excepetions;
using VacinaCovid.Utilitarios.Exceptions;
using VacinaCovid.Utilitarios.Messages;
using VacinaCovid.Utilitarios.Response;

namespace VacinaCovid.Middleware
{
    public class ApiMiddleware : IMiddleware
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ApiMiddleware));
        private readonly IGerenciadorTransacao _gerenciadorTransacao;

        public ApiMiddleware(IGerenciadorTransacao gerenciadorTransacao)
        {
            _gerenciadorTransacao = gerenciadorTransacao;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            try
            {
                await _gerenciadorTransacao.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
                
                await next.Invoke(context);
                
                await _gerenciadorTransacao.CommitTransactionsAsync();

                stopwatch.Stop();
                _log.InfoFormat("Serviço exevutado com sucesso: {0} {1} {2}ms", context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                await _gerenciadorTransacao.RollbackTransactionsAsync();

                stopwatch.Stop();
                _log.Error($"Erro no serviço {context.Request.Path} / Mensagens: {ex.Message} [{stopwatch.ElapsedMilliseconds}]");
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            var messages = new List<string>();

            switch (ex)
            {
                case BusinessException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    messages.Add(ex.Message);
                    break;

                case BusinessListException: 
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    messages = ((BusinessListException)ex).Messages;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    messages.Add(InfraMessages.ErroInesperado);
                    break;
            }

            await response.WriteAsync(JsonConvert.SerializeObject(new DefaultResponse(HttpStatusCode.InternalServerError, messages)));
        }
    }
}