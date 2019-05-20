using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GenericAPI.CrossCutting;
using GenericAPI.Services.Common;

namespace GenericAPI.Controllers.Common
{
    /// <summary>
    /// Controlador Base
    /// </summary>
    /// <typeparam name="TEntity">Classe</typeparam>
    [Authorize]
    public class BaseController<TEntity> : ApiController, IBaseController<TEntity> where TEntity : class
    {
        /// <summary>
        /// Lista os objetos de uma classe
        /// </summary>
        /// <param name="pagina">Pagina</param>
        /// <returns>objetos</returns>
        [AcceptVerbs("GET")]
        [ResponseType(typeof(HttpResponseMessage))]
        public virtual IHttpActionResult GetAll(int pagina = 1)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                BaseService<TEntity> service = new BaseService<TEntity>();

                var entidades = service.Todos(pagina);

                response = Request.CreateResponse(HttpStatusCode.OK, entidades);
                response.Headers.Add("X-Server-Message", $"{typeof(TEntity).Name}s listados.");
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                response = ExceptionFilter.Find(ref response, ex);
            }

            return ResponseMessage(response);
        }

        /// <summary>
        /// Busca pelo objeto de uma classe
        /// </summary>
        /// <param name="id">ID do elemento</param>
        /// <returns>objeto</returns>
        [AcceptVerbs("GET")]
        [ResponseType(typeof(HttpResponseMessage))]
        public virtual IHttpActionResult Get(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                BaseService<TEntity> service = new BaseService<TEntity>();

                var entidade = service.Buscar(id);

                response = Request.CreateResponse(HttpStatusCode.OK, entidade);
                response.Headers.Add("X-Server-Message", $"{typeof(TEntity).Name} encontrado.");

                return ResponseMessage(response);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                response = ExceptionFilter.Find(ref response, ex);
            }

            return ResponseMessage(response);
        }

        /// <summary>
        /// Cria um novo elemento de uma classe
        /// </summary>
        /// <param name="entidade">Elemento a ser criado</param>
        /// <returns>Objeto criado</returns>
        [AcceptVerbs("POST")]
        [ResponseType(typeof(HttpResponseMessage))]
        public virtual IHttpActionResult Post([FromBody]TEntity request)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                if (!ModelState.IsValid)
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    response.Headers.Add("X-Server-Message", "Formato ou inexistência de dados necessários a inserção em nossa base de dados.");

                    return ResponseMessage(response);

                }

                BaseService<TEntity> service = new BaseService<TEntity>();
                var entidade = service.Adicionar(request);

                response = Request.CreateResponse(HttpStatusCode.OK, entidade);
                response.Headers.Add("X-Server-Message", $"{typeof(TEntity).Name} Cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                response = ExceptionFilter.Find(ref response, ex);
            }

            return ResponseMessage(response);
        }

        /// <summary>
        /// Edita o objeto de uma classe
        /// </summary>
        /// <param name="id">ID do objeto</param>
        /// <param name="value">objeto a ser alterado</param>
        /// <returns>Objeto alterado</returns>
        [AcceptVerbs("PUT")]
        [ResponseType(typeof(HttpResponseMessage))]
        public virtual IHttpActionResult Put(int id, [FromBody]TEntity request)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                BaseService<TEntity> service = new BaseService<TEntity>();

                bool wasEdited = service.Editar(id, request);

                if (wasEdited)
                {
                    response = Request.CreateResponse(HttpStatusCode.NoContent);
                    response.Headers.Add("X-Server-Message", $"{typeof(TEntity).Name} editado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                response = ExceptionFilter.Find(ref response, ex);
            }

            return ResponseMessage(response);
        }

        /// <summary>
        /// Exclui o objeto de uma classe
        /// </summary>
        /// <param name="id">ID do objeto</param>
        /// <returns></returns>
        [AcceptVerbs("DELETE")]
        [ResponseType(typeof(HttpResponseMessage))]
        public virtual IHttpActionResult Delete(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                BaseService<TEntity> service = new BaseService<TEntity>();

                bool wasDeleted = service.Excluir(id);

                if (wasDeleted)
                {
                    response = Request.CreateResponse(HttpStatusCode.NoContent);
                    response.Headers.Add("X-Server-Message", $"{typeof(TEntity).Name} excluido com sucesso.");
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                response = ExceptionFilter.Find(ref response, ex);
            }

            return ResponseMessage(response);
        }
    }
}