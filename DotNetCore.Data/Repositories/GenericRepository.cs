using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotNetCore.Data.Interfaces;

namespace DotNetCore.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IDisposable, new()
    {
        protected readonly IDbFactory _dbFactory;

        public GenericRepository(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        public void Dispose()
        {
            _dbFactory.Dispose();
        }

        public IDbFactory Db => _dbFactory;

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Db.Context().Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            Db.Context().Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Db.Context().Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Db.Context().Set<TEntity>().RemoveRange(entities);
        }

        public TEntity Get(int id)
        {
            return Db.Context().Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Db.Context().Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Context().Set<TEntity>().Where(predicate);
        }

    }
}