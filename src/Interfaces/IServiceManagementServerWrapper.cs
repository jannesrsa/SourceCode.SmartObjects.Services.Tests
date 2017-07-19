using System;

namespace SourceCode.SmartObjects.Services.Tests.Interface
{
    public interface IServiceManagementServerWrapper
    {
        bool DeleteServiceInstance(Guid ServiceInstanceGuid, bool IgnoreDependancy);

        string GetServiceInstanceCompact(Guid ServiceInstanceGuid);
    }
}