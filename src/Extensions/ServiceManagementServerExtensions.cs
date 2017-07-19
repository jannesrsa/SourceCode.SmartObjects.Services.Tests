using System;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Interface;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Extensions
{
    public static class ServiceManagementServerExtensions
    {
        public static void DeleteServiceInstance(this ServiceManagementServer server, Guid serviceInstanceGuid)
        {
            DeleteServiceInstance(new ServiceManagementServerWrapper(server), serviceInstanceGuid);
        }

        internal static void DeleteServiceInstance(this IServiceManagementServerWrapper server, Guid serviceInstanceGuid)
        {
            server.ThrowIfNull("server");

            if (!string.IsNullOrEmpty(server.GetServiceInstanceCompact(serviceInstanceGuid)))
            {
                server.DeleteServiceInstance(serviceInstanceGuid, false);
            }
        }
    }
}