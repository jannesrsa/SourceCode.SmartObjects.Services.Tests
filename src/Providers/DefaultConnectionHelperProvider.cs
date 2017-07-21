using System;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Services.Tests.Interfaces;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class DefaultConnectionHelperProvider : IConnectionProvider
    {
        public DefaultConnectionHelperProvider()
        {
            SmartObjectConnectionStringBuilder.Host = Environment.MachineName;
            SmartObjectConnectionStringBuilder.Port = 5555;
            SmartObjectConnectionStringBuilder.Integrated = true;
            SmartObjectConnectionStringBuilder.IsPrimaryLogin = true;
        }

        public SCConnectionStringBuilder SmartObjectConnectionStringBuilder { get; } = new SCConnectionStringBuilder();

        public SCConnectionStringBuilder WorkflowConnectionStringBuilder
        {
            get
            {
                var workflowConnectionStringBuilder = new SCConnectionStringBuilder(SmartObjectConnectionStringBuilder.ConnectionString);
                workflowConnectionStringBuilder.Port = 5252;
                return workflowConnectionStringBuilder;
            }
        }

        public string GetCurrentUser()
        {
            var currentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            var userFQN = string.Concat("K2:", currentUserName);
            return userFQN;
        }

        T IConnectionProvider.GetServer<T>()
        {
            T server;

            switch (typeof(T).Name.ToString())
            {
                case "ServiceManagementServerWrapper":
                    server = new ServiceManagementServerWrapper() as T;
                    break;

                case "SmartObjectClientServerWrapper":
                    server = new SmartObjectClientServerWrapper() as T;
                    break;

                case "SmartObjectManagementServerWrapper":
                    server = new SmartObjectManagementServerWrapper() as T;
                    break;

                default:
                    return null;
            }

            server.BaseAPIServer?.CreateConnection();
            server.BaseAPIServer?.Connection?.Open(SmartObjectConnectionStringBuilder.ConnectionString);

            return server;
        }

        public TResult Invoke<TServer, TResult>(Func<TResult> func, ref TServer server) where TServer : BaseAPI, new()
        {
            throw new NotImplementedException();
        }
    }
}