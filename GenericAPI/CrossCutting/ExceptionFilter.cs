using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace GenericAPI.CrossCutting
{
    /// <summary>
    /// Tratamento de Exceções
    /// </summary>
    internal class ExceptionFilter
    {
        /// <summary>
        /// Busca o tipo da exceção gerada
        /// </summary>
        /// <param name="response"></param>
        /// <returns>Tipo da exceção gerada e mensagem</returns>
        public static HttpResponseMessage Find(ref HttpResponseMessage response, dynamic ex)
        {
            switch (ex)
            {
                case NotFindException exception:
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Headers.Add("X-Server-Message", exception.Message);
                    break;
                case NullReferenceException exception:
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Headers.Add("X-Server-Message", "Referencia a objeto inexistente.");
                    break;
                case AccessViolationException exception:
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.Headers.Add("X-Server-Message", "tentativa de ler ou gravar a memória protegida.");
                    break;
                case ArgumentNullException exception:
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Headers.Add("X-Server-Message", "Tentativa de acesso a metódo com parametro nulo.");
                    break;
                case FormatException exception:
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Headers.Add("X-Server-Message", "Formato do argumento enviado Inválido.");
                    break;
                case IndexOutOfRangeException exception:
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Headers.Add("X-Server-Message", "tentativa de acessar um elemento de uma matriz ou coleção com um índice que está fora dos limites.");
                    break;
                case DBConcurrencyException exception:
                    response.Headers.Add("X-Server-Message", "Nada foi alterado.");
                    break;
                case SqlException exception:
                    response.Headers.Add("X-Server-Message", "Erro retornado pelo SQL. Fale com o Administrador.");
                    break;
                case DbEntityValidationException exception:
                    List<DbValidationError> validationErrors = new List<DbValidationError>();

                    foreach (var item in exception.EntityValidationErrors)
                    {
                        validationErrors.AddRange(item.ValidationErrors);
                    }

                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Headers.Add("X-Server-Message", validationErrors.Find(x => x.ErrorMessage != null).ErrorMessage);
                    break;
                case DbUpdateException exception:
                    response.Headers.Add("X-Server-Message", "Erro ao salvar dados.");
                    break;
                case ArgumentException exception:
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Headers.Add("X-Server-Message", "Argumentos fornecidos para um método não é válido.");
                    break;
                default:
                    response.Headers.Add("X-Server-Message", ex.Message);
                    break;
            }

            return response;
        }
    }
}