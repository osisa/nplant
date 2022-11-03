// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SystemEnvironment.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Windows.Forms;

namespace NPlant.UI
{
    public static class SystemEnvironment
    {
        #region Public Properties

        public static string ExecutionDirectory 
            => Path.GetDirectoryName(Application.ExecutablePath);

        #endregion

        #region Public Methods and Operators

        public static SystemSettings GetSettings()
        {
            string javaHome = Environment.GetEnvironmentVariable("NPLANT_JAVA_HOME", EnvironmentVariableTarget.User);

            if (javaHome.IsNullOrEmpty())
                javaHome = Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.User);

            return new SystemSettings()
                   {
                       JavaPath = javaHome
                   };
        }

        public static void SetSettings(SystemSettings settings)
        {
            Environment.SetEnvironmentVariable("NPLANT_JAVA_HOME", settings.JavaPath, EnvironmentVariableTarget.User);
        }

        #endregion
    }

    public class SystemSettings
    {
        #region Public Properties

        public string JavaPath { get; set; }

        #endregion
    }
}