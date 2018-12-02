using BusinessLayer.Filters;
using BusinessLayer.Services;
using Common.Models;
using Common.Models.Additional;
using DataLayer.Repositories.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UnitTests.Base;
using Xunit;

namespace UnitTests.Tests.Drones
{
    public class DroneServiceTest : BaseUnitTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Not_Pass_Code_Validation_For_Empty_Code(String code)
        {
            IDroneRepository droneRepository = new Mock<IDroneRepository>().Object;
            DroneService droneService = new DroneService(droneRepository);

            Boolean isValid = droneService.IsCodeValid(code);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData(null, null, 7)]
        [InlineData("", 0, 3)]
        [InlineData("", 1, 1)]
        [InlineData(null, 2, 1)]
        [InlineData("", 3, 2)]
        [InlineData("a870-4148-a2a6", 0, 1)]
        [InlineData("a870-4148-a2a6", 1, 0)]
        [InlineData("429e3909-63da-4af6-9559-903bbdc8dfc2", null, 1)]
        public void Should_Return_Correct_Filtered_Drones_By_Filter(String droneCode, Int32? droneType, Int32 count)
        {
            Mock<IDroneRepository> droneRepository = new Mock<IDroneRepository>();

            droneRepository.Setup(_ => _.GetAll(It.IsAny<Expression<Func<Drone, Boolean>>>(),
                It.IsAny<Func<IQueryable<Drone>, IQueryable<Drone>>>())).Returns(GetTestDrones());

            DroneService droneService = new DroneService(droneRepository.Object);

            DroneListFilter filter = new DroneListFilter
            {
                DroneCode = droneCode,
                DroneType = droneType
            };


            ICollection<Drone> filteredDrones = droneService.GetDronesByPersonId(String.Empty, filter);

            Assert.Equal(count, filteredDrones.Count);

        }

        private List<Drone> GetTestDrones()
        {
            return new List<Drone>
            {
                new Drone
                {
                    Code = "429e3909-63da-4af6-9559-903bbdc8dfc2",
                    Type = DroneType.Delivery
                },
                new Drone
                {
                    Code = "1ebc6b10-d9a8-4272-b3f4-06ba746337d9",
                    Type = DroneType.Military
                },
                new Drone
                {
                    Code = "c02a5c2e-a870-4148-a2a6-2c0f1d766a72",
                    Type = DroneType.Individual
                },
                new Drone
                {
                    Code = "d3ce83b9-ff4c-443d-bc2f-d33a6be8be86",
                    Type = DroneType.Police
                },
                new Drone
                {
                    Code = "73aa4826-ad20-4b21-9851-6145b906d4dd",
                    Type = DroneType.Individual
                },
                new Drone
                {
                    Code = "e234d611-e2c7-4f0f-89ae-889b10c65ff7",
                    Type = DroneType.Individual
                },
                new Drone
                {
                    Code = "f286df7d-5507-42ef-972b-91e8cabd6151",
                    Type = DroneType.Delivery
                }
            };
        }
    }
}
