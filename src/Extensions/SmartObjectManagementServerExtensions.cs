using System;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Extensions
{
    public static class SmartObjectManagementServerExtensions
    {
        public static bool ContainsSmartObject(this SmartObjectManagementServer server, string systemName)
        {
            return WrapperFactory.Instance.GetSmartObjectManagementServerWrapper(server).ContainsSmartObject(systemName);
        }

        public static void DeleteSmartObject(this SmartObjectManagementServer server, string systemName)
        {
            WrapperFactory.Instance.GetSmartObjectManagementServerWrapper(server).DeleteSmartObject(systemName);
        }

        public static void DeleteSmartObjects(this SmartObjectManagementServer server, Guid serviceInstanceGuid)
        {
            WrapperFactory.Instance.GetSmartObjectManagementServerWrapper(server).DeleteSmartObjects(serviceInstanceGuid);
        }
    }
}