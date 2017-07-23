using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;

namespace SourceCode.SmartObjects.Services.Tests.Managers.Tests
{
    [TestClass()]
    public class ServiceInstanceManagerTests
    {
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void Delete_DefaultValues()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<Guid>()))
                .Returns(mockSmartObjectExplorer);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            var serviceTypeSettings = Mock.Of<ServiceTypeSettings>();
            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings);
            var serviceInstanceSettings = Mock.Of<ServiceInstanceSettings>();

            var serviceInstanceManager = new ServiceInstanceManager(serviceTypeCreator.Object, serviceInstanceSettings);

            // Act
            serviceInstanceManager.Delete();
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }
    }
}