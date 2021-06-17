using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VxTel.Application.Exceptions;
using VxTel.Application.Interfaces.Application;
using VxTel.Application.ViewModels;
using VxTel.Domain.Entities;
using VxTel.Domain.Interfaces.Repository;

namespace VxTel.Application.Services
{
    public class PriceApplication : IPriceApplication
    {
        private readonly IPriceRepository _priceRepository;

        public PriceApplication(IPriceRepository priceRepository) =>
            _priceRepository = priceRepository;

        public async Task<PriceViewModel> Add(PriceViewModel entity)
        {
            if(await IsUnique(entity.Origin, entity.Destination))
                throw new ApiException("Já existe preço cadastrado com a origem e destino informados.");

            var price = new Price(
                   entity.Id,
                   entity.Origin,
                   entity.Destination,
                   entity.Charge,
                   entity.CreatedAt,
                   entity.UpdatedAt
                   );
            _priceRepository.Add(price);
            return new PriceViewModel(price);
        }

        public async Task<IEnumerable<PriceViewModel>> GetAll() =>
            (await _priceRepository.GetAll()).Select(x => new PriceViewModel(x));


        public async Task<PriceViewModel> GetById(Guid id) =>
            new PriceViewModel(await ReturnPriceIfFinded(id));

        public async Task Remove(Guid id) =>
            _priceRepository.Delete(await ReturnPriceIfFinded(id));


        public async Task<PriceViewModel> Update(Guid id, PriceViewModel entity)
        {
            var priceRepository = await ReturnPriceIfFinded(id);
            var price = new Price(
                    priceRepository.Id,
                    entity.Origin,
                    entity.Destination,
                    entity.Charge,
                    priceRepository.CreatedAt,
                    priceRepository.UpdatedAt
                    );
            _priceRepository.Update(price);
            return new PriceViewModel(price);
        }

        public async Task<Price> ReturnPriceIfFinded(Guid id)
        {
            var price = await _priceRepository.GetById(id);
            if (price == null)
                throw new ApiException("Não existe Preço com o Id informado.");
            return price;
        }

        public async Task<bool> IsUnique(string origin, string destination) =>
            await _priceRepository.VerifyIfPriceExists(origin, destination);
    }
}
