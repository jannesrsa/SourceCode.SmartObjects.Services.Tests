using System;
using SourceCode.Deployment.Management;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    /// <summary>
    /// ConnectionHelper
    /// </summary>
    public static class ConnectionHelper
    {
        private static WrapperFactory _factory;

        static ConnectionHelper()
        {
            ResetWrapperFactory();
        }

        public static SCConnectionStringBuilder SmartObjectConnectionStringBuilder
        {
            get
            {
                return _factory.SCConnectionStringBuilder;
            }
        }

        public static SCConnectionStringBuilder WorkflowConnectionStringBuilder
        {
            get
            {
                var workflowConnectionStringBuilder = new SCConnectionStringBuilder(_factory.SCConnectionStringBuilder.ConnectionString);
                workflowConnectionStringBuilder.Port = 5252;
                return workflowConnectionStringBuilder;
            }
        }

        public static string GetCurrentUser()
        {
            var currentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            var userFQN = string.Concat("K2:", currentUserName);
            return userFQN;
        }

        public static T GetServer<T>()
            where T : BaseAPI, new()
        {
            return _factory.GetServer<T>();
        }

        public static TResult Invoke<TServer, TResult>(Func<TResult> func, ref TServer server)
            where TServer : BaseAPI, new()
        {
            func.ThrowIfNull("func");

            if (server == null)
            {
                server = GetServer<TServer>();
                using (server.Connection)
                {
                    return func();
                }
            }
            else
            {
                return func();
            }
        }

        internal static EnvironmentSettingsManagerWrapper GetEnvironmentSettingsManagerWrapper(EnvironmentSettingsManager server)
        {
            return _factory.GetEnvironmentSettingsManagerWrapper(server);
        }

        internal static PackageDeploymentManagerWrapper GetPackageDeploymentManagerWrapper(PackageDeploymentManager server)
        {
            return _factory.GetPackageDeploymentManagerWrapper(server);
        }

        internal static ServiceManagementServerWrapper GetServiceManagementServerWrapper(ServiceManagementServer server)
        {
            return _factory.GetServiceManagementServerWrapper(server);
        }

        internal static SmartObjectClientServerWrapper GetSmartObjectClientServerWrapper(SmartObjectClientServer server)
        {
            return _factory.GetSmartObjectClientServerWrapper(server);
        }

        internal static SmartObjectManagementServerWrapper GetSmartObjectManagementServerWrapper(SmartObjectManagementServer server)
        {
            return _factory.GetSmartObjectManagementServerWrapper(server);
        }

        internal static WebRequestWrapper GetWebRequestWrapper()
        {
            return _factory.GetWebRequestWrapper();
        }

        internal static void ResetWrapperFactory()
        {
            _factory = new WrapperFactory();
        }

        internal static void UpdateWrapperFactory(WrapperFactory factory)
        {
            _factory = factory;
        }
    }
}