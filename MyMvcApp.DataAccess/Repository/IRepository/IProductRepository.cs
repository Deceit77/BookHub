using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MyMvcApp.Models;
using System.Threading.Tasks;
namespace MyMvcApp.DataAccess.Repository.IRepository
{
public interface IProductRepository: IRepository<Product>
    {
        void Update(Product obj);
       

    }
}