using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VxTel.Domain.Entities;
using VxTel.Domain.Interfaces.Repository;
using VxTel.Infrastructure.Context;

namespace VxTel.Infrastructure.Repositories
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        private readonly DbSet<Price> _dbSet;

        public PriceRepository(VxTelDbContext context) : base(context) =>
            _dbSet = context.Set<Price>();

        public async Task<Price> GetPriceToCall(string origin, string destination) => 
            await _dbSet.SingleOrDefaultAsync(x => x.Origin == origin && x.Destination == destination);

        public async Task<bool> VerifyIfPriceExists(string origin, string destination) =>
            await _dbSet.AnyAsync(p => p.Origin == origin && p.Destination == destination);
    }
}
