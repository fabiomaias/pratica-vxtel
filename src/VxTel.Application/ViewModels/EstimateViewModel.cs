using VxTel.Domain.Entities;

namespace VxTel.Application.ViewModels
{
    public class EstimateViewModel
    {
        public EstimateViewModel(Plan plan, Price price, int time)
        {
            PriceWithPlan = plan.CalculateCall(price, time);
            PriceWithoutPlan = price.CalculateCall(time);
        }

        public double PriceWithPlan { get; set; }
        public double PriceWithoutPlan { get; set; }
    }
}
