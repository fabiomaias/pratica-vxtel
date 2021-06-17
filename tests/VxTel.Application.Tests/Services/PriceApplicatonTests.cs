using Moq;
using System.Threading.Tasks;
using VxTel.Application.Exceptions;
using VxTel.Application.Services;
using VxTel.Application.ViewModels;
using VxTel.Domain.Entities;
using VxTel.Domain.Interfaces.Repository;
using Xunit;

namespace VxTel.Application.Tests.Services
{
    [Collection(nameof(TestCollection))]
    public class PriceApplicationTests
    {
        private readonly TestsFixtures _testsFixtures;

        public PriceApplicationTests(TestsFixtures testsFixtures)
        {
            _testsFixtures = testsFixtures;
        }

        [Fact(DisplayName = "Add Price With Success")]
        [Trait("Category", "Price Application")]
        public async Task PriceApplication_Add_ShouldSuccess()
        {
            // Arrange
            var priceRepository = new Mock<IPriceRepository>();
            var price = _testsFixtures.GenerateNewPrice();
            var priceApplication = new PriceApplication(priceRepository.Object);

            // Act
            await priceApplication.Add(new PriceViewModel(price));

            // Asset
            priceRepository.Verify(pr => pr.Add(It.IsAny<Price>()), Times.Once);
        }

        [Fact(DisplayName = "Get All Prices With Success")]
        [Trait("Category", "Price Application")]
        public async Task PriceApplication_GetAll_ShouldSuccess()
        {
            //Arrange             
            var priceRepository = new Mock<IPriceRepository>();
            priceRepository.Setup(r => r.GetAll()).Returns(Task.FromResult(_testsFixtures.GenerateNewPrices()));
            var priceApplication = new PriceApplication(priceRepository.Object);

            //Act
            var priceResult = await priceApplication.GetAll();

            //Assert
            Assert.NotNull(priceResult);
            priceRepository.Verify(pr => pr.GetAll(), Times.Once);
        }

        [Fact(DisplayName = "Get Price By Id With Success")]
        [Trait("Category", "Price Application")]
        public async Task PriceApplication_GetById_ShouldSuccess()
        {
            //Arrange             
            var priceRepository = new Mock<IPriceRepository>();
            var price = _testsFixtures.GenerateNewPrice();
            priceRepository.Setup(r => r.GetById(price.Id)).Returns(Task.FromResult(price));
            var priceApplication = new PriceApplication(priceRepository.Object);

            //Act
            var priceResult = await priceApplication.GetById(price.Id);

            //Assert
            Assert.NotNull(priceResult);
            priceRepository.Verify(pr => pr.GetById(price.Id), Times.Once);
        }

        [Fact(DisplayName = "Delete Price With Success")]
        [Trait("Category", "Price Application")]
        public async Task PriceApplication_Delete_ShouldSuccess()
        {
            //Arrange             
            var priceRepository = new Mock<IPriceRepository>();
            var price = _testsFixtures.GenerateNewPrice();
            priceRepository.Setup(r => r.GetById(price.Id)).Returns(Task.FromResult(price));
            var priceApplication = new PriceApplication(priceRepository.Object);

            //Act
            await priceApplication.Remove(price.Id);

            //Assert
            priceRepository.Verify(pr => pr.Delete(price), Times.Once);
        }

        [Fact(DisplayName = "Update Price With Success")]
        [Trait("Category", "Price Application")]
        public async Task PriceApplication_Update_ShouldSuccess()
        {
            //Arrange             
            var priceRepository = new Mock<IPriceRepository>();
            var price = _testsFixtures.GenerateNewPrice();
            priceRepository.Setup(r => r.GetById(price.Id)).Returns(Task.FromResult(price));
            var priceApplication = new PriceApplication(priceRepository.Object);

            //Act
            await priceApplication.Update(price.Id, new PriceViewModel(price));

            //Assert
            priceRepository.Verify(pr => pr.Update(It.IsAny<Price>()), Times.Once);
        }

        [Fact(DisplayName = "Api Exception When Price Does Not Unique")]
        [Trait("Category", "Price Application")]
        public async Task PriceApplication_Add_ShouldApiExceptionWhenPriceDoesNotUnique()
        {
            // Arrange
            var priceRepository = new Mock<IPriceRepository>();
            var price = _testsFixtures.GenerateNewPrice();
            priceRepository.Setup(pr => pr.VerifyIfPriceExists(price.Origin, price.Destination)).Returns(Task.FromResult(true));
            var priceApplication = new PriceApplication(priceRepository.Object);

            // Act
            var priceException = await Assert.ThrowsAsync<ApiException>(() =>
                priceApplication.Add(new PriceViewModel(price)));

            // Asset
            Assert.Equal("Já existe preço cadastrado com a origem e destino informados.", priceException.Message);
        }

        [Fact(DisplayName = "Api Exception When Price Does Not Exist")]
        [Trait("Category", "Price Application")]
        public async Task PriceApplication_Add_ShouldApiExceptionWhenPriceDoesNotExist()
        {
            //Arrange
            var price = _testsFixtures.GenerateNewPrice();
            var priceRepository = new Mock<IPriceRepository>();

            priceRepository.Setup(pr => pr.GetById(price.Id)).Returns(Task.FromResult((Price)null));

            var priceApplication = new PriceApplication(priceRepository.Object);

            //Act
            var priceException = await Assert
                .ThrowsAsync<ApiException>(() => priceApplication.ReturnPriceIfFinded(price.Id));

            //Assert
            Assert.Equal("Não existe Preço com o Id informado.", priceException.Message);
        }
    }
}
