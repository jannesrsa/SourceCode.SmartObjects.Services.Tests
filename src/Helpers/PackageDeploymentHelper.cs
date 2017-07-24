using System;
using System.Linq;
using System.Reflection;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    public static class PackageDeploymentHelper
    {
        private const string _sessionName = "SmartObjectsServicesTestsHelpers";

        public static void DeployPackage(byte[] package)
        {
            var packageDeploymentManager = WrapperFactory.Instance.GetPackageDeploymentManagerWrapper(null);
            packageDeploymentManager.DeployPackage(package);
        }

        public static void DeployPackages(Assembly assembly)
        {
            assembly.ThrowIfNull("assembly");

            var resources = assembly.GetManifestResourceNames().Where(i => i.IndexOf(".kspx", StringComparison.OrdinalIgnoreCase) >= 0);

            var packageDeploymentManager = WrapperFactory.Instance.GetPackageDeploymentManagerWrapper(null);
            packageDeploymentManager.DeployPackages(assembly, resources);
        }
    }
}