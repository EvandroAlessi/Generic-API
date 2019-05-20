using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPI.Services.Common
{
    public interface IBaseService<TEntidade> where TEntidade : class
    {
        IList<TEntidade> Todos(int pagina = 1, int take = 10);
        TEntidade Buscar(int id);
        TEntidade Adicionar(TEntidade entidade);
        bool Editar(int id, TEntidade entidade);
        bool Excluir(int id);
    }
}
