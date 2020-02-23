using System.Collections.Generic;

namespace Tienda.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> ObtenerTodos();
        TEntity ObtenerPorId(int id);
        void Crear(TEntity entity);
        void Actualizar(int id, TEntity entity);
        void Eliminar(TEntity entity);
    }
}
