// Licensed via The Unlicense by Peter Hyde. See https://unlicense.org

namespace ApiBenchmarks.CliClient
{
    using System.Runtime.InteropServices;
    using System.Security.Principal;

    /// <summary>
    /// Windows user rights checker.
    /// </summary>
    internal class WindowsRightsChecker
    {
        /// <summary>
        /// Checks for elevated user permissions.
        /// </summary>
        /// <returns>True if elevated, otherwise false.</returns>
        public bool IsElevated()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }
    }
}
