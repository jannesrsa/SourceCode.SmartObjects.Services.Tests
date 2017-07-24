using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace SourceCode.SmartObjects.Services.Tests.Wrappers
{
    [ExcludeFromCodeCoverage]
    internal class SecurityWrapper
    {
        internal virtual void InvokeAsUser(Action action, string userName, string password)
        {
            const int LOGON32_PROVIDER_DEFAULT = 0;

            //This parameter causes LogonUser to create a primary token.
            const int LOGON32_LOGON_INTERACTIVE = 2;

            // Call LogonUser to obtain a handle to an access token.
            bool returnValue = NativeMethods.LogonUser(userName, Environment.UserDomainName, password,
                LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                out SafeTokenHandle safeTokenHandle);

            if (false == returnValue)
            {
                int ret = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(ret);
            }

            // Use the token handle returned by LogonUser.
            using (safeTokenHandle)
            using (WindowsIdentity newId = new WindowsIdentity(safeTokenHandle.DangerousGetHandle()))
            using (WindowsImpersonationContext impersonatedUser = newId.Impersonate())
            {
                Debug.WriteLine("User: " + WindowsIdentity.GetCurrent().Name);

                action();
            }
        }

        internal static class NativeMethods
        {
            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, out SafeTokenHandle phToken);
        }

        internal sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeTokenHandle()
                : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                return NativeMethods.CloseHandle(handle);
            }

            internal static class NativeMethods
            {
                [DllImport("kernel32.dll")]
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
                [SuppressUnmanagedCodeSecurity]
                [return: MarshalAs(UnmanagedType.Bool)]
                internal static extern bool CloseHandle(IntPtr handle);
            }
        }
    }
}