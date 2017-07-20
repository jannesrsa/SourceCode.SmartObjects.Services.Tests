using System;
using SourceCode.Hosting.Client.BaseAPI;

namespace SourceCode.SmartObjects.Services.Tests.Interfaces
{
    public interface IConnectionProvider
    {
        SCConnectionStringBuilder SmartObjectConnectionStringBuilder { get; }

        SCConnectionStringBuilder WorkflowConnectionStringBuilder { get; }

        string GetCurrentUser();

        T GetServer<T>() where T : class, IBaseAPI;

        TResult Invoke<TServer, TResult>(Func<TResult> func, ref TServer server)
            where TServer : BaseAPI, new();
    }
}