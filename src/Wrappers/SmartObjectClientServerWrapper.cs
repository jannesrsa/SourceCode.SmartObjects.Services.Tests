using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Helpers;
using SourceCode.SmartObjects.Services.Tests.Interfaces;
using SourceCode.SmartObjects.Services.Tests.Managers;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class SmartObjectClientServerWrapper : IBaseAPI
    {
        private readonly SmartObjectClientServer _serviceClientServer;

        public SmartObjectClientServerWrapper(SmartObjectClientServer serviceClientServer)
        {
            serviceClientServer.ThrowIfNull(nameof(serviceClientServer));

            _serviceClientServer = serviceClientServer;
        }

        public SmartObjectClientServerWrapper()
        {
        }

        public BaseAPI BaseAPIServer
        {
            get
            {
                return _serviceClientServer;
            }
        }

        [ExcludeFromCodeCoverage]
        public virtual SmartObject ExecuteBulkScalar(SmartObject smartObject, DataTable inputTable)
        {
            return _serviceClientServer.ExecuteBulkScalar(smartObject, inputTable);
        }

        [ExcludeFromCodeCoverage]
        public virtual SmartObjectList ExecuteList(SmartObject smartObject, ExecuteListOptions options)
        {
            return _serviceClientServer.ExecuteList(smartObject, options);
        }

        [ExcludeFromCodeCoverage]
        public virtual DataTable ExecuteListDataTable(SmartObject smartObject, ExecuteListOptions options)
        {
            return _serviceClientServer.ExecuteListDataTable(smartObject, options);
        }

        [ExcludeFromCodeCoverage]
        public virtual SmartObjectReader ExecuteListReader(SmartObject smartObject, ExecuteListReaderOptions options)
        {
            return _serviceClientServer.ExecuteListReader(smartObject, options);
        }

        [ExcludeFromCodeCoverage]
        public virtual SmartObject ExecuteScalar(SmartObject smartObject)
        {
            return _serviceClientServer.ExecuteScalar(smartObject);
        }

        [ExcludeFromCodeCoverage]
        public virtual DataTable ExecuteSQLQueryDataTable(string sqlString)
        {
            return _serviceClientServer.ExecuteSQLQueryDataTable(sqlString);
        }

        [ExcludeFromCodeCoverage]
        public virtual SmartObject GetSmartObject(string smartObjectName)
        {
            return _serviceClientServer.GetSmartObject(smartObjectName);
        }

        internal SmartObject Deserialize(string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings, string value)
        {
            var smartObject = SmartObjectHelper.GetSmartObject(this, serviceObjectName, serviceInstanceSettings);

            smartObject.MethodToExecute = "Deserialize";
            smartObject.SetInputPropertyValue("Serialized_Item__String_", value);

            SmartObjectHelper.ExecuteScalar(this, smartObject);

            return smartObject;
        }

        internal DataTable DeserializeTypedArray(string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings, string value)
        {
            var smartObject = SmartObjectHelper.GetSmartObject(this, serviceObjectName, serviceInstanceSettings);

            smartObject.MethodToExecute = "DeserializeTypedArray";
            smartObject.SetInputPropertyValue("Serialized_Array", value);

            var dataTable = SmartObjectHelper.ExecuteListDataTable(this, smartObject);

            return dataTable;
        }

        internal string Serialize(string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            actions.ThrowIfNull("actions");

            var smartObject = SmartObjectHelper.GetSmartObject(this, serviceObjectName, serviceInstanceSettings);
            smartObject.MethodToExecute = "Serialize";

            foreach (var action in actions)
            {
                action(smartObject);
            }

            var serialized = SmartObjectHelper.ExecuteScalar(this, smartObject);
            return serialized.GetReturnPropertyValue("Serialized_Item__String_");
        }

        internal string SerializeAddItemToArray(string serviceObjectName, string existingSerializedArray,
            ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            actions.ThrowIfNull("actions");

            var smartObject = SmartObjectHelper.GetSmartObject(this, serviceObjectName, serviceInstanceSettings);
            smartObject.MethodToExecute = "SerializeAddItemToArray";
            smartObject.SetInputPropertyValue("Serialized_Array", existingSerializedArray);

            foreach (var action in actions)
            {
                action(smartObject);
            }

            SmartObjectHelper.ExecuteScalar(this, smartObject);

            return smartObject.Properties["Serialized_Array"].Value;
        }

        internal string SerializeItemToArray(string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings, params Action<SmartObject>[] actions)
        {
            actions.ThrowIfNull("actions");

            var smartObject = SmartObjectHelper.GetSmartObject(this, serviceObjectName, serviceInstanceSettings);
            smartObject.MethodToExecute = "SerializeItemToArray";

            foreach (var action in actions)
            {
                action(smartObject);
            }

            SmartObjectHelper.ExecuteScalar(this, smartObject);

            return smartObject.Properties["Serialized_Array"].Value;
        }
    }
}