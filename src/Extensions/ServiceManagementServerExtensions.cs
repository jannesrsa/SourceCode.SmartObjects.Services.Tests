using System;
using SourceCode.SmartObjects.Services.Management;

namespace SourceCode.SmartObjects.Services.Tests.Extensions
{
    public static class ServiceManagementServerExtensions
    {
        public static void DeleteServiceInstance(this ServiceManagementServer server, Guid serviceInstanceGuid)
        {
            server.ThrowIfNull("server");

            if (!string.IsNullOrEmpty(server.GetServiceInstanceCompact(serviceInstanceGuid)))
            {
                server.DeleteServiceInstance(serviceInstanceGuid, false);
            }
        }
    }
}