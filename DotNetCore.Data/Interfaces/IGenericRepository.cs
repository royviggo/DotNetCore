using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DotNetCore.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        TEntity GetById(int id);
        TEntity GetById(string id);

        EntityState GetEntityState(TEntity entity);
        void SetEntityState(TEntity entity, EntityState state);
        TEntity GetOriginal(TEntity entity);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetList<TResult>(Expression<Func<TEntity, bool>> predicate = null,
                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    }
}