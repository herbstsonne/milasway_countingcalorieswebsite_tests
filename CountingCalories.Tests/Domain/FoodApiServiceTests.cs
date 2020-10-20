using System.Collections.Generic;
using System.Linq;
using CountingCalories.Domain.Entities;
using CountingCalories.Domain.Repository.Contract;
using CountingCalories.Domain.Services;
using CountingCalories.Shared.ViewModels;
using Moq;
using Xunit;

namespace CountingCalories.Tests
{
    public class FoodApiServiceTests
    {
        private Mock<IAsyncRepository<FoodEntity>> _repoMoq;
        private FoodApiService _service;

        public FoodApiServiceTests()
        {
            SetupMocks();
        }

        private void SetupMocks()
        {
            _repoMoq = new Mock<IAsyncRepository<FoodEntity>>();

            _repoMoq.Setup(r => r.GetAll())
                .Returns(
                new List<FoodEntity>()
                {
                    new FoodEntity{ Id = 0, Name = "Salad", CaloriesPer100G = 20},
                    new FoodEntity{ Id = 1, Name = "Bread", CaloriesPer100G = 300}
                }
            );
            _repoMoq.Setup(r => r.GetByName(It.IsAny<string>()))
                .Returns(
                    new FoodEntity { Id = 0, Name = "Salad", CaloriesPer100G = 20 }
                );
            var repo = _repoMoq.Object;
            _service = new FoodApiService(repo);

        }

        [Fact]
        public void TestGetAllFood()
        {
            var allFood = _service.GetAllFood();
            var allFoodList = allFood.ToList();
            Assert.NotNull(allFood);
            Assert.Equal(2, allFoodList.Count());
            var firstElement = allFoodList[0];

            Assert.Equal(typeof(FoodView), firstElement.GetType());
        }

        [Fact]
        public void GetFoodByName()
        {
            var foodView = _service.GetFoodByName("Bread");

            Assert.NotNull(foodView);
            Assert.Equal(typeof(FoodView), foodView.GetType());
        }
    }
}
