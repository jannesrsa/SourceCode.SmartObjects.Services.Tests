using System;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Extensions
{
    public static class ServiceManagementServerExtensions
    {
        public static void DeleteServiceInstance(this ServiceManagementServer server, Guid serviceInstanceGuid)
        {
            WrapperFactory.Instance.GetServiceManagementServerWrapper(server).DeleteServiceInstance(serviceInstanceGuid);
        }
    }
}