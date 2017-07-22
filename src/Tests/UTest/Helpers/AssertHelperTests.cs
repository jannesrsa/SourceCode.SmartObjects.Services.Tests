using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class AssertHelperTests
    {
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AreEqual_False()
        {
            // Action
            AssertHelper.AreEqual(true, false);
        }

        [TestMethod()]
        public void AreEqual_True()
        {
            // Action
            AssertHelper.AreEqual(true, true);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void Fail_WithMessage()
        {
            // Action
            AssertHelper.Fail(Guid.NewGuid().ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void IsTrue_WithFalse()
        {
            // Action
            AssertHelper.IsTrue(false, Guid.NewGuid().ToString());
        }

        [TestMethod()]
        public void IsTrue_WithTrue()
        {
            // Action
            AssertHelper.IsTrue(true, Guid.NewGuid().ToString());
        }
    }
}