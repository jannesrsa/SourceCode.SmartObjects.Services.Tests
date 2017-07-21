using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class AssertHelperTests
    {
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void Fail_WithMessage()
        {
            AssertHelper.Fail(Guid.NewGuid().ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void IsTrue_WithFalse()
        {
            AssertHelper.IsTrue(false, Guid.NewGuid().ToString());
        }

        [TestMethod()]
        public void IsTrue_WithTrue()
        {
            AssertHelper.IsTrue(true, Guid.NewGuid().ToString());
        }
    }
}