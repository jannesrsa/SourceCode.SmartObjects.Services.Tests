using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;

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

            MockWrapperFactory.Instance.ServiceManagementServer.Setup(x => x.GetServiceInstanceCompact(serviceInstanceGuid)).Returns(Resources.ServiceInstance_URMService);
            MockWrapperFactory.Instance.ServiceManagementServer.Setup(x => x.DeleteServiceInstance(serviceInstanceGuid, It.IsAny<bool>())).Returns(true);

            // Action
            ServiceManagementServerExtensions.DeleteServiceInstance(null, serviceInstanceGuid);
        }

        [TestInitialize()]
        public void TestInit()
        {
            MockWrapperFactory.MockInstance();
        }
    }
}