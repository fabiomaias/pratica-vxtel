using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VxTel.Application.Exceptions;
using VxTel.Application.Interfaces.Application;
using VxTel.Application.ViewModels;
using VxTel.Domain.Entities;
using VxTel.Domain.Interfaces.Repository;

namespace VxTel.Application.Services
{
    public class PlanApplication : IPlanApplication
    {
        private readonly IPlanRepository _planRepository;

        public PlanApplication(IPlanRepository planRepository) =>
            _planRepository = planRepository;

        public async Task<PlanViewModel> Add(PlanViewModel entity)
        {
            if (await IsUniqueName(entity.Name))
                throw new ApiException("Já existe plano cadastrado com este nome.");

            var plan = new Plan(
                    entity.Id,
                    entity.Name,
                    entity.Minutes,
                    entity.CreatedAt,
                    entity.UpdatedAt
                    );
            _planRepository.Add(plan);
            return new PlanViewModel(plan);
        }

        public async Task<IEnumerable<PlanViewModel>> GetAll() =>
            (await _planRepository.GetAll()).Select(x => new PlanViewModel(x));


        public async Task<PlanViewModel> GetById(Guid id) =>
            new PlanViewModel(await ReturnPlanIfFinded(id));

        public async Task Remove(Guid id) =>
            _planRepository.Delete(await ReturnPlanIfFinded(id));
       
               
        public async Task<PlanViewModel> Update(Guid id, PlanViewModel entity)
        {
            var planRepository = await ReturnPlanIfFinded(id);
            var plan = new Plan(
                    planRepository.Id,
                    entity.Name,
                    entity.Minutes,
                    planRepository.CreatedAt,
                    planRepository.UpdatedAt
                    );
            _planRepository.Update(plan);
            return new PlanViewModel(plan);
        }

        public async Task<Plan> ReturnPlanIfFinded(Guid id)
        {
            var plan = await _planRepository.GetById(id);
            if (plan == null)
                throw new ApiException("O Plano com o Id informado não existe.");
            return plan;
        }

        public async Task<bool> IsUniqueName(string name) =>
            await _planRepository.VerifyIfPlanNameExists(name);

    }
}
