using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class ValidationExtensionsTests
    {
        [TestMethod()]
        public void ThrowIfNull_ValidObject()
        {
            ValidationExtensions.ThrowIfNull(new object(), Guid.NewGuid().ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowIfNull_Null()
        {
            ValidationExtensions.ThrowIfNull(default(object), Guid.NewGuid().ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowIfNullOrWhiteSpace_Null()
        {
            ValidationExtensions.ThrowIfNullOrWhiteSpace(default(string), Guid.NewGuid().ToString());
        }

        [TestMethod()]
        public void ThrowIfNullOrWhiteSpace_ValidString()
        {
            ValidationExtensions.ThrowIfNullOrWhiteSpace(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowIfNullOrWhiteSpace_WhiteSpace()
        {
            ValidationExtensions.ThrowIfNullOrWhiteSpace(string.Empty, Guid.NewGuid().ToString());
        }
    }
}