using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SourceCode.SmartObjects.Services.Management;

namespace SourceCode.SmartObjects.Services.Tests.Managers
{
    [ExcludeFromCodeCoverage]
    public abstract class ServiceInstanceSettings
    {
        public abstract IDictionary<string, string> ConfigurationSettings
        {
            get;
        }

        public virtual string Description
        {
            get;
        }

        public abstract string DisplayName
        {
            get;
        }

        public abstract Guid Guid
        {
            get;
        }

        public abstract string Name
        {
            get;
        }

        public abstract ServiceAuthenticationInfo ServiceAuthentication
        {
            get;
        }
    }
}