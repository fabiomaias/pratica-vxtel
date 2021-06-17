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
    public class PlanApplicationTests
    {
        private readonly TestsFixtures _testsFixtures;

        public PlanApplicationTests(TestsFixtures testsFixtures)
        {
            _testsFixtures = testsFixtures;
        }

        [Fact(DisplayName = "Add Plan With Success")]
        [Trait("Category", "Plan Application")]
        public async Task PlanApplication_Add_ShouldSuccess()
        {
            // Arrange
            var planRepository = new Mock<IPlanRepository>();
            var plan = _testsFixtures.GenerateNewPlan();
            var planApplication = new PlanApplication(planRepository.Object);

            // Act
            await planApplication.Add(new PlanViewModel(plan));

            // Asset
            planRepository.Verify(pr => pr.Add(It.IsAny<Plan>()), Times.Once);
        }

        [Fact(DisplayName = "Get All Plans With Success")]
        [Trait("Category", "Plan Application")]
        public async Task PlanApplication_GetAll_ShouldSuccess()
        {
            //Arrange             
            var planRepository = new Mock<IPlanRepository>();
            planRepository.Setup(r => r.GetAll()).Returns(Task.FromResult(_testsFixtures.GenerateNewPlans()));
            var planApplication = new PlanApplication(planRepository.Object);

            //Act
            var planResult = await planApplication.GetAll();

            //Assert
            Assert.NotNull(planResult);
            planRepository.Verify(pr => pr.GetAll(), Times.Once);
        }

        [Fact(DisplayName = "Get Plan By Id With Success")]
        [Trait("Category", "Plan Application")]
        public async Task PlanApplication_GetById_ShouldSuccess()
        {
            //Arrange             
            var planRepository = new Mock<IPlanRepository>();
            var plan = _testsFixtures.GenerateNewPlan();
            planRepository.Setup(r => r.GetById(plan.Id)).Returns(Task.FromResult(plan));
            var planApplication = new PlanApplication(planRepository.Object);

            //Act
            var planResult = await planApplication.GetById(plan.Id);

            //Assert
            Assert.NotNull(planResult);
            planRepository.Verify(pr => pr.GetById(plan.Id), Times.Once);
        }

        [Fact(DisplayName = "Delete Plan With Success")]
        [Trait("Category", "Plan Application")]
        public async Task PlanApplication_Delete_ShouldSuccess()
        {
            //Arrange             
            var planRepository = new Mock<IPlanRepository>();
            var plan = _testsFixtures.GenerateNewPlan();
            planRepository.Setup(r => r.GetById(plan.Id)).Returns(Task.FromResult(plan));
            var planApplication = new PlanApplication(planRepository.Object);

            //Act
            await planApplication.Remove(plan.Id);

            //Assert
            planRepository.Verify(pr => pr.Delete(plan), Times.Once);
        }

        [Fact(DisplayName = "Update Plan With Success")]
        [Trait("Category", "Plan Application")]
        public async Task PlanApplication_Update_ShouldSuccess()
        {
            //Arrange             
            var planRepository = new Mock<IPlanRepository>();
            var plan = _testsFixtures.GenerateNewPlan();
            planRepository.Setup(r => r.GetById(plan.Id)).Returns(Task.FromResult(plan));
            var planApplication = new PlanApplication(planRepository.Object);

            //Act
            await planApplication.Update(plan.Id, new PlanViewModel(plan));

            //Assert
            planRepository.Verify(pr => pr.Update(It.IsAny<Plan>()), Times.Once);
        }

        [Fact(DisplayName = "Api Exception When Plan Does Not Unique")]
        [Trait("Category", "Plan Application")]
        public async Task PlanApplication_Add_ShouldApiExceptionWhenPlanDoesNotUnique()
        {
            // Arrange
            var planRepository = new Mock<IPlanRepository>();
            var plan = _testsFixtures.GenerateNewPlan();
            planRepository.Setup(pr => pr.VerifyIfPlanNameExists(plan.Name)).Returns(Task.FromResult(true));
            var planApplication = new PlanApplication(planRepository.Object);

            // Act
            var planException = await Assert.ThrowsAsync<ApiException>(() =>
                planApplication.Add(new PlanViewModel(plan)));

            // Asset
            Assert.Equal("Já existe plano cadastrado com este nome.", planException.Message);
        }

        [Fact(DisplayName = "Api Exception When Plan Does Not Exist")]
        [Trait("Category", "Plan Application")]
        public async Task PlanApplication_Add_ShouldApiExceptionWhenPlanDoesNotExist()
        {
            //Arrange
            var plan = _testsFixtures.GenerateNewPlan();
            var planRepository = new Mock<IPlanRepository>();

            planRepository.Setup(pr => pr.GetById(plan.Id)).Returns(Task.FromResult((Plan)null));

            var planApplication = new PlanApplication(planRepository.Object);

            //Act
            var planException = await Assert
                .ThrowsAsync<ApiException>(() => planApplication.ReturnPlanIfFinded(plan.Id));

            //Assert
            Assert.Equal("O Plano com o Id informado não existe.", planException.Message);
        }
    }
}
