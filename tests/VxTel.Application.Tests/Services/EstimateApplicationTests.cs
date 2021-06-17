using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using VxTel.Application.Exceptions;
using VxTel.Application.Services;
using VxTel.Domain.Interfaces.Repository;
using Xunit;

namespace VxTel.Application.Tests.Services
{
    [Collection(nameof(TestCollection))]
    public class EstimateApplicationTests
    {
        private readonly TestsFixtures _testsFixtures;

        public EstimateApplicationTests(TestsFixtures testsFixtures)
        {
            _testsFixtures = testsFixtures;
        }

        [Fact(DisplayName = "Verify if price exists based on origin and destination before calculate")]
        [Trait("Category", "Estimate Application")]
        public async Task EstimateApplication_VerifyIfPriceExists_ShouldReturnApiException()
        {
            // Arrange
            var mock = new AutoMocker();
            var estimateApplication = mock.CreateInstance<EstimateApplication>();
            var priceFixture = _testsFixtures.GenerateNewPrice();
            mock.GetMock<IPriceRepository>().Setup(pr => pr.VerifyIfPriceExists(priceFixture.Origin, priceFixture.Destination))
                                            .Returns(Task.FromResult(false));
            // Act
            var estimateException = await Assert.ThrowsAsync<ApiException>(() =>
            estimateApplication.EstimatePrice(priceFixture.Origin, priceFixture.Destination, 20, Guid.NewGuid()));
            
            // Assert
            Assert.Equal("Não existe preço cadastrado para a origem e destino informados.", estimateException.Message);
        }
    }
}
