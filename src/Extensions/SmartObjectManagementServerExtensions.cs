using System;
using System.Linq;
using SourceCode.SmartObjects.Management;

namespace SourceCode.SmartObjects.Services.Tests.Extensions
{
    public static class SmartObjectManagementServerExtensions
    {
        public static bool ContainsSmartObject(this SmartObjectManagementServer server, string systemName)
        {
            server.ThrowIfNull("server");

            return server.GetSmartObjects(systemName).SmartObjectList.Any();
        }

        public static void DeleteSmartObject(this SmartObjectManagementServer server, string systemName)
        {
            server.ThrowIfNull("server");

            if (server.ContainsSmartObject(systemName))
            {
                server.DeleteSmartObject(systemName, true);
            }
        }

        public static void DeleteSmartObjects(this SmartObjectManagementServer server, Guid serviceInstanceGuid)
        {
            server.ThrowIfNull("server");

            foreach (SmartObjectInfo smartObject in server.GetSmartObjects(serviceInstanceGuid).SmartObjects)
            {
                server.DeleteSmartObject(smartObject.Name);
            }
        }
    }
}