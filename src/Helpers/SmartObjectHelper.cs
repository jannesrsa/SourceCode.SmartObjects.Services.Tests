using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SourceCode.SmartObjects.Authoring;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Managers;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    /// <summary>
    /// SmartObjectHelper
    /// </summary>
    public static class SmartObjectHelper
    {
        public static readonly Guid SmartBoxServiceInstanceGuid = new Guid("e5609413-d844-4325-98c3-db3cacbd406d");

        public static void CompareDataTables(DataTable dataTable1, DataTable dataTable2)
        {
            dataTable1.ThrowIfNull("dataTable1");
            dataTable2.ThrowIfNull("dataTable2");

            AssertHelper.AreEqual(dataTable1.Rows.Count, dataTable2.Rows.Count);
            AssertHelper.AreEqual(dataTable1.Columns.Count, dataTable2.Columns.Count);

            for (int i = 0; i < dataTable1.Rows.Count; i++)
            {
                DataRow dataRow1 = dataTable1.Rows[i];
                DataRow dataRow2 = dataTable2.Rows[i];

                foreach (DataColumn dataColumn1 in dataTable1.Columns)
                {
                    var dataColumn2 = dataTable2.Columns[dataColumn1.ColumnName];
                    AssertHelper.AreEqual(dataRow1[dataColumn1], dataRow2[dataColumn2]);
                }
            }
        }

        public static bool ContainsSmartObject(SmartObjectManagementServer server, string systemName)
        {
            return ContainsSmartObject(new SmartObjectManagementServerWrapper(server), systemName);
        }

        public static void DeleteSmartObject(SmartObjectManagementServer server, string systemName)
        {
            DeleteSmartObject(new SmartObjectManagementServerWrapper(server), systemName);
        }

        public static SmartObject ExecuteBulkScalar(SmartObjectClientServer clientServer, SmartObject smartObject, DataTable inputTable)
        {
            return ExecuteBulkScalar(new SmartObjectClientServerWrapper(clientServer), smartObject, inputTable);
        }

        public static SmartObjectList ExecuteList(SmartObjectClientServer clientServer, SmartObject smartObject, ExecuteListOptions options = null)
        {
            return ExecuteList(new SmartObjectClientServerWrapper(clientServer), smartObject, options);
        }

        public static DataTable ExecuteListDataTable(SmartObjectClientServer clientServer, SmartObject smartObject, ExecuteListOptions options = null)
        {
            return ExecuteListDataTable(new SmartObjectClientServerWrapper(clientServer), smartObject, options);
        }

        public static SmartObjectReader ExecuteListReader(SmartObjectClientServer clientServer, SmartObject smartObject, ExecuteListReaderOptions options = null)
        {
            return ExecuteListReader(new SmartObjectClientServerWrapper(clientServer), smartObject, options);
        }

        public static SmartObject ExecuteScalar(SmartObjectClientServer clientServer, SmartObject smartObject)
        {
            return ExecuteScalar(new SmartObjectClientServerWrapper(clientServer), smartObject);
        }

        public static DataTable ExecuteSQLQueryDataTable(SmartObjectClientServer clientServer, string query)
        {
            return ExecuteSQLQueryDataTable(new SmartObjectClientServerWrapper(clientServer), query);
        }

        public static ServiceConfigInfo GetServiceConfigInfo(Guid serviceTypeGuid)
        {
            var connection = ConnectionHelper.GetServerWrapper<ServiceManagementServerWrapper>();
            using (connection.BaseAPIServer?.Connection)
            {
                string serviceInstanceConfigXml = connection.GetServiceInstanceConfig(serviceTypeGuid);
                return ServiceConfigInfo.Create(serviceInstanceConfigXml);
            }
        }

        public static ServiceInstance GetServiceInstance(ServiceInstanceSettings serviceInstanceSettings)
        {
            serviceInstanceSettings.ThrowIfNull("serviceInstanceSettings");

            var connection = ConnectionHelper.GetServerWrapper<SmartObjectManagementServerWrapper>();
            using (connection.BaseAPIServer?.Connection)
            {
                var serviceInstance = ServiceInstance.Create(connection.GetServiceInstance(serviceInstanceSettings.Guid, ServiceExplorerLevel.ServiceObject));
                return serviceInstance;
            }
        }

        public static ServiceInstanceInfo GetServiceInstance(Guid serviceInstanceGuid)
        {
            var serviceManagementServer = ConnectionHelper.GetServerWrapper<ServiceManagementServerWrapper>();
            using (serviceManagementServer.BaseAPIServer?.Connection)
            {
                var serviceInstanceXml = serviceManagementServer.GetServiceInstanceCompact(serviceInstanceGuid);
                if (string.IsNullOrEmpty(serviceInstanceXml))
                {
                    return null;
                }
                else
                {
                    return ServiceInstanceInfo.Create(serviceInstanceXml);
                }
            }
        }

        public static ServiceObject GetServiceObject(ServiceInstanceSettings serviceInstanceSettings, string serviceObjectName)
        {
            serviceInstanceSettings.ThrowIfNull("serviceInstanceSettings");

            var connection = ConnectionHelper.GetServerWrapper<SmartObjectManagementServerWrapper>();
            using (connection.BaseAPIServer?.Connection)
            {
                var serviceObject = ServiceObject.Create(connection.GetServiceInstanceServiceObject(serviceInstanceSettings.Guid, serviceObjectName));
                return serviceObject;
            }
        }

        public static ServiceTypeInfo GetServiceType(Guid guid)
        {
            var serviceManagementServer = ConnectionHelper.GetServerWrapper<ServiceManagementServerWrapper>();
            using (serviceManagementServer.BaseAPIServer?.Connection)
            {
                var serviceTypeXml = serviceManagementServer.GetServiceType(guid);
                if (string.IsNullOrEmpty(serviceTypeXml))
                {
                    return null;
                }
                else
                {
                    return ServiceTypeInfo.Create(serviceTypeXml);
                }
            }
        }

        public static ServiceTypeInfo GetServiceTypeInfo(Guid serviceTypeGuid)
        {
            var connection = ConnectionHelper.GetServerWrapper<ServiceManagementServerWrapper>();
            using (connection.BaseAPIServer?.Connection)
            {
                string serviceTypeInfoXml = connection.GetServiceType(serviceTypeGuid);
                return ServiceTypeInfo.Create(serviceTypeInfoXml);
            }
        }

        public static SmartObject GetSmartObject(SmartObjectClientServer clientServer, string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings)
        {
            return GetSmartObject(new SmartObjectClientServerWrapper(clientServer), serviceObjectName, serviceInstanceSettings);
        }

        public static SmartObjectDefinition GetSmartObjectDefinition(string smartObjectName)
        {
            var managementServer = ConnectionHelper.GetServerWrapper<SmartObjectManagementServerWrapper>();
            using (managementServer.BaseAPIServer?.Connection)
            {
                return SmartObjectDefinition.Create(managementServer.GetSmartObjectDefinition(smartObjectName));
            }
        }

        public static string GetSmartObjectName(string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings)
        {
            serviceInstanceSettings.ThrowIfNull("serviceInstanceSettings");

            var managementServer = ConnectionHelper.GetServerWrapper<SmartObjectManagementServerWrapper>();
            using (managementServer.BaseAPIServer?.Connection)
            {
                return GetSmartObjectName(managementServer, serviceObjectName, serviceInstanceSettings);
            }
        }

        public static IEnumerable<SmartObjectInfo> GetSmartObjects(Guid[] guids)
        {
            var managementServer = ConnectionHelper.GetServerWrapper<SmartObjectManagementServerWrapper>();
            using (managementServer.BaseAPIServer?.Connection)
            {
                return managementServer.GetSmartObjects(guids).SmartObjectList.ToArray();
            }
        }

        public static void PublishSmartObjects(SmartObjectDefinitionsPublish smartObjectDefinitionsPublish)
        {
            smartObjectDefinitionsPublish.ThrowIfNull("smartObjectDefinitionsPublish");

            var managementServer = ConnectionHelper.GetServerWrapper<SmartObjectManagementServerWrapper>();
            using (managementServer.BaseAPIServer?.Connection)
            {
                // Delete SmartObjects
                foreach (SmartObjectDefinition smartObject in smartObjectDefinitionsPublish.SmartObjects)
                {
                    SmartObjectHelper.DeleteSmartObject(managementServer, smartObject.Name);
                }

                managementServer.PublishSmartObjects(smartObjectDefinitionsPublish.ToPublishXml());
            }
        }

        public static void PublishSmartObjectsFromResources(Assembly assembly, string category)
        {
            assembly.ThrowIfNull("assembly");

            using (var publishSmO = new SmartObjectDefinitionsPublish())
            {
                // Get the SmartObjects from the embeded resources of the assembly
                foreach (var resource in assembly.GetManifestResourceNames())
                {
                    if (resource.IndexOf(".sodx", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        using (var streamSodx = assembly.GetManifestResourceStream(resource))
                        {
                            var resourceSmartObject = SmartObjectDefinition.Create(streamSodx);
                            resourceSmartObject.AddDeploymentCategory(category);
                            publishSmO.SmartObjects.Add(resourceSmartObject);
                        }
                    }
                }

                // Publish the SmartObjects
                PublishSmartObjects(publishSmO);
            }
        }

        public static void VerifyAllReturnPropertiesHasValues(SmartMethodBase method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            foreach (SmartProperty returnProperty in method.ReturnProperties)
            {
                AssertHelper.IsFalse(string.IsNullOrEmpty(returnProperty.Value));
            }
        }

        public static void VerifyAllReturnPropertiesHasValues(SmartObject smartObject)
        {
            smartObject.ThrowIfNull("smartObject");

            var method = smartObject.GetMethod(smartObject.MethodToExecute);
            SmartObjectHelper.VerifyAllReturnPropertiesHasValues(method);
        }

        public static void VerifyPaging(SmartObjectClientServer clientServer, SmartObject smartObject, int pageSize)
        {
            VerifyPaging(new SmartObjectClientServerWrapper(clientServer), smartObject, pageSize);
        }

        internal static bool ContainsSmartObject(SmartObjectManagementServerWrapper server, string systemName)
        {
            server.ThrowIfNull("server");

            return server.GetSmartObjects(systemName).SmartObjectList.Any();
        }

        internal static void DeleteSmartObject(SmartObjectManagementServerWrapper server, string systemName)
        {
            if (ContainsSmartObject(server, systemName))
            {
                server.DeleteSmartObject(systemName, true);
            }
        }

        internal static SmartObject ExecuteBulkScalar(SmartObjectClientServerWrapper clientServer, SmartObject smartObject, DataTable inputTable)
        {
            clientServer.ThrowIfNull("clientServer");

            try
            {
                return clientServer.ExecuteBulkScalar(smartObject, inputTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessage());
            }
        }

        internal static SmartObjectList ExecuteList(SmartObjectClientServerWrapper clientServer, SmartObject smartObject, ExecuteListOptions options = null)
        {
            clientServer.ThrowIfNull("clientServer");

            if (options == null)
            {
                options = new ExecuteListOptions();
            }

            try
            {
                return clientServer.ExecuteList(smartObject, options);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessage());
            }
        }

        internal static DataTable ExecuteListDataTable(SmartObjectClientServerWrapper clientServer, SmartObject smartObject, ExecuteListOptions options = null)
        {
            clientServer.ThrowIfNull("clientServer");

            if (options == null)
            {
                options = new ExecuteListOptions();
            }

            try
            {
                return clientServer.ExecuteListDataTable(smartObject, options);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessage());
            }
        }

        internal static SmartObjectReader ExecuteListReader(SmartObjectClientServerWrapper clientServer, SmartObject smartObject, ExecuteListReaderOptions options = null)
        {
            clientServer.ThrowIfNull("clientServer");

            if (options == null)
            {
                options = new ExecuteListReaderOptions();
            }

            try
            {
                return clientServer.ExecuteListReader(smartObject, options);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessage());
            }
        }

        internal static SmartObject ExecuteScalar(SmartObjectClientServerWrapper clientServer, SmartObject smartObject)
        {
            clientServer.ThrowIfNull("clientServer");

            try
            {
                return clientServer.ExecuteScalar(smartObject);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessage());
            }
        }

        internal static DataTable ExecuteSQLQueryDataTable(SmartObjectClientServerWrapper clientServer, string query)
        {
            clientServer.ThrowIfNull("clientServer");

            try
            {
                return clientServer.ExecuteSQLQueryDataTable(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessage());
            }
        }

        internal static SmartObject GetSmartObject(SmartObjectClientServerWrapper clientServer, string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings)
        {
            clientServer.ThrowIfNull("clientServer");

            var smartObjectName = GetSmartObjectName(serviceObjectName, serviceInstanceSettings);
            return clientServer.GetSmartObject(smartObjectName);
        }

        internal static string GetSmartObjectName(SmartObjectManagementServerWrapper managementServer, string serviceObjectName, ServiceInstanceSettings serviceInstanceSettings)
        {
            var preSmartObjectName = string.Concat(serviceInstanceSettings.Name, "_");
            var smartObjectExplorer = managementServer.GetSmartObjects(SearchProperty.SystemName, SearchOperator.EndsWith, string.Concat("_", serviceObjectName));
            return (from s in smartObjectExplorer.SmartObjectList
                    where s.Name.StartsWith(preSmartObjectName)
                    select s.Name).FirstOrDefault();
        }

        internal static void VerifyPaging(SmartObjectClientServerWrapper clientServer, SmartObject smartObject, int pageSize)
        {
            var totalDataTable = SmartObjectHelper.ExecuteListDataTable(clientServer, smartObject);
            AssertHelper.IsTrue(totalDataTable.Rows.Count > 0);

            for (int pageNumber = 1; totalDataTable.GetCondition(pageNumber, pageSize); pageNumber++)
            {
                var options = new ExecuteListReaderOptions
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    IncludeTotalRecordCount = (pageNumber % 2) == 1
                };

                var pagedReader = SmartObjectHelper.ExecuteListReader(clientServer, smartObject, options);
                if (options.IncludeTotalRecordCount)
                {
                    AssertHelper.AreEqual(totalDataTable.Rows.Count, pagedReader.TotalRecordCount);
                }
                else
                {
                    AssertHelper.AreEqual(-1, pagedReader.TotalRecordCount);
                }

                using (var pagedResults = new DataTable())
                {
                    pagedResults.Load(pagedReader);

                    SmartObjectHelper.CompareDataTables(totalDataTable.GetPagedResult(pageNumber, pageSize),
                        pagedResults);
                }
            }
        }
    }
}