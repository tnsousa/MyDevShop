using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    /// <summary>
    /// Classe responsável por manipular o Produto em memória
    /// </summary>
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }

        /// <summary>
        /// Oficializa as alterações no cache, atualizando os produtos na memória
        /// </summary>
        public void Commit()
        {
            cache["products"] = products;
        }

        /// <summary>
        /// Insere o Produto na memoria
        /// </summary>
        /// <param name="p">Produto a ser inserido</param>
        public void Insert(Product p)
        {
            products.Add(p);
        }

        /// <summary>
        /// Atualiza o produto na memória
        /// </summary>
        /// <param name="product">Produto a ser atualizado</param>
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        /// <summary>
        /// Obtém o Produto desejado, realizando uma consulta na memória em Cache
        /// </summary>
        /// <param name="Id">Id do Produto</param>
        /// <returns></returns>
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        /// <summary>
        /// Retorna uma lista que pode ser manipulada
        /// </summary>
        /// <returns></returns>
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
