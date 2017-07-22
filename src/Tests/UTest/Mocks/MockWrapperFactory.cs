using Moq;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Helpers;
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

            Factory.Setup(x => x.GetSmartObjectClientServerWrapper(null)).Returns(SmartObjectClientServer.Object);
            Factory.Setup(x => x.GetSmartObjectManagementServerWrapper(null)).Returns(SmartObjectManagementServer.Object);
            Factory.Setup(x => x.GetServiceManagementServerWrapper(null)).Returns(ServiceManagementServer.Object);
            Factory.Setup(x => x.GetEnvironmentSettingsManagerWrapper(null)).Returns(EnvironmentSettingsManager.Object);
            Factory.Setup(x => x.GetPackageDeploymentManagerWrapper(null)).Returns(PackageDeploymentManager.Object);

            Factory.Setup(x => x.GetServer<SmartObjectClientServer>()).Returns(new SmartObjectClientServer());
            Factory.Setup(x => x.GetServer<SmartObjectManagementServer>()).Returns(new SmartObjectManagementServer());
            Factory.Setup(x => x.GetServer<ServiceManagementServer>()).Returns(new ServiceManagementServer());

            ConnectionHelper.UpdateWrapperFactory(Factory.Object);
        }

        public Mock<EnvironmentSettingsManagerWrapper> EnvironmentSettingsManager { get; }

        public Mock<WrapperFactory> Factory { get; }

        public Mock<PackageDeploymentManagerWrapper> PackageDeploymentManager { get; private set; }

        public Mock<ServiceManagementServerWrapper> ServiceManagementServer { get; }

        public Mock<SmartObjectClientServerWrapper> SmartObjectClientServer { get; }

        public Mock<SmartObjectManagementServerWrapper> SmartObjectManagementServer { get; }
    }
}