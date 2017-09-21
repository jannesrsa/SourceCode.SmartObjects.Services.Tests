using System;
using SourceCode.Hosting.Client.BaseAPI;

namespace SourceCode.SmartObjects.Services.Tests.Interfaces
{
    public interface IBaseAPI
    {
        BaseAPI BaseAPIServer { get; }
    }
}