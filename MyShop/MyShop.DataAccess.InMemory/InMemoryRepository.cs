using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        readonly ObjectCache cache = MemoryCache.Default;
        readonly string className;
        List<T> items;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = items.Find(p => p.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        /// <summary>
        /// Obtém o Produto desejado, realizando uma consulta na memória em Cache
        /// </summary>
        /// <param name="Id">Id do Produto</param>
        /// <returns></returns>
        public T Find(string Id)
        {
            T t = items.Find(p => p.Id == Id);

            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        /// <summary>
        /// Retorna uma lista que pode ser manipulada
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = items.Find(p => p.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
    }
}
