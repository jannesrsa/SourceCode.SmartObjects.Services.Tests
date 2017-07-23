using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;

namespace SourceCode.SmartObjects.Services.Tests.Managers.Tests
{
    [TestClass()]
    public class SmartObjectsManagerTests
    {
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void Delete_WithDefaultValues()
        {
            // Arrange
            var serviceInstanceSettings = new Mock<ServiceInstanceSettings>();
            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService");

            var smartObjectsManager = new Mock<SmartObjectsManager>(serviceInstanceSettings.Object);

            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            _mockWrapperFactory.SmartObjectManagementServer
               .Setup(i => i.GetSmartObjects(
                   It.IsAny<Guid>()))
               .Returns(mockSmartObjectExplorer);

            // Action
            smartObjectsManager.Object.Delete();
        }

        [TestMethod()]
        public void GetServiceInstanceSettings()
        {
            // Arrange
            var serviceInstanceSettings = new Mock<ServiceInstanceSettings>();
            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService");

            // Action
            var smartObjectsManager = new Mock<SmartObjectsManager>(serviceInstanceSettings.Object);

            // Assert
            Assert.AreEqual(serviceInstanceSettings.Object, smartObjectsManager.Object.ServiceInstanceSettings);
        }

        [TestMethod()]
        public void Register_WithDefaultValues()
        {
            // Arrange
            var serviceInstanceSettings = new Mock<ServiceInstanceSettings>();
            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService");

            var smartObjectsManager = new Mock<SmartObjectsManager>(serviceInstanceSettings.Object);

            // Action
            smartObjectsManager.Object.Register();
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }
    }
}