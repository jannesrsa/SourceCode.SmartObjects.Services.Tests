using System;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    /// <summary>
    /// ConnectionHelper
    /// </summary>
    public static class ConnectionHelper
    {
        private static readonly SCConnectionStringBuilder _connBuilder = new SCConnectionStringBuilder();

        static ConnectionHelper()
        {
            _connBuilder.Host = Environment.MachineName;
            _connBuilder.Port = 5555;
            _connBuilder.Integrated = true;
            _connBuilder.IsPrimaryLogin = true;
        }

        public static SCConnectionStringBuilder SmartObjectConnectionStringBuilder
        {
            get
            {
                return ConnectionHelper._connBuilder;
            }
        }

        public static SCConnectionStringBuilder WorkflowConnectionStringBuilder
        {
            get
            {
                var workflowConnectionStringBuilder = new SCConnectionStringBuilder(ConnectionHelper._connBuilder.ConnectionString);
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
            T server = new T();

            server.CreateConnection();
            server.Connection.Open(_connBuilder.ConnectionString);

            return server;
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
    }
}