using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VxTel.Application.Interfaces
{
    public interface IGenericApplication<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(Guid id, T entity);
        Task Remove(Guid id);

        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
    }
}
