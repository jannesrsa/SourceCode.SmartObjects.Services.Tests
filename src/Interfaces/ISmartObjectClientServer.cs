using System.Data;
using SourceCode.SmartObjects.Client;

namespace SourceCode.SmartObjects.Services.Tests.Interfaces
{
    public interface ISmartObjectClientServer : IBaseAPI
    {
        SmartObject ExecuteBulkScalar(SmartObject smartObject, DataTable inputTable);

        SmartObjectList ExecuteList(SmartObject smartObject, ExecuteListOptions options);

        DataTable ExecuteListDataTable(SmartObject smartObject, ExecuteListOptions options);

        SmartObjectReader ExecuteListReader(SmartObject smartObject, ExecuteListReaderOptions options);

        SmartObject ExecuteScalar(SmartObject smartObject);

        DataTable ExecuteSQLQueryDataTable(string sqlString);

        SmartObject GetSmartObject(string smartObjectName);
    }
}