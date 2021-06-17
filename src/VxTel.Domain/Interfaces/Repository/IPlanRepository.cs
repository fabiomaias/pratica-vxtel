using System.Threading.Tasks;
using VxTel.Domain.Entities;

namespace VxTel.Domain.Interfaces.Repository
{
    public interface IPlanRepository : IGenericRepository<Plan>
    {
        Task<bool> VerifyIfPlanNameExists(string name);
     }
}
