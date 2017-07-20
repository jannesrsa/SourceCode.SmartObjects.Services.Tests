using System;

namespace SourceCode.SmartObjects.Services.Tests.Interfaces
{
    public interface IServiceManagementServer : IBaseAPI
    {
        bool DeleteServiceInstance(Guid ServiceInstanceGuid, bool IgnoreDependancy);

        string GetServiceInstanceCompact(Guid ServiceInstanceGuid);

        string GetServiceInstanceConfig(Guid ServiceTypeGuid);

        string GetServiceType(Guid ServiceTypeGuid);
    }
}