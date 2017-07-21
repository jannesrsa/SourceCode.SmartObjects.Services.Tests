using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Helpers;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class WrapperFactory
    {
        internal virtual SmartObjectManagementServerWrapper GetSmartObjectManagementServerWrapper(SmartObjectManagementServer server)
        {
            if (server == null)
            {
                server = ConnectionHelper.GetServer<SmartObjectManagementServer>();
            }

            return new SmartObjectManagementServerWrapper(server);
        }

        internal virtual SmartObjectClientServerWrapper GetSmartObjectClientServerWrapper(SmartObjectClientServer server)
        {
            if (server == null)
            {
                server = ConnectionHelper.GetServer<SmartObjectClientServer>();
            }

            return new SmartObjectClientServerWrapper(server);
        }

        internal virtual ServiceManagementServerWrapper GetServiceManagementServerWrapper(ServiceManagementServer server)
        {
            if (server == null)
            {
                server = ConnectionHelper.GetServer<ServiceManagementServer>();
            }

            return new ServiceManagementServerWrapper(server);
        }
    }
}