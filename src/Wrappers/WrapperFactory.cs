using System;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Helpers;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class WrapperFactory
    {
        internal WrapperFactory()
        {
            SCConnectionStringBuilder.Host = Environment.MachineName;
            SCConnectionStringBuilder.Port = 5555;
            SCConnectionStringBuilder.Integrated = true;
            SCConnectionStringBuilder.IsPrimaryLogin = true;
        }

        internal SCConnectionStringBuilder SCConnectionStringBuilder { get; } = new SCConnectionStringBuilder();

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
    }
}