using System;
using System.Collections.Generic;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    public static class EnvironmentHelper
    {
        private readonly static Dictionary<string, string> _cachedEnvironmentFields = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public static string GetEnvironmentFieldByName(string name)
        {
            string value;
            if (_cachedEnvironmentFields.TryGetValue(name, out value))
            {
                return value;
            }

            var server = ConnectionHelper.GetEnvironmentSettingsManagerWrapper(null);
            var field = server.GetItemByName(name);

            _cachedEnvironmentFields[name] = field.Value;

            return field.Value;
        }

        public static class FieldNames
        {
            internal const string SmartFormsRuntime = "SmartForms Runtime";
        }
    }
}