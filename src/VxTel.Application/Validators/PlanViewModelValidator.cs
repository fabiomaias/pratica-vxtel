using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using VxTel.Application.ViewModels;
using VxTel.Domain.Interfaces.Repository;

namespace VxTel.Application.Validators
{
    public class PlanViewModelValidator : AbstractValidator<PlanViewModel>
    {
        public PlanViewModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O nome do plano é requerido.");
            RuleFor(p => p.Minutes)
                .NotEmpty().WithMessage("A quantidade de minutos é requerido.")
                .GreaterThan(0).WithMessage("O valor dos minutos deve ser um inteiro positivo maior que 0.");
        }
    }
}
