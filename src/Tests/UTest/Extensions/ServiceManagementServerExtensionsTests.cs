using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class ServiceManagementServerExtensionsTests
    {
        [TestMethod()]
        public void DeleteServiceInstance_WithServiceInstanceExist()
        {
            //Arrange
            Guid serviceInstanceGuid = Guid.NewGuid();
            var mockServiceManagementServer = new Mock<ServiceManagementServerWrapper>();

            mockServiceManagementServer.Setup(x => x.GetServiceInstanceCompact(serviceInstanceGuid)).Returns(Resources.ServiceInstance_URMService);
            mockServiceManagementServer.Setup(x => x.DeleteServiceInstance(serviceInstanceGuid, It.IsAny<bool>())).Returns(true);

            // Act
            mockServiceManagementServer.Object.DeleteServiceInstance(serviceInstanceGuid);
        }
    }
}