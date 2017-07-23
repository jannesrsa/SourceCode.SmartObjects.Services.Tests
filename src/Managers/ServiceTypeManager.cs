using System;
using System.Linq;
using SourceCode.SmartObjects.Services.Management;
using SourceCode.SmartObjects.Services.Tests.Helpers;

namespace SourceCode.SmartObjects.Services.Tests.Managers
{
    public class ServiceTypeManager
    {
        private readonly string _className;
        private readonly string _displayName;
        private readonly Guid _guid;
        private readonly string _name;
        private readonly bool _registered;

        public ServiceTypeManager(ServiceTypeSettings serviceTypeSettings)
        {
            if (serviceTypeSettings == null)
            {
                throw new ArgumentNullException("serviceTypeSettings");
            }

            _guid = serviceTypeSettings.DefaultGuid;
            _name = serviceTypeSettings.DefaultName;
            _displayName = serviceTypeSettings.DefaultDisplayName;
            _className = serviceTypeSettings.ClassName;

            // Use the existing ServiceType's values
            if (!serviceTypeSettings.AlwaysUseDefaults)
            {
                var server = ConnectionHelper.GetServiceManagementServerWrapper(null);
                var serviceTypeCollection = server.GetServiceTypes();
                var serviceType = serviceTypeCollection.FirstOrDefault(i => i.Class.Equals(_className));

                if (serviceType != null)
                {
                    _registered = true;

                    _guid = serviceType.Guid;
                    _name = serviceType.Name;
                    _displayName = serviceType.DisplayName;
                }
            }
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        public void Delete()
        {
            var server = ConnectionHelper.GetServer<ServiceManagementServer>();
            using (server.Connection)
            {
                var serviceManagementServerWrapper = ConnectionHelper.GetServiceManagementServerWrapper(server);
                serviceManagementServerWrapper.DeleteServiceType(_guid, false);
            }
        }

        public void Register()
        {
            if (_registered)
            {
                return;
            }

            var server = ConnectionHelper.GetServer<ServiceManagementServer>();
            using (server.Connection)
            {
                var serviceManagementServerWrapper = ConnectionHelper.GetServiceManagementServerWrapper(server);

                var service = (from si in serviceManagementServerWrapper.GetRegisterableServices()
                               where si.Key == _className
                               select si).FirstOrDefault();

                var path = service.Value;

                serviceManagementServerWrapper.RegisterServiceType(
                    _guid,
                    _name,
                    _displayName,
                    _className,
                    path,
                    _className);
            }
        }
    }
}