using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenGetExceptionMessageCalledOnExceptionExtensions
    {
        [TestMethod()]
        public void WithException()
        {
            //Arrange
            string expected = "Test";
            Exception exception = new Exception(expected);

            // Act
            var actual = ExceptionExtensions.GetExceptionMessage(exception);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithNullException()
        {
            //Arrange
            Exception exception = null;

            // Act
            ExceptionExtensions.GetExceptionMessage(exception);
        }

        [TestMethod()]
        public void WithSmartObjectException()
        {
            //Arrange

            var ctor = typeof(SmartObjectExceptionData).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault();

            string serviceName = Guid.NewGuid().ToString();
            string serviceGuid = Guid.NewGuid().ToString();
            string message = Guid.NewGuid().ToString();
            string innerExceptionMessage = Guid.NewGuid().ToString();
            SeverityType severity = SeverityType.Error;

            var smartObjectExceptionData = (SmartObjectExceptionData)ctor.Invoke(new object[] { serviceName, serviceGuid, message, innerExceptionMessage, severity });

            var collection = new SmartObjectExceptionDataCollection();
            collection.Add(smartObjectExceptionData);

            var exception = new SmartObjectException(collection);
            string expected = $@"Service: {serviceName}
Service Guid: {serviceGuid}
Severity: {severity}
Error Message: {message}
InnerException Message: {innerExceptionMessage}";

            // Act
            var actual = ExceptionExtensions.GetExceptionMessage(exception);

            // Assert
            Assert.AreEqual(expected.Trim(), actual.Trim());
        }
    }
}