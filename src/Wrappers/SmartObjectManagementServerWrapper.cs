using System;
using System.Diagnostics.CodeAnalysis;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interfaces;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class SmartObjectManagementServerWrapper : ISmartObjectManagementServer
    {
        private SmartObjectManagementServer _smartObjectManagementServer;

        internal SmartObjectManagementServerWrapper(SmartObjectManagementServer smartObjectManagementServer)
        {
            smartObjectManagementServer.ThrowIfNull(nameof(smartObjectManagementServer));

            _smartObjectManagementServer = smartObjectManagementServer;
        }

        public BaseAPI BaseAPIServer
        {
            get
            {
                return _smartObjectManagementServer;
            }
        }

        public void DeleteSmartObject(string systemName, bool ignoreDependancyException)
        {
            _smartObjectManagementServer.DeleteSmartObject(systemName, ignoreDependancyException);
        }

        public string GetServiceInstance(Guid serviceInstanceGuid, ServiceExplorerLevel serviceLevel)
        {
            return _smartObjectManagementServer.GetServiceInstance(serviceInstanceGuid, serviceLevel);
        }

        public string GetServiceInstanceServiceObject(Guid serviceInstanceGuid, string serviceObjectName)
        {
            return _smartObjectManagementServer.GetServiceInstanceServiceObject(serviceInstanceGuid, serviceObjectName);
        }

        public string GetSmartObjectDefinition(string systemName)
        {
            return _smartObjectManagementServer.GetSmartObjectDefinition(systemName);
        }

        public SmartObjectExplorer GetSmartObjects(SearchProperty searchProperty, SearchOperator searchOperator, string value)
        {
            return _smartObjectManagementServer.GetSmartObjects(searchProperty, searchOperator, value);
        }

        public SmartObjectExplorer GetSmartObjects(string systemName)
        {
            return _smartObjectManagementServer.GetSmartObjects(systemName);
        }

        public SmartObjectExplorer GetSmartObjects(Guid[] guids)
        {
            return _smartObjectManagementServer.GetSmartObjects(guids);
        }

        public void PublishSmartObjects(string smartObjectDefinitionsPublish)
        {
            _smartObjectManagementServer.PublishSmartObjects(smartObjectDefinitionsPublish);
        }
    }
}