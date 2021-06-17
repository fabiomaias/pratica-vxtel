using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using VxTel.Application.ModelsRequests;
using VxTel.Domain.Interfaces.Repository;

namespace VxTel.Application.Validators
{
    public class EstimateRequestValidator : AbstractValidator<EstimateRequest>
    {
        public EstimateRequestValidator()
        {
            RuleFor(e => e.Origin)
                .NotEmpty().WithMessage("O campo origem deve possuir um valor.")
                .Length(3, 3).WithMessage("O campo origem deve conter três caracteres.");

            RuleFor(e => e.Destination)
                .NotEmpty().WithMessage("O campo destino deve possuir um valor.")
                .Length(3, 3).WithMessage("O campo destino deve conter três caracteres.");

            RuleFor(e => e.Time)
                .NotEmpty().WithMessage("O campo tempo deve ser preenchido.")
                .GreaterThan(0).WithMessage("O campo tempo de ser inteiro maior que 0.");

            RuleFor(e => e.PlanId)
                .NotEmpty().WithMessage("O plano deve ser informado.");
        }
    }
}
