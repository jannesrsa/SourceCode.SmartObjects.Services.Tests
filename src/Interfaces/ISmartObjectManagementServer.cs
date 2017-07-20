using System;
using SourceCode.SmartObjects.Management;

namespace SourceCode.SmartObjects.Services.Tests.Interfaces
{
    public interface ISmartObjectManagementServer : IBaseAPI
    {
        void DeleteSmartObject(string systemName, bool ignoreDependancyException);

        string GetServiceInstance(Guid serviceInstanceGuid, ServiceExplorerLevel serviceLevel);

        string GetServiceInstanceServiceObject(Guid serviceInstanceGuid, string serviceObjectName);

        string GetSmartObjectDefinition(string systemName);

        SmartObjectExplorer GetSmartObjects(Guid[] guids);

        SmartObjectExplorer GetSmartObjects(SearchProperty searchProperty, SearchOperator searchOperator, string value);

        SmartObjectExplorer GetSmartObjects(string systemName);

        void PublishSmartObjects(string smartObjectDefinitionsPublish);
    }
}