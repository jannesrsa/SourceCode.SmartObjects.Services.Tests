using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;
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
            var mockWrapperFactory = new MockWrapperFactory();

            mockWrapperFactory.ServiceManagementServer.Setup(x => x.GetServiceInstanceCompact(serviceInstanceGuid)).Returns(Resources.ServiceInstance_URMService);
            mockWrapperFactory.ServiceManagementServer.Setup(x => x.DeleteServiceInstance(serviceInstanceGuid, It.IsAny<bool>())).Returns(true);

            // Act
            ServiceManagementServerExtensions.DeleteServiceInstance(null, serviceInstanceGuid);
        }
    }
}