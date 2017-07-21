using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interfaces;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class SmartObjectManagementServerWrapper : IBaseAPI
    {
        private readonly SmartObjectManagementServer _smartObjectManagementServer;

        public SmartObjectManagementServerWrapper(SmartObjectManagementServer smartObjectManagementServer)
        {
            smartObjectManagementServer.ThrowIfNull(nameof(smartObjectManagementServer));

            _smartObjectManagementServer = smartObjectManagementServer;
        }

        public SmartObjectManagementServerWrapper()
        {
        }

        public BaseAPI BaseAPIServer
        {
            get
            {
                return _smartObjectManagementServer;
            }
        }

        public bool ContainsSmartObject(string systemName)
        {
            return this.GetSmartObjects(systemName).SmartObjectList.Any();
        }

        public void DeleteSmartObject(string systemName)
        {
            if (this.ContainsSmartObject(systemName))
            {
                this.DeleteSmartObject(systemName, true);
            }
        }

        public void DeleteSmartObjects(Guid serviceInstanceGuid)
        {
            foreach (SmartObjectInfo smartObject in this.GetSmartObjects(serviceInstanceGuid).SmartObjects)
            {
                this.DeleteSmartObject(smartObject.Name);
            }
        }

        [ExcludeFromCodeCoverage]
        internal virtual void DeleteSmartObject(string systemName, bool ignoreDependancyException)
        {
            _smartObjectManagementServer.DeleteSmartObject(systemName, ignoreDependancyException);
        }

        [ExcludeFromCodeCoverage]
        internal virtual string GetServiceInstance(Guid serviceInstanceGuid, ServiceExplorerLevel serviceLevel)
        {
            return _smartObjectManagementServer.GetServiceInstance(serviceInstanceGuid, serviceLevel);
        }

        [ExcludeFromCodeCoverage]
        internal virtual string GetServiceInstanceServiceObject(Guid serviceInstanceGuid, string serviceObjectName)
        {
            return _smartObjectManagementServer.GetServiceInstanceServiceObject(serviceInstanceGuid, serviceObjectName);
        }

        [ExcludeFromCodeCoverage]
        internal virtual string GetSmartObjectDefinition(string systemName)
        {
            return _smartObjectManagementServer.GetSmartObjectDefinition(systemName);
        }

        [ExcludeFromCodeCoverage]
        internal virtual SmartObjectExplorer GetSmartObjects(SearchProperty searchProperty, SearchOperator searchOperator, string value)
        {
            return _smartObjectManagementServer.GetSmartObjects(searchProperty, searchOperator, value);
        }

        [ExcludeFromCodeCoverage]
        internal virtual SmartObjectExplorer GetSmartObjects(string systemName)
        {
            return _smartObjectManagementServer.GetSmartObjects(systemName);
        }

        [ExcludeFromCodeCoverage]
        internal virtual SmartObjectExplorer GetSmartObjects(Guid[] guids)
        {
            return _smartObjectManagementServer.GetSmartObjects(guids);
        }

        [ExcludeFromCodeCoverage]
        internal virtual SmartObjectExplorer GetSmartObjects(Guid baseServiceInstanceGuid)
        {
            return _smartObjectManagementServer.GetSmartObjects(baseServiceInstanceGuid);
        }

        [ExcludeFromCodeCoverage]
        internal virtual void PublishSmartObjects(string smartObjectDefinitionsPublish)
        {
            _smartObjectManagementServer.PublishSmartObjects(smartObjectDefinitionsPublish);
        }
    }
}