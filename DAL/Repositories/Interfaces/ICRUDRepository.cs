using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// Contrato genérico de servicio de acceso a datos
    /// </summary>
    public interface ICRUDRepository<T,Id>
    {
        /// <summary>
        /// Obtener entidad por su Identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Entidad</returns>
        public Task<T> Get(Id id);
        /// <summary>
        /// Obtener lista de objetos SIN paginación y SIN filtado
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public Task<List<T>> Get();
        /// <summary>
        /// Crear entidad
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <returns>Identificador</returns>
        public Task<T> Create(T entity);

        /// <summary>
        /// Actualizar entidad
        /// </summary>
        /// <param name="entity">Entidad</param>
        public Task Update(T entity);
        /// <summary>
        /// Eliminar entidad
        /// </summary>
        /// <param name="entity">Entidad</param>
        public Task Delete(T entity);

        /// <summary>
        /// Valida si existe la entidad
        /// </summary>
        /// <param name="entity">Entidad</param>
        public Task<bool> Exist(T entity);
    }
}