using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;

namespace SourceCode.SmartObjects.Services.Tests.Managers.Tests
{
    [TestClass()]
    public class ServiceInstanceManagerTests
    {
        [TestMethod()]
        public void Delete_DefaultValues()
        {
            // Arrange
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject();

            var serviceTypeSettings = Mock.Of<ServiceTypeSettings>();
            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings);
            var serviceInstanceSettings = Mock.Of<ServiceInstanceSettings>();

            var serviceInstanceManager = new ServiceInstanceManager(serviceTypeCreator.Object, serviceInstanceSettings);

            // Action
            serviceInstanceManager.Delete();
        }

        [TestMethod()]
        public void RegisterTest_WithDeleteExistingServiceInstance()
        {
            // Arrange
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject();

            var serviceInstanceSettings = new Mock<ServiceInstanceSettings>();
            var serviceInstanceManager = MockWrapperFactory.WithExistingServiceInstance(serviceInstanceSettings);

            serviceInstanceSettings
              .SetupGet(i => i.Guid)
              .Returns(new Guid("5D273AD6-E27A-46F8-BE67-198B36085F99"));

            // Action 1
            serviceInstanceManager.Register();

            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService2");

            // Action 2
            serviceInstanceManager.Register();
        }

        [TestMethod()]
        public void RegisterTest_WithRefreshExistingServiceInstance()
        {
            // Arrange
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject();
            var serviceInstanceManager = MockWrapperFactory.WithExistingServiceInstance();

            // Action 1
            serviceInstanceManager.Register();

            // Action 2
            serviceInstanceManager.Register(true);
        }

        [TestInitialize()]
        public void TestInit()
        {
            MockWrapperFactory.MockInstance();
        }

        [TestMethod()]
        public void UpdateTest_WithRefreshExistingServiceInstance()
        {
            // Arrange
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject();

            var configurationSettings = new Dictionary<string, string>();
            var serviceInstanceManager = MockWrapperFactory.WithExistingServiceInstance(configurationSettings: configurationSettings);

            // Action
            serviceInstanceManager.Update(configurationSettings);
        }
    }
}