using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class ActionExtensionsTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AssertException_WithActionNull()
        {
            //Arrange
            Action action = null;

            // Action
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        public void AssertException_WithMatchingExceptionType()
        {
            //Arrange
            var exception = new InvalidOperationException(Guid.NewGuid().ToString());
            Action action = () => throw exception;

            // Action
            ActionExtensions.AssertException<InvalidOperationException>(action);
        }

        [TestMethod()]
        public void AssertException_WithMatchingExceptionTypeAndMessage()
        {
            //Arrange
            var exception = new InvalidOperationException(Guid.NewGuid().ToString());
            Action action = () => throw exception;

            // Action
            ActionExtensions.AssertException<InvalidOperationException>(action, exception.Message);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssertException_WithNotThrowingException()
        {
            //Arrange
            Action action = () => { Assert.AreEqual(string.Empty, string.Empty); };

            // Action
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void AssertException_WithWrongExceptionType()
        {
            //Arrange
            Action action = () => throw new NotImplementedException();

            // Action
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssertException_WithWrongMessage()
        {
            //Arrange
            Action action = () => throw new ArgumentNullException(Guid.NewGuid().ToString());

            // Action
            ActionExtensions.AssertException<ArgumentNullException>(action, Guid.NewGuid().ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void IgnoreException_WithActionNull()
        {
            // Action
            ActionExtensions.IgnoreException(null);
        }

        [TestMethod()]
        public void IgnoreException_WithException()
        {
            //Arrange
            Action action = () => { Assert.AreEqual(Guid.NewGuid(), Guid.NewGuid()); };

            // Action
            ActionExtensions.IgnoreException(action);
        }

        [TestMethod()]
        public void IgnoreException_WithNotThrowingException()
        {
            //Arrange
            Action action = () => { Assert.AreEqual(string.Empty, string.Empty); };

            // Action
            ActionExtensions.IgnoreException(action);
        }
    }
}