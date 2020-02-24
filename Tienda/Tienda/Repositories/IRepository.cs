using System.Collections.Generic;

namespace Tienda.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Create(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(TEntity entity);
    }
}
