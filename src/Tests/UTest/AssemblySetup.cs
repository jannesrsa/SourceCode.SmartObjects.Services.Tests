using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class AssemblySetup
    {
        private static MockWrapperFactory _mockWrapperFactory;

        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext context)
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }
    }
}