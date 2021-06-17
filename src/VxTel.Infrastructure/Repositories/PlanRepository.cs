using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VxTel.Domain.Entities;
using VxTel.Domain.Interfaces.Repository;
using VxTel.Infrastructure.Context;

namespace VxTel.Infrastructure.Repositories
{
    public class PlanRepository : GenericRepository<Plan>, IPlanRepository
    {
        private readonly DbSet<Plan> _plan;

        public PlanRepository(VxTelDbContext context) : base(context) =>
            _plan = context.Set<Plan>();

        public async Task<bool> VerifyIfPlanNameExists(string name) =>
            await _plan.AnyAsync(p => p.Name == name);
        }
}
