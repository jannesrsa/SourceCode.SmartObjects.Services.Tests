using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenIgnoreExceptionIsCalledOnActionExtensions
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithActionNull()
        {
            // Act
            ActionExtensions.IgnoreException(null);
        }

        [TestMethod()]
        public void WithNotThrowingException()
        {
            //Arrange
            Action action = () => Console.WriteLine("Test");

            // Act
            ActionExtensions.IgnoreException(action);
        }

        [TestMethod()]
        public void WithException()
        {
            //Arrange
            Action action = () => throw new NotImplementedException();

            // Act
            ActionExtensions.IgnoreException(action);
        }

    }
}