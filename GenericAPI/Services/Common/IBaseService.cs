using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPI.Services.Common
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IList<TEntity> FindAll(int pagina = 1, int take = 10);
        TEntity Find(int id);
        TEntity Add(TEntity entidade);
        bool Edit(int id, TEntity entidade);
        bool Delete(int id);
    }
}
