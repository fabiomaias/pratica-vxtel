using System;
using System.Threading.Tasks;
using VxTel.Application.Exceptions;
using VxTel.Application.Interfaces.Application;
using VxTel.Application.ViewModels;
using VxTel.Domain.Interfaces.Repository;

namespace VxTel.Application.Services
{
    public class EstimateApplication : IEstimateApplication
    {
        private readonly IPlanRepository _planRepository;
        private readonly IPriceRepository _priceRepository;

        public EstimateApplication(IPlanRepository planRepository, IPriceRepository priceRepository)
        {
            _planRepository = planRepository;
            _priceRepository = priceRepository;
        }

        public async Task<EstimateViewModel> EstimatePrice(string origin, string destination, int time, Guid planId)
        {
            if (!await VerifyPrice(origin, destination))
                throw new ApiException("Não existe preço cadastrado para a origem e destino informados.");

            var price = await _priceRepository.GetPriceToCall(origin, destination);
            var plan = await _planRepository.GetById(planId);
            return new EstimateViewModel(plan, price, time);
        }

        private async Task<bool> VerifyPrice(string origin, string destination) =>
            await _priceRepository.VerifyIfPriceExists(origin, destination);
    }
}
