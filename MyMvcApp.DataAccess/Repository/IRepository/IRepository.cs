using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MyMvcApp.Models;
using System.Threading.Tasks;
namespace MyMvcApp.DataAccess.Repository.IRepository
{
public interface IRepository<T> where T : class
{
    
    IEnumerable<T> GetAll(string? includeProperties =null);
    T Get(Expression<Func<T, bool>> filter,string? includeProperties =null);
    void Add(T entity);
      void Remove(T entity);
       void RemoveRange(IEnumerable<T> entity);


}


}