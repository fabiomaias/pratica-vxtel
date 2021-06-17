using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VxTel.Domain.Entities;
using Xunit;

namespace VxTel.Domain.Tests.Entities
{
    public class PriceTests
    {
        [Theory(DisplayName = "Calculate Call Price Without Plan")]
        [Trait("Category", "Plan Calculate Call")]
        [InlineData("011", "016", 1.90, 20, 38)]
        [InlineData("011", "017", 1.70, 80, 136)]
        [InlineData("018", "011", 1.90, 200, 380)]
        public void Price_CalculateCallPriceWithoutPlan_ShouldReturnCorrectValue(string origin, 
            string destination, double pricePlan, int time, double resultCalculated)
        {
            // Arrange
            var price = new Price(Guid.NewGuid(), origin, destination, pricePlan, DateTime.Now, null);

            // Act
            var result = price.CalculateCall(time);

            // Assert
            Assert.Equal(resultCalculated, result);
        }
    }
}
