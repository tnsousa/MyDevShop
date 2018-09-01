using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        /// <summary>
        /// Oficializa as alterações no cache, atualizando os produtos na memória
        /// </summary>
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        /// <summary>
        /// Insere o Produto na memoria
        /// </summary>
        /// <param name="p">Produto a ser inserido</param>
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        /// <summary>
        /// Atualiza o produto na memória
        /// </summary>
        /// <param name="product">Produto a ser atualizado</param>
        public void Update(ProductCategory product)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == product.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = product;
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
        public ProductCategory Find(string Id)
        {
            ProductCategory product = productCategories.Find(p => p.Id == Id);

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
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}

