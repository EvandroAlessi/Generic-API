using GenericAPI.DAL;
using GenericAPI.Services.Common;
using GenericAPI.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericAPI.Services.Common
{
    /// <summary>
    /// Regras de negocio gerais
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adiciona a entidade enviada ao banco de dados
        /// </summary>
        /// <param name="request">entidade</param>
        /// <returns>entidade</returns>
        public virtual TEntity Adicionar(TEntity request)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var entidade = contexto.Set<TEntity>().Add(request);
                    contexto.SaveChanges();

                    return entidade;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca as entidades
        /// </summary>
        /// <returns>Lista de entidades</returns>
        public virtual IList<TEntity> Todos(int pagina = 1, int take = 10)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var entidades = contexto.Set<TEntity>().OrderBy(x => (typeof(TEntity).Name + "ID")).Skip((pagina - 1) * take).Take(take).ToList();

                    if (entidades == null || entidades.Count == 0)
                    {
                        throw new NotFindException("Não existem elementos a serem listados.");
                    }

                    return entidades;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca uma entidade pelo ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade</returns>
        public virtual TEntity Buscar(int id)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var entidade = contexto.Set<TEntity>().Find(id);

                    if (entidade == null)
                    {
                        throw new NotFindException("Elemento não encontrado.");
                    }

                    return entidade;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Edita uma entidade
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <param name="request">Entidade</param>
        /// <returns>Entidade editada</returns>
        public virtual bool Editar(int id, TEntity request)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var entidade = contexto.Set<TEntity>().Find(id);

                    if (entidade == null)
                    {
                        throw new NotFindException("Elemento não encontrado.");
                    }

                    UpdateData.Change(ref entidade, request);

                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Exclui uma entidade
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade excluida</returns>
        public virtual bool Excluir(int id)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var entidade = contexto.Set<TEntity>().Find(id);

                    if (entidade == null)
                    {
                        throw new NotFindException("Elemento não encontrado.");
                    }

                    contexto.Set<TEntity>().Remove(entidade);
                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}