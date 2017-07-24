using System;
using System.Diagnostics.CodeAnalysis;
using SourceCode.Deployment.Management;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Helpers;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    [ExcludeFromCodeCoverage]
    internal class WrapperFactory
    {
        private static WrapperFactory _instance = new WrapperFactory();

        internal WrapperFactory()
        {
            SCConnectionStringBuilder.Host = Environment.MachineName;
            SCConnectionStringBuilder.Port = 5555;
            SCConnectionStringBuilder.Integrated = true;
            SCConnectionStringBuilder.IsPrimaryLogin = true;
        }

        public static WrapperFactory Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        internal SCConnectionStringBuilder SCConnectionStringBuilder { get; } = new SCConnectionStringBuilder();

        internal virtual EnvironmentSettingsManagerWrapper GetEnvironmentSettingsManagerWrapper(EnvironmentSettingsManager server)
        {
            if (server == null)
            {
                server = new EnvironmentSettingsManager(false, false);
                server.ConnectToServer(ConnectionHelper.SmartObjectConnectionStringBuilder.ConnectionString);
                server.InitializeSettingsManager(true);
            }

            return new EnvironmentSettingsManagerWrapper(server);
        }

        internal virtual PackageDeploymentManagerWrapper GetPackageDeploymentManagerWrapper(PackageDeploymentManager server)
        {
            if (server == null)
            {
                server = ConnectionHelper.GetServer<PackageDeploymentManager>();
            }

            return new PackageDeploymentManagerWrapper(server);
        }

        internal virtual SecurityWrapper GetSecurityWrapper()
        {
            return new SecurityWrapper();
        }

        internal virtual T GetServer<T>() where T : BaseAPI, new()
        {
            T server = new T();

            server.CreateConnection();
            server.Connection.Open(SCConnectionStringBuilder.ConnectionString);

            return server;
        }

        internal virtual ServiceManagementServerWrapper GetServiceManagementServerWrapper(ServiceManagementServer server)
        {
            if (server == null)
            {
                server = ConnectionHelper.GetServer<ServiceManagementServer>();
            }

            return new ServiceManagementServerWrapper(server);
        }

        internal virtual SmartObjectClientServerWrapper GetSmartObjectClientServerWrapper(SmartObjectClientServer server)
        {
            if (server == null)
            {
                server = ConnectionHelper.GetServer<SmartObjectClientServer>();
            }

            return new SmartObjectClientServerWrapper(server);
        }

        internal virtual SmartObjectManagementServerWrapper GetSmartObjectManagementServerWrapper(SmartObjectManagementServer server)
        {
            if (server == null)
            {
                server = ConnectionHelper.GetServer<SmartObjectManagementServer>();
            }

            return new SmartObjectManagementServerWrapper(server);
        }

        internal virtual WebRequestWrapper GetWebRequestWrapper()
        {
            return new WebRequestWrapper();
        }
    }
}