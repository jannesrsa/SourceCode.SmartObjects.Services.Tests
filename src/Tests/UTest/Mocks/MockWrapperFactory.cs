using System;
using System.Collections.Generic;
using Moq;
using SourceCode.Deployment.Management;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Managers;
using SourceCode.SmartObjects.Services.Tests.UTest.Factories;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.UTest.Mocks
{
    internal class MockWrapperFactory
    {
        public MockWrapperFactory()
        {
            Factory = new Mock<WrapperFactory>();

            SmartObjectClientServer = new Mock<SmartObjectClientServerWrapper>();
            SmartObjectManagementServer = new Mock<SmartObjectManagementServerWrapper>();
            ServiceManagementServer = new Mock<ServiceManagementServerWrapper>();
            EnvironmentSettingsManager = new Mock<EnvironmentSettingsManagerWrapper>();
            PackageDeploymentManager = new Mock<PackageDeploymentManagerWrapper>();
            WebRequestManager = new Mock<WebRequestWrapper>();
            SecurityManager = new Mock<SecurityWrapper>();

            Factory.Setup(x => x.GetSmartObjectClientServerWrapper(It.IsAny<SmartObjectClientServer>())).Returns(SmartObjectClientServer.Object);
            Factory.Setup(x => x.GetSmartObjectManagementServerWrapper(It.IsAny<SmartObjectManagementServer>())).Returns(SmartObjectManagementServer.Object);
            Factory.Setup(x => x.GetServiceManagementServerWrapper(It.IsAny<ServiceManagementServer>())).Returns(ServiceManagementServer.Object);
            Factory.Setup(x => x.GetEnvironmentSettingsManagerWrapper(It.IsAny<EnvironmentSettingsManager>())).Returns(EnvironmentSettingsManager.Object);
            Factory.Setup(x => x.GetPackageDeploymentManagerWrapper(It.IsAny<PackageDeploymentManager>())).Returns(PackageDeploymentManager.Object);
            Factory.Setup(x => x.GetWebRequestWrapper()).Returns(WebRequestManager.Object);
            Factory.Setup(x => x.GetSecurityWrapper()).Returns(SecurityManager.Object);

            Factory.Setup(x => x.GetServer<SmartObjectClientServer>()).Returns(new SmartObjectClientServer());
            Factory.Setup(x => x.GetServer<SmartObjectManagementServer>()).Returns(new SmartObjectManagementServer());
            Factory.Setup(x => x.GetServer<ServiceManagementServer>()).Returns(new ServiceManagementServer());
            Factory.Setup(x => x.GetServer<PackageDeploymentManager>()).Returns(new PackageDeploymentManager());

            WrapperFactory.Instance = Factory.Object;
        }

        public Mock<EnvironmentSettingsManagerWrapper> EnvironmentSettingsManager { get; }

        public Mock<WrapperFactory> Factory { get; }

        public Mock<PackageDeploymentManagerWrapper> PackageDeploymentManager { get; private set; }

        public Mock<SecurityWrapper> SecurityManager { get; }

        public Mock<ServiceManagementServerWrapper> ServiceManagementServer { get; }

        public Mock<SmartObjectClientServerWrapper> SmartObjectClientServer { get; }

        public Mock<SmartObjectManagementServerWrapper> SmartObjectManagementServer { get; }

        public Mock<WebRequestWrapper> WebRequestManager { get; }

        public void WithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings serviceInstanceSettings)
        {
            smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            this.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<SearchProperty>(),
                    It.IsAny<SearchOperator>(),
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            this.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<Guid>()))
                .Returns(mockSmartObjectExplorer);

            this.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            this.SmartObjectClientServer
                .Setup(x => x.GetSmartObject(
                    It.IsAny<string>()))
                .Returns(smartObject);

            this.SmartObjectClientServer
                .Setup(x => x.ExecuteScalar(It.IsAny<SmartObject>()))
                .Returns(smartObject);

            this.ServiceManagementServer
                .Setup(i => i.GetServiceInstanceConfig(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstanceConfig);

            this.ServiceManagementServer
                .Setup(i => i.GetServiceInstancesCompact(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstancesCompact_URMService);

            this.ServiceManagementServer
                .Setup(i => i.GetServiceInstance(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstance_URMService_Full);

            serviceInstanceSettings = settings.Object;
        }

        public void WithProcessInstanceSmartObject()
        {
            WithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings serviceInstanceSettings);
        }

        public ServiceInstanceManager WithExistingServiceInstance(Mock<ServiceInstanceSettings> serviceInstanceSettings = null, Dictionary<string, string> configurationSettings = null)
        {
            var serviceTypeSettings = Mock.Of<ServiceTypeSettings>();
            var serviceTypeCreator = new Mock<ServiceTypeManager>(serviceTypeSettings);

            if (serviceInstanceSettings == null)
            {
                serviceInstanceSettings = new Mock<ServiceInstanceSettings>();
            }

            serviceInstanceSettings
                .SetupGet(i => i.Name)
                .Returns("URMService");

            serviceInstanceSettings
                .SetupGet(i => i.Description)
                .Returns("URMService Description");

            serviceInstanceSettings
                .SetupGet(i => i.Guid)
                .Returns(new Guid("4C2F62EA-BE8D-4600-A2B5-185902BDD20A"));

            serviceInstanceSettings
                .SetupGet(i => i.ServiceAuthentication)
                .Returns(new ServiceAuthenticationInfo());

            if (configurationSettings == null)
            {
                configurationSettings = new Dictionary<string, string>();
            }

            configurationSettings["HostServerConnectionString"] = Guid.NewGuid().ToString();

            serviceInstanceSettings
                .SetupGet(i => i.ConfigurationSettings)
                .Returns(configurationSettings);

            var serviceInstanceManager = new ServiceInstanceManager(serviceTypeCreator.Object, serviceInstanceSettings.Object);
            return serviceInstanceManager;
        }
    }
}