using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GenericAPI.Controllers.Common
{
    interface IBaseController<TEntity> where TEntity : class
    {
        IHttpActionResult GetAll(int pagina = 1);
        IHttpActionResult Get(int id);
        IHttpActionResult Post([FromBody]TEntity request);
        IHttpActionResult Put(int id, [FromBody]TEntity request);
        IHttpActionResult Delete(int id);
    }
}
