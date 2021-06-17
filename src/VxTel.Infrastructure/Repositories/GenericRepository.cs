using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VxTel.Domain.Common;
using VxTel.Domain.Interfaces;
using VxTel.Infrastructure.Context;

namespace VxTel.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseTraceable
    {
        private readonly VxTelDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(VxTelDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.AddAsync(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAll() => await _dbSet.ToListAsync();

        public async Task<T> GetById(Guid id) =>
            await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
    }
}
