using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entites;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        T Get(Expression<Func<T, bool>> filter = null);
        T GetOne(Expression<Func<T, bool>> filter = null);
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        List<T> GetListLimited(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> order = null, int quantity = 15);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Counter();
    }
}
