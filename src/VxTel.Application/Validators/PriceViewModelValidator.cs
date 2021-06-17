using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using VxTel.Application.ViewModels;
using VxTel.Domain.Interfaces.Repository;

namespace VxTel.Application.Validators
{
    public class PriceViewModelValidator : AbstractValidator<PriceViewModel>
    {
        public PriceViewModelValidator()
        {

            RuleFor(p => p.Origin)
                .NotEmpty().WithMessage("A origem deve ser informada.")
                .Length(3, 3).WithMessage("A origem deve possuir três caracteres. Ex: 011");
            RuleFor(p => p.Destination)
                .NotEmpty().WithMessage("O destino deve ser informado.")
                .Length(3, 3).WithMessage("O destino deve possuir três caracteres.Ex: 017");
            RuleFor(p => p.Charge)
                .NotEmpty().WithMessage("O valor da cobrança deve ser informado.")
                .GreaterThan(0).WithMessage("O valor da cobrança deve ser um valor maior que 0");
        }
    }
}
