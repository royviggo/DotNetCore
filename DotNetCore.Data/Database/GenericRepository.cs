using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Data.Database
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext db)
        {
            _dbContext = db ?? throw new ArgumentNullException(nameof(db));
            _dbSet = db.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (GetEntityState(entity) == EntityState.Detached)
            {
                SetEntityState(entity, EntityState.Modified);
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Remove(entity);
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public TEntity GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public EntityState GetEntityState(TEntity entity)
        {
            return _dbContext.Entry(entity).State;
        }

        public void SetEntityState(TEntity entity, EntityState state)
        {
            _dbContext.Entry(entity).State = state;
        }

        public TEntity GetOriginal(TEntity entity)
        {
            var original = (TEntity)Activator.CreateInstance(typeof(TEntity));
            var type = typeof(TEntity);

            var values = _dbContext.Entry(entity).OriginalValues;

            foreach (var property in values.Properties)
            {
                var propertyInfo = type.GetProperty(property.Name);
                propertyInfo?.SetValue(original, values.GetValue<object>(property));
            }

            return original;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

    }
}