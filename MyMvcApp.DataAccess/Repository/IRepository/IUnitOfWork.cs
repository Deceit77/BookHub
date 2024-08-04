using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MyMvcApp.Models;
using MyMvcApp.DataAccess.Repository;
using System.Threading.Tasks;
namespace MyMvcApp.DataAccess.Repository.IRepository
{
public interface IUnitOfWork
    {
    ICategoryRepository Category { get;}
     IProductRepository Product { get;}
    void Save();
    } 
}