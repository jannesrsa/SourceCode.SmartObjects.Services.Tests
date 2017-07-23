using System.Diagnostics.CodeAnalysis;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class EnvironmentSettingsManagerWrapper
    {
        private EnvironmentSettingsManager _environmentSettingsManager;

        [ExcludeFromCodeCoverage]
        public EnvironmentSettingsManagerWrapper(EnvironmentSettingsManager environmentSettingsManager)
        {
            environmentSettingsManager.ThrowIfNull(nameof(environmentSettingsManager));

            _environmentSettingsManager = environmentSettingsManager;
        }

        public EnvironmentSettingsManagerWrapper()
        {
        }

        [ExcludeFromCodeCoverage]
        public virtual void Dispose()
        {
            _environmentSettingsManager?.Dispose();
        }

        [ExcludeFromCodeCoverage]
        internal virtual EnvironmentField GetItemByName(string fieldName)
        {
            using (_environmentSettingsManager)
            {
                var template = _environmentSettingsManager.EnvironmentTemplates.DefaultTemplate;
                var environment = template.DefaultEnvironment;

                _environmentSettingsManager.GetEnvironmentFields(environment);

                return environment.EnvironmentFields.GetItemByName(fieldName);
            }
        }
    }
}