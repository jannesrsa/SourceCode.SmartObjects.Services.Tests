using System;
using System.Diagnostics.CodeAnalysis;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interfaces;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class ServiceManagementServerWrapper : IServiceManagementServer
    {
        private ServiceManagementServer _serviceManagementServer;

        internal ServiceManagementServerWrapper(ServiceManagementServer serviceManagementServer)
        {
            serviceManagementServer.ThrowIfNull(nameof(serviceManagementServer));

            _serviceManagementServer = serviceManagementServer;
        }

        public BaseAPI BaseAPIServer
        {
            get
            {
                return _serviceManagementServer;
            }
        }

        public bool DeleteServiceInstance(Guid ServiceInstanceGuid, bool IgnoreDependancy)
        {
            return _serviceManagementServer.DeleteServiceInstance(ServiceInstanceGuid, IgnoreDependancy);
        }

        public string GetServiceInstanceCompact(Guid ServiceInstanceGuid)
        {
            return _serviceManagementServer.GetServiceInstanceCompact(ServiceInstanceGuid);
        }

        public string GetServiceInstanceConfig(Guid ServiceTypeGuid)
        {
            return _serviceManagementServer.GetServiceInstanceConfig(ServiceTypeGuid);
        }

        public string GetServiceType(Guid ServiceTypeGuid)
        {
            return _serviceManagementServer.GetServiceType(ServiceTypeGuid);
        }
    }
}