using BusinessLayer.Services;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using DataLayer.Repositories.Abstractions;
using Moq;
using System;
using UnitTests.Base;
using Xunit;

namespace UnitTests.Tests.Zones
{
    public class ZoneValidationRequestServiceTest : BaseUnitTest
    {
        [Theory]
        [InlineData("target-zone-id")]
        [InlineData("1638498-65432-546546-54654")]
        public void Should_Throw_Exception_On_Invalid_Zone_Id_On_Modify(String targetZoneId)
        {
            IZoneValidationRequestRepository zoneValidationRequestRepository = new Mock<IZoneValidationRequestRepository>().Object;
            Mock<IZoneService> zoneServiceMock = new Mock<IZoneService>();

            zoneServiceMock.Setup(_ => _.GetZoneById(targetZoneId, null)).Returns((Zone)null);

            ZoneValidationRequestService validationService = new ZoneValidationRequestService(zoneValidationRequestRepository, zoneServiceMock.Object);

            ZoneValidationRequest request = new ZoneValidationRequest
            {
                TargetZoneId = targetZoneId
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => validationService.CreateModifyZoneRequest(request, String.Empty));
        }

        [Theory]
        [InlineData("target-zone-id", "test", "request-person-id")]
        [InlineData("1638498-65432-546546-54654", "person-2", "person-1")]
        public void Should_Throw_Exception_On_Not_Owner_Invali_Zone_Id_On_Modify(String targetZoneId, String ownerId, String requestPersonId)
        {
            IZoneValidationRequestRepository zoneValidationRequestRepository = new Mock<IZoneValidationRequestRepository>().Object;
            Mock<IZoneService> zoneServiceMock = new Mock<IZoneService>();

            zoneServiceMock.Setup(_ => _.GetZoneById(targetZoneId, null)).Returns(new Zone { OwnerId = ownerId });

            ZoneValidationRequestService validationService = new ZoneValidationRequestService(zoneValidationRequestRepository, zoneServiceMock.Object);

            ZoneValidationRequest request = new ZoneValidationRequest
            {
                TargetZoneId = targetZoneId
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => validationService.CreateModifyZoneRequest(request, requestPersonId));
        }

        [Theory]
        [InlineData("request-id")]
        [InlineData("1638498-65432-546546-54654")]
        public void Should_Throw_Exception_On_Invalid_Zone_Id_On_Cancel(String requestId)
        {
            Mock<IZoneValidationRequestRepository> zoneValidationRequestRepositoryMock = new Mock<IZoneValidationRequestRepository>();
            Mock<IZoneService> zoneServiceMock = new Mock<IZoneService>();

            zoneValidationRequestRepositoryMock.Setup(_ => _.GetById(requestId)).Returns((ZoneValidationRequest)null);

            ZoneValidationRequestService validationService = new ZoneValidationRequestService(zoneValidationRequestRepositoryMock.Object, zoneServiceMock.Object);

            Assert.Throws<ArgumentOutOfRangeException>(() => validationService.CancelZoneRequest(requestId, String.Empty));
        }

        [Theory]
        [InlineData("request-id", "person_id")]
        [InlineData("1638498-65432-546546-54654", "person_number_1")]
        public void Should_Throw_Exception_On_Invalid_Requester_Id_On_Cancel(String requestId, String requestPersonId)
        {
            Mock<IZoneValidationRequestRepository> zoneValidationRequestRepositoryMock = new Mock<IZoneValidationRequestRepository>();
            Mock<IZoneService> zoneServiceMock = new Mock<IZoneService>();

            zoneValidationRequestRepositoryMock.Setup(_ => _.GetById(requestId)).Returns(new ZoneValidationRequest { RequesterId = String.Empty });

            ZoneValidationRequestService validationService = new ZoneValidationRequestService(zoneValidationRequestRepositoryMock.Object, zoneServiceMock.Object);

            Assert.Throws<ArgumentOutOfRangeException>(() => validationService.CancelZoneRequest(requestId, requestPersonId));
        }
    }
}
