using System;
using System.Diagnostics.CodeAnalysis;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interface;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class ServiceManagementServerWrapper : IServiceManagementServerWrapper
    {
        private ServiceManagementServer _serviceManagementServer;

        internal ServiceManagementServerWrapper(ServiceManagementServer serviceManagementServer)
        {
            serviceManagementServer.ThrowIfNull(nameof(serviceManagementServer));

            _serviceManagementServer = serviceManagementServer;
        }

        public bool DeleteServiceInstance(Guid ServiceInstanceGuid, bool IgnoreDependancy)
        {
            return _serviceManagementServer.DeleteServiceInstance(ServiceInstanceGuid, IgnoreDependancy);
        }

        public string GetServiceInstanceCompact(Guid ServiceInstanceGuid)
        {
            return _serviceManagementServer.GetServiceInstanceCompact(ServiceInstanceGuid);
        }
    }
}