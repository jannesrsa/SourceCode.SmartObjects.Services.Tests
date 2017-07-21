using System;
using System.Diagnostics.CodeAnalysis;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interfaces;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class ServiceManagementServerWrapper : IBaseAPI
    {
        private readonly ServiceManagementServer _serviceManagementServer;

        public ServiceManagementServerWrapper(ServiceManagementServer serviceManagementServer)
        {
            serviceManagementServer.ThrowIfNull(nameof(serviceManagementServer));

            _serviceManagementServer = serviceManagementServer;
        }

        public ServiceManagementServerWrapper()
        {
        }

        [ExcludeFromCodeCoverage]
        public virtual BaseAPI BaseAPIServer
        {
            get
            {
                return _serviceManagementServer;
            }
        }

        [ExcludeFromCodeCoverage]
        public virtual bool DeleteServiceInstance(Guid ServiceInstanceGuid, bool IgnoreDependancy)
        {
            return _serviceManagementServer.DeleteServiceInstance(ServiceInstanceGuid, IgnoreDependancy);
        }

        [ExcludeFromCodeCoverage]
        public virtual string GetServiceInstanceCompact(Guid ServiceInstanceGuid)
        {
            return _serviceManagementServer.GetServiceInstanceCompact(ServiceInstanceGuid);
        }

        [ExcludeFromCodeCoverage]
        public virtual string GetServiceInstanceConfig(Guid ServiceTypeGuid)
        {
            return _serviceManagementServer.GetServiceInstanceConfig(ServiceTypeGuid);
        }

        [ExcludeFromCodeCoverage]
        public virtual string GetServiceType(Guid ServiceTypeGuid)
        {
            return _serviceManagementServer.GetServiceType(ServiceTypeGuid);
        }

        internal void DeleteServiceInstance(Guid serviceInstanceGuid)
        {
            if (!string.IsNullOrEmpty(this.GetServiceInstanceCompact(serviceInstanceGuid)))
            {
                this.DeleteServiceInstance(serviceInstanceGuid, false);
            }
        }
    }
}