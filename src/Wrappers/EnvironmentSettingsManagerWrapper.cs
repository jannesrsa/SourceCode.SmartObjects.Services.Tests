using System;
using System.Diagnostics.CodeAnalysis;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    internal class EnvironmentSettingsManagerWrapper : IDisposable
    {
        private EnvironmentSettingsManager _environmentSettingsManager;

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
            var template = _environmentSettingsManager.EnvironmentTemplates.DefaultTemplate;
            var environment = template.DefaultEnvironment;

            _environmentSettingsManager.GetEnvironmentFields(environment);

            return environment.EnvironmentFields.GetItemByName(fieldName);
        }
    }
}