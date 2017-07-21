using System;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interfaces;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    /// <summary>
    /// ConnectionHelper
    /// </summary>
    public static class ConnectionHelper
    {
        private static IConnectionProvider _connectionProvider = new DefaultConnectionHelperProvider();

        public static SCConnectionStringBuilder SmartObjectConnectionStringBuilder
        {
            get
            {
                return _connectionProvider.SmartObjectConnectionStringBuilder;
            }
        }

        public static SCConnectionStringBuilder WorkflowConnectionStringBuilder
        {
            get
            {
                return _connectionProvider.WorkflowConnectionStringBuilder;
            }
        }

        public static string GetCurrentUser()
        {
            return _connectionProvider.GetCurrentUser();
        }

        public static T GetServer<T>()
            where T : BaseAPI, new()
        {
            switch (typeof(T).Name.ToString())
            {
                case "ServiceManagementServer":
                    return (T)GetServerWrapper<ServiceManagementServerWrapper>().BaseAPIServer;

                case "SmartObjectClientServer":
                    return (T)GetServerWrapper<SmartObjectClientServerWrapper>().BaseAPIServer;

                case "SmartObjectManagementServer":
                    return (T)GetServerWrapper<SmartObjectManagementServerWrapper>().BaseAPIServer;
            }

            T server = new T();

            server.CreateConnection();
            server.Connection.Open(_connectionProvider.SmartObjectConnectionStringBuilder.ConnectionString);

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

        internal static T GetServerWrapper<T>() where T : class, IBaseAPI
        {
            return (T)_connectionProvider.GetServer<T>();
        }

        internal static void UpdateConnectionProvider(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
    }
}