using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using SourceCode.Deployment.Management;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Interfaces;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    [ExcludeFromCodeCoverage]
    internal class PackageDeploymentManagerWrapper : IBaseAPI
    {
        private const string _sessionName = "SmartObjectsServicesTestsHelpers";
        private readonly PackageDeploymentManager _packageDeploymentManager;

        public PackageDeploymentManagerWrapper(PackageDeploymentManager packageDeploymentManager)
        {
            packageDeploymentManager.ThrowIfNull(nameof(packageDeploymentManager));

            _packageDeploymentManager = packageDeploymentManager;
        }

        public PackageDeploymentManagerWrapper()
        {
        }

        public BaseAPI BaseAPIServer
        {
            get
            {
                return _packageDeploymentManager;
            }
        }

        internal virtual void DeployPackage(byte[] package)
        {
            using (_packageDeploymentManager.Connection)
            {
                using (var fileStream = new MemoryStream(package))
                using (var session = _packageDeploymentManager.CreateSession(_sessionName, fileStream))
                {
                    session.Deploy();
                }
            }
        }

        internal virtual void DeployPackages(Assembly assembly, IEnumerable<string> resources)
        {
            using (_packageDeploymentManager.Connection)
            {
                // Get the KSPX package from the embeded resources of the assembly
                foreach (var resource in resources)
                {
                    using (var streamKspx = assembly.GetManifestResourceStream(resource))
                    using (var session = _packageDeploymentManager.CreateSession(resource))
                    {
                        session.Load(streamKspx);
                        session.Deploy();
                    }
                }
            }
        }
    }
}