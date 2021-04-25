using System;
using System.Management;
using System.Text.RegularExpressions;

namespace TPlayer
{
    public class PlatformHelper
    {
        /// <summary>
        /// Initializes the <see cref="PlatformHelper"/> class.
        /// </summary>
        public PlatformHelper()
        {
            OperatingSystem os = Environment.OSVersion;
            Win32NT = os.Platform == PlatformID.Win32NT;
            XpOrHigher = Win32NT && os.Version.Major >= 5;
            VistaOrHigher = Win32NT && os.Version.Major >= 6;
            SevenOrHigher = Win32NT && (os.Version >= new Version(6, 1));
            EightOrHigher = Win32NT && (os.Version >= new Version(6, 2, 9200));
            EightPointOneOrHigher = Win32NT && (os.Version >= new Version(6, 3));
            TenOrHigher = Win32NT && (os.Version >= new Version(10, 0));
            RunningOnMono = Type.GetType("Mono.Runtime") != null;
            Build = os.Version.Build;
            Name = "Unknown OS";
            using (
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem"))
            {
                foreach (ManagementObject oss in searcher.Get())
                {
                    Name = oss["Caption"].ToString();
                    break;
                }
            }

            Name = Regex.Replace(Name, "^.*(?=Windows)", "").TrimEnd().TrimStart(); // Remove everything before first match "Windows" and trim end & start
            Is64Bit = Environment.Is64BitOperatingSystem;
            FullName = string.Format("{0} {1} 位", Name, Is64Bit ? 64 : 32);
        }
        public int Build { get; private set; }
        /// <summary>
        /// Gets the full name of the operating system running on this computer (including the edition and architecture).
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Gets the name of the operating system running on this computer (including the edition).
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Determines whether the Operating System is 32 or 64-bit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is 64-bit, otherwise <c>false</c> for 32-bit.
        /// </value>
        public bool Is64Bit { get; private set; }

        /// <summary>
        /// Returns a indicating whether the application is running in Mono runtime.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the application is running in Mono runtime; otherwise, <c>false</c>.
        /// </value>
        public bool RunningOnMono { get; private set; }

        /// <summary>
        /// Returns a indicating whether the Operating System is Windows 32 NT based.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 32 NT based; otherwise, <c>false</c>.
        /// </value>
        public bool Win32NT { get; private set; }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows XP or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows XP or higher; otherwise, <c>false</c>.
        /// </value>
        public bool XpOrHigher { get; private set; }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows Vista or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows Vista or higher; otherwise, <c>false</c>.
        /// </value>
        public bool VistaOrHigher { get; private set; }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows 7 or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 7 or higher; otherwise, <c>false</c>.
        /// </value>
        public bool SevenOrHigher { get; private set; }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows 8 or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 8 or higher; otherwise, <c>false</c>.
        /// </value>
        public bool EightOrHigher { get; private set; }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows 8.1 or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 8.1 or higher; otherwise, <c>false</c>.
        /// </value>
        public bool EightPointOneOrHigher { get; private set; }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows 10 or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 10 or higher; otherwise, <c>false</c>.
        /// </value>
        public bool TenOrHigher { get; private set; }
    }
}
