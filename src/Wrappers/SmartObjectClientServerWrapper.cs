using System.Data;
using System.Diagnostics.CodeAnalysis;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interfaces;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class SmartObjectClientServerWrapper : ISmartObjectClientServer
    {
        private SmartObjectClientServer _serviceClientServer;

        internal SmartObjectClientServerWrapper(SmartObjectClientServer serviceClientServer)
        {
            serviceClientServer.ThrowIfNull(nameof(serviceClientServer));

            _serviceClientServer = serviceClientServer;
        }

        public BaseAPI BaseAPIServer
        {
            get
            {
                return _serviceClientServer;
            }
        }

        public SmartObject ExecuteBulkScalar(SmartObject smartObject, DataTable inputTable)
        {
            return _serviceClientServer.ExecuteBulkScalar(smartObject, inputTable);
        }

        public SmartObjectList ExecuteList(SmartObject smartObject, ExecuteListOptions options)
        {
            return _serviceClientServer.ExecuteList(smartObject, options);
        }

        public DataTable ExecuteListDataTable(SmartObject smartObject, ExecuteListOptions options)
        {
            return _serviceClientServer.ExecuteListDataTable(smartObject, options);
        }

        public SmartObjectReader ExecuteListReader(SmartObject smartObject, ExecuteListReaderOptions options)
        {
            return _serviceClientServer.ExecuteListReader(smartObject, options);
        }

        public SmartObject ExecuteScalar(SmartObject smartObject)
        {
            return _serviceClientServer.ExecuteScalar(smartObject);
        }

        public DataTable ExecuteSQLQueryDataTable(string sqlString)
        {
            return _serviceClientServer.ExecuteSQLQueryDataTable(sqlString);
        }

        public SmartObject GetSmartObject(string smartObjectName)
        {
            return _serviceClientServer.GetSmartObject(smartObjectName);
        }
    }
}