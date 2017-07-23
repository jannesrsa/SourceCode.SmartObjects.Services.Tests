using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
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

        [TestMethod()]
        public void RegisterTest_WithDeleteExistingServiceInstance()
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

            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstanceConfig(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstanceConfig);

            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstancesCompact(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstancesCompact_URMService);

            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstance(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstance_URMService_Full);

            var serviceTypeSettings = Mock.Of<ServiceTypeSettings>();
            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings);

            var serviceInstanceSettings = new Mock<ServiceInstanceSettings>();
            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService");

            serviceInstanceSettings
                .SetupGet(i => i.ServiceAuthentication)
                .Returns(new ServiceAuthenticationInfo());

            var configurationSettings = new Dictionary<string, string>();
            configurationSettings["HostServerConnectionString"] = Guid.NewGuid().ToString();

            serviceInstanceSettings
                .SetupGet(i => i.ConfigurationSettings)
                .Returns(configurationSettings);

            var serviceInstanceManager = new ServiceInstanceManager(serviceTypeCreator.Object, serviceInstanceSettings.Object);

            // Action 1
            serviceInstanceManager.Register();

            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService2");

            serviceInstanceSettings
              .SetupGet(i => i.Guid)
              .Returns(new Guid("5D273AD6-E27A-46F8-BE67-198B36085F99"));

            // Action 2
            serviceInstanceManager.Register();
        }

        [TestMethod()]
        public void RegisterTest_WithRefreshExistingServiceInstance()
        {
            // Arrange
            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstanceConfig(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstanceConfig);

            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstancesCompact(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstancesCompact_URMService);

            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstance(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstance_URMService_Full);

            var serviceTypeSettings = Mock.Of<ServiceTypeSettings>();
            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings);

            var serviceInstanceSettings = new Mock<ServiceInstanceSettings>();
            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService");

            serviceInstanceSettings
                .SetupGet(i => i.Guid)
                .Returns(new Guid("4C2F62EA-BE8D-4600-A2B5-185902BDD20A"));

            serviceInstanceSettings
                .SetupGet(i => i.ServiceAuthentication)
                .Returns(new ServiceAuthenticationInfo());

            var configurationSettings = new Dictionary<string, string>();
            configurationSettings["HostServerConnectionString"] = Guid.NewGuid().ToString();

            serviceInstanceSettings
                .SetupGet(i => i.ConfigurationSettings)
                .Returns(configurationSettings);

            var serviceInstanceManager = new ServiceInstanceManager(serviceTypeCreator.Object, serviceInstanceSettings.Object);

            // Action 1
            serviceInstanceManager.Register();

            // Action 2
            serviceInstanceManager.Register(true);
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }

        [TestMethod()]
        public void UpdateTest_WithRefreshExistingServiceInstance()
        {
            // Arrange
            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstanceConfig(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstanceConfig);

            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstancesCompact(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstancesCompact_URMService);

            var serviceTypeSettings = Mock.Of<ServiceTypeSettings>();
            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings);

            var serviceInstanceSettings = new Mock<ServiceInstanceSettings>();
            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService");

            serviceInstanceSettings
                .SetupGet(i => i.Guid)
                .Returns(new Guid("4C2F62EA-BE8D-4600-A2B5-185902BDD20A"));

            serviceInstanceSettings
                .SetupGet(i => i.ServiceAuthentication)
                .Returns(new ServiceAuthenticationInfo());

            var configurationSettings = new Dictionary<string, string>();
            configurationSettings["HostServerConnectionString"] = Guid.NewGuid().ToString();

            serviceInstanceSettings
                .SetupGet(i => i.ConfigurationSettings)
                .Returns(configurationSettings);

            var serviceInstanceManager = new ServiceInstanceManager(serviceTypeCreator.Object, serviceInstanceSettings.Object);

            // Action
            serviceInstanceManager.Update(configurationSettings);
        }
    }
}