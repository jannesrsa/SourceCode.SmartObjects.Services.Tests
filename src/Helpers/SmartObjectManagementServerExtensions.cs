using System;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.Helpers;

namespace SourceCode.SmartObjects.Services.Tests.Extensions
{
    public static class SmartObjectManagementServerExtensions
    {
        public static bool ContainsSmartObject(this SmartObjectManagementServer server, string systemName)
        {
            return ConnectionHelper.GetSmartObjectManagementServerWrapper(server).ContainsSmartObject(systemName);
        }

        public static void DeleteSmartObject(this SmartObjectManagementServer server, string systemName)
        {
            ConnectionHelper.GetSmartObjectManagementServerWrapper(server).DeleteSmartObject(systemName);
        }

        public static void DeleteSmartObjects(this SmartObjectManagementServer server, Guid serviceInstanceGuid)
        {
            ConnectionHelper.GetSmartObjectManagementServerWrapper(server).DeleteSmartObjects(serviceInstanceGuid);
        }
    }
}