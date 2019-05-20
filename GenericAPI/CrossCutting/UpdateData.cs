using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericAPI.CrossCutting
{
    /// <summary>
    /// Altera dados do Banco
    /// </summary>
    public class UpdateData
    {
        /// <summary>
        /// Efetua a modificação dos valores de cada propriedade de um objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v1">Objeto presente no Banco de dados</param>
        /// <param name="v2">Objeto enviado para alteração</param>
        /// <returns></returns>
        public static TEntity Change<TEntity>(ref TEntity v1, TEntity v2)
        {
            var propriedades = typeof(TEntity).GetProperties();

            foreach (var prop in propriedades)
            {
                var value = typeof(TEntity).GetProperty(prop.Name).GetValue(v2);

                if (prop.Name != typeof(TEntity).Name + "ID" && value != null)
                    prop.SetValue(v1, value);
            }

            return v1;
        }
    }
}