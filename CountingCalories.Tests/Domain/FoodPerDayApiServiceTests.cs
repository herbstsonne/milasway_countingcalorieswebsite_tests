using System.Collections.Generic;
using CountingCalories.Domain.Entities;
using CountingCalories.Domain.Repository.Contract;
using CountingCalories.Domain.Services;
using CountingCalories.Shared.ViewModels;
using Moq;
using Xunit;

namespace CountingCalories.Tests.Domain
{
    public class FoodPerDayApiServiceTests
    {
        private FoodPerDayApiService _service;

        public FoodPerDayApiServiceTests()
        {
            SetupMocks();
        }

        private void SetupMocks()
        {
            var repoMoq = new Mock<IAsyncRepository<FoodPerDayEntity>>();

            repoMoq.Setup(r => r.GetByDate(It.IsAny<string>()))
                .Returns(

                    new FoodPerDayEntity
                    {
                        Id = 0,
                        Day = "01.10.2020",
                        AllEntries = new List<FoodEntryEntity>()
                        {
                            new FoodEntryEntity
                            {
                                Id = 0,
                                FoodId = 2,
                                FoodName = "Bread",
                                Amount = 20,
                                Calories = 80
                            }
                        }
                    }

                );
            _service = new FoodPerDayApiService(repoMoq.Object);
        }

        [Fact]
        public void GetFoodPerDayByDateTest()
        {
            var date = "01.10.2020";
            var res = _service.GetFoodPerDayByDate(date);
            Assert.NotNull(res);

            Assert.Equal(typeof(FoodPerDayView), res.GetType());

            Assert.Equal(0, res.Id);
            Assert.Equal(date, res.Day);
            Assert.NotNull(res.AllFoodEntries);

            var entries = res.AllFoodEntries;
            Assert.Single(entries);

            var firstEntry = entries[0];

            Assert.Equal(2, firstEntry.FoodId);
            Assert.Equal("Bread", firstEntry.FoodName);
            Assert.Equal(20, firstEntry.Amount);
            Assert.Equal(80, firstEntry.Calories);
        }

    }
}
