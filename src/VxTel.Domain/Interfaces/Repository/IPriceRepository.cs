using System.Threading.Tasks;
using VxTel.Domain.Entities;

namespace VxTel.Domain.Interfaces.Repository
{
    public interface IPriceRepository : IGenericRepository<Price>
    {
        Task<Price> GetPriceToCall(string origin, string destination);

        Task<bool> VerifyIfPriceExists(string origin, string destination);
    }
}
