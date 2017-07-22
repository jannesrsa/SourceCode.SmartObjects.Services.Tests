using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class PackageDeploymentHelperTests
    {
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void DeployPackage_WithDefaultValues()
        {
            // Action
            PackageDeploymentHelper.DeployPackage(null);
        }

        [TestMethod()]
        public void DeployPackages_WithAssembly()
        {
            // Action
            PackageDeploymentHelper.DeployPackages(typeof(PackageDeploymentHelper).Assembly);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DeployPackages_WithNullAssembly()
        {
            // Action
            PackageDeploymentHelper.DeployPackages(null);
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }
    }
}