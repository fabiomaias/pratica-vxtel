using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VxTel.Domain.Common;

namespace VxTel.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseTraceable
    {
        Task<T> GetById(Guid id);
        Task<IReadOnlyList<T>> GetAll();

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
