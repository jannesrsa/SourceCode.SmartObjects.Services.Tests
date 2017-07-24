using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Managers
{
    public class SmartObjectsManager
    {
        private readonly ServiceInstanceSettings _serviceInstanceSettings;

        public SmartObjectsManager(ServiceInstanceSettings serviceInstanceSettings)
        {
            _serviceInstanceSettings = serviceInstanceSettings;
        }

        public ServiceInstanceSettings ServiceInstanceSettings
        {
            get { return _serviceInstanceSettings; }
        }

        public void Delete()
        {
            var smartObjectManagementServer = WrapperFactory.Instance.GetServer<SmartObjectManagementServer>();
            using (smartObjectManagementServer.Connection)
            {
                var smartObjectManagementServerWrapper = WrapperFactory.Instance.GetSmartObjectManagementServerWrapper(smartObjectManagementServer);
                foreach (SmartObjectInfo smartObject in smartObjectManagementServerWrapper.GetSmartObjects(_serviceInstanceSettings.Guid).SmartObjects)
                {
                    smartObjectManagementServerWrapper.DeleteSmartObject(smartObject.Name);
                }
            }
        }

        public void Register()
        {
            var managementServer = WrapperFactory.Instance.GetServer<SmartObjectManagementServer>();
            using (managementServer.Connection)
            {
                var smartObjectManagementServerWrapper = WrapperFactory.Instance.GetSmartObjectManagementServerWrapper(managementServer);
                smartObjectManagementServerWrapper.GenerateSmartObjects(_serviceInstanceSettings.Guid, true, true, true);
            }
        }
    }
}