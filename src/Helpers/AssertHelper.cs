using System;
using System.Reflection;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    internal static class AssertHelper
    {
        public static void AreEqual<T>(T expected, T actual, string message = null, params object[] parameters)
        {
            if (expected?.Equals(actual) == true)
            {
                return;
            }

            var formattedMessage = FormatMessage(message, parameters);

            throw new Exception($"{MethodInfo.GetCurrentMethod().Name}{formattedMessage}\r\nExpected:{expected?.ToString() ?? string.Empty}\r\nActual{actual?.ToString() ?? string.Empty}");
        }

        public static void Fail(string message = null, params object[] parameters)
        {
            var formattedMessage = FormatMessage(message, parameters);

            throw new Exception($"{MethodInfo.GetCurrentMethod().Name}{formattedMessage}");
        }

        public static void IsFalse(bool condition, string message = null, params object[] parameters)
        {
            if (!condition)
            {
                return;
            }

            var formattedMessage = FormatMessage(message, parameters);

            throw new Exception($"{MethodInfo.GetCurrentMethod().Name}{formattedMessage}");
        }

        public static void IsNotNull(object value, string message = null, params object[] parameters)
        {
            if (value != null)
            {
                return;
            }

            var formattedMessage = FormatMessage(message, parameters);

            throw new Exception($"{MethodInfo.GetCurrentMethod().Name} Value: {value}{formattedMessage}");
        }

        public static void IsTrue(bool condition, string message = null, params object[] parameters)
        {
            if (condition)
            {
                return;
            }

            var formattedMessage = FormatMessage(message, parameters);

            throw new Exception($"{MethodInfo.GetCurrentMethod().Name}{formattedMessage}");
        }

        private static string FormatMessage(string message, params object[] parameters)
        {
            if (message == null)
            {
                message = string.Empty;
            }
            else
            {
                if (parameters?.Length > 0)
                {
                    message = string.Format(message, parameters);
                }

                message = $"\r\n{message}";
            }

            return message;
        }
    }
}