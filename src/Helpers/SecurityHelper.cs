using System;
using System.Linq;
using System.Security.Principal;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.Helpers
{
    /// <summary>
    /// Helper class for security specific code.
    /// </summary>
    public static class SecurityHelper
    {
        public static void InvokeAsUser(Action action, string userName, string password)
        {
            action.ThrowIfNull("action");

            userName.ThrowIfNullOrWhiteSpace("userName");

            // Strip-off Domain Name
            userName = userName.Split('\\').Last();

            var currentUserName = WindowsIdentity.GetCurrent().Name;
            var userDomainName = string.Format("{0}\\{1}", Environment.UserDomainName, userName);

            if (userDomainName.Equals(currentUserName, StringComparison.InvariantCultureIgnoreCase))
            {
                action();
            }
            else
            {
                var securityWrapper = WrapperFactory.Instance.GetSecurityWrapper();
                securityWrapper.InvokeAsUser(action, userName, password);
            }
        }
    }
}