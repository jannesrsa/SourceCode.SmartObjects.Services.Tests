using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interfaces;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenDeleteServiceInstanceCalledOnServiceManagementServerExtensions
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithServerNull()
        {
            //Arrange
            ServiceManagementServer server = null;
            Guid serviceInstanceGuid = Guid.NewGuid();

            // Act
            ServiceManagementServerExtensions.DeleteServiceInstance(server, serviceInstanceGuid);
        }

        [TestMethod()]
        public void WithServiceInstanceExist()
        {
            //Arrange
            Guid serviceInstanceGuid = Guid.NewGuid();
            var mockServiceManagementServer = new Mock<IServiceManagementServer>();

            mockServiceManagementServer.Setup(x => x.GetServiceInstanceCompact(serviceInstanceGuid)).Returns(Resources.ServiceInstance_URMService);
            mockServiceManagementServer.Setup(x => x.DeleteServiceInstance(serviceInstanceGuid, It.IsAny<bool>())).Returns(true);

            // Act
            ServiceManagementServerExtensions.DeleteServiceInstance(mockServiceManagementServer.Object, serviceInstanceGuid);
        }
    }
}