using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core.Entites;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();

            }
        }

        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity GetOne(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public List<TEntity> GetListLimited(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> order = null, int quantity=15)
        {
            using (var context = new TContext())
            {
                List<TEntity> result = null;
                if (filter == null)
                {
                    if (order == null)
                    {
                        result = context.Set<TEntity>().Take(Convert.ToInt32(quantity)).ToList();
                    }
                    else
                    {
                        result = context.Set<TEntity>().OrderByDescending(order).Take(quantity).ToList();
                    }
                }
                else
                {
                    if (order == null)
                    {
                        result = context.Set<TEntity>().Where(filter).ToList();
                    }
                    else
                    {
                        result = context.Set<TEntity>().OrderByDescending(order).Where(filter).ToList();
                    }
                }

                return result;
            }
        }

        public int Counter()
        {
            int ret; 
            using (var context = new TContext())
            {
              ret= context.Set<TEntity>().Count();
                return ret;
            }
            return ret;
        }

    }
}
