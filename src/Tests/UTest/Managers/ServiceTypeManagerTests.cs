using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;

namespace SourceCode.SmartObjects.Services.Tests.Managers.Tests
{
    [TestClass()]
    public class ServiceTypeManagerTests
    {
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            var serviceTypeSettings = Mock.Of<ServiceTypeSettings>();
            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings);

            // Action
            serviceTypeCreator.Object.Delete();
        }

        [TestMethod()]
        public void Register_NewServiceType()
        {
            // Arrange
            var serviceTypeSettings = Mock.Of<ServiceTypeSettings>();
            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings);

            var registerableServices = new Dictionary<string, string>();
            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetRegisterableServices())
                .Returns(registerableServices);

            // Action
            serviceTypeCreator.Object.Register();
        }

        [TestMethod()]
        public void Register_WithExisingServiceType()
        {
            // Arrange
            var urmServiceType = ServiceTypeInfo.Create(Resources.ServiceType_URMService);

            var serviceTypeSettings = new Mock<ServiceTypeSettings>();
            serviceTypeSettings
                .SetupGet(i => i.ClassName)
                .Returns(urmServiceType.Class);

            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings.Object);

            var registerableServices = new Dictionary<string, string>();
            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetRegisterableServices())
                .Returns(registerableServices);

            var serviceTypeInfoCollection = new List<ServiceTypeInfo>();
            serviceTypeInfoCollection.Add(urmServiceType);

            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceTypes())
                .Returns(serviceTypeInfoCollection);

            // Action
            serviceTypeCreator.Object.Register();
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }
    }
}