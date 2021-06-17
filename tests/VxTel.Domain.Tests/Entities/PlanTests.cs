using System;
using VxTel.Domain.Entities;
using Xunit;

namespace VxTel.Domain.Tests.Entities
{
    public class PlanTests
    {
        [Theory(DisplayName = "Calculate Call Price With Plan")]
        [Trait("Category", "Plan Calculate Call")]
        [InlineData("011", "016", 1.90, 20, "FaleMais30", 30, 0)]
        [InlineData("011", "017", 1.70, 80, "FaleMais60", 60, 37.40)]
        [InlineData("018", "011", 1.90, 200, "FaleMais120", 120, 167.20)]
        public void Plan_CalculateCallPriceWithPlan_ShouldReturnCorrectValue(string origin, string destination, double pricePlan, 
            int time, string planName, int timeOfPlan, double resultCalculated)
        {
            // Arrange
            var plan = new Plan(Guid.NewGuid(), planName, timeOfPlan, DateTime.Now, null);
            var price = new Price(Guid.NewGuid(), origin, destination, pricePlan, DateTime.Now, null);

            // Act
            var result = plan.CalculateCall(price, time);

            // Assert
            Assert.Equal(resultCalculated, result);
        }
    }
}
