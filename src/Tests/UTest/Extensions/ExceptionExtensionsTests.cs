﻿using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Client;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class ExceptionExtensionsTests
    {
        [TestMethod()]
        public void GetExceptionMessage_WithException()
        {
            //Arrange
            string expected = "Test";
            var exception = new InvalidOperationException(expected);

            // Action
            var actual = ExceptionExtensions.GetExceptionMessage(exception);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetExceptionMessage_WithNullException()
        {
            //Arrange
            Exception exception = null;

            // Action
            ExceptionExtensions.GetExceptionMessage(exception);
        }

        [TestMethod()]
        public void GetExceptionMessage_WithSmartObjectException()
        {
            //Arrange
            var ctor = typeof(SmartObjectExceptionData).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault();

            string serviceName = Guid.NewGuid().ToString();
            string serviceGuid = Guid.NewGuid().ToString();
            string message = Guid.NewGuid().ToString();
            string innerExceptionMessage = Guid.NewGuid().ToString();
            const SeverityType severity = SeverityType.Error;

            var smartObjectExceptionData = (SmartObjectExceptionData)ctor.Invoke(new object[] { serviceName, serviceGuid, message, innerExceptionMessage, severity });

            var collection = new SmartObjectExceptionDataCollection
            {
                smartObjectExceptionData
            };
            var exception = new SmartObjectException(collection);
            string expected = $@"Service: {serviceName}
Service Guid: {serviceGuid}
Severity: {severity.ToString()}
Error Message: {message}
InnerException Message: {innerExceptionMessage}";

            // Action
            var actual = ExceptionExtensions.GetExceptionMessage(exception);

            // Assert
            Assert.AreEqual(Regex.Replace(expected.Trim(), @"\s+", " "), Regex.Replace(actual.Trim(), @"\s+", " "));
        }
    }
}