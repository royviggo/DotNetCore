using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNetCore.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        TEntity GetById(int id);
        TEntity GetById(string id);

        EntityState GetEntityState(TEntity entity);
        void SetEntityState(TEntity entity, EntityState state);
        TEntity GetOriginal(TEntity entity);

        IEnumerable<TEntity> GetAll();
    }
}