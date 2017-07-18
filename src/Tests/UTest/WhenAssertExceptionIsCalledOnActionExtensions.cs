using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenAssertExceptionIsCalledOnActionExtensions
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithActionNull()
        {
            // Act
            ActionExtensions.AssertException<Exception>(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void WithNotThrowingException()
        {
            //Arrange
            Action action = () => Console.WriteLine("Test");

            // Act
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotImplementedException))]
        public void WithnWrongExceptionType()
        {
            //Arrange
            Action action = () => throw new NotImplementedException();

            // Act
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void WithWrongMessage()
        {
            //Arrange
            Action action = () => throw new Exception(Guid.NewGuid().ToString());

            // Act
            ActionExtensions.AssertException<Exception>(action, Guid.NewGuid().ToString());
        }

        [TestMethod()]
        public void WithMatchingExceptionType()
        {
            //Arrange
            var exception = new Exception(Guid.NewGuid().ToString());
            Action action = () => throw exception;

            // Act
            ActionExtensions.AssertException<Exception>(action);
        }

        [TestMethod()]
        public void WithMatchingExceptionTypeAndMessage()
        {
            //Arrange
            var exception = new Exception(Guid.NewGuid().ToString());
            Action action = () => throw exception;

            // Act
            ActionExtensions.AssertException<Exception>(action, exception.Message);
        }
    }
}