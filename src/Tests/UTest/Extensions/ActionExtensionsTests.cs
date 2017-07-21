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

            // Act
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        public void AssertException_WithMatchingExceptionType()
        {
            //Arrange
            var exception = new Exception(Guid.NewGuid().ToString());
            Action action = () => throw exception;

            // Act
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        public void AssertException_WithMatchingExceptionTypeAndMessage()
        {
            //Arrange
            var exception = new Exception(Guid.NewGuid().ToString());
            Action action = () => throw exception;

            // Act
            ActionExtensions.AssertException<Exception>(action, exception.Message);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AssertException_WithNotThrowingException()
        {
            //Arrange
            Action action = () => Console.WriteLine("Test");

            // Act
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void AssertException_WithnWrongExceptionType()
        {
            //Arrange
            Action action = () => throw new NotImplementedException();

            // Act
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AssertException_WithWrongMessage()
        {
            //Arrange
            Action action = () => throw new Exception(Guid.NewGuid().ToString());

            // Act
            ActionExtensions.AssertException<Exception>(action, Guid.NewGuid().ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void IgnoreException_WithActionNull()
        {
            // Act
            ActionExtensions.IgnoreException(null);
        }

        [TestMethod()]
        public void IgnoreException_WithException()
        {
            //Arrange
            Action action = () => throw new NotImplementedException();

            // Act
            ActionExtensions.IgnoreException(action);
        }

        [TestMethod()]
        public void IgnoreException_WithNotThrowingException()
        {
            //Arrange
            Action action = () => Console.WriteLine("Test");

            // Act
            ActionExtensions.IgnoreException(action);
        }
    }
}