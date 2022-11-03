// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ConsoleEnvironment.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Console
{
    public static class ConsoleEnvironment
    {
        #region Static Fields

        public static readonly string ExecutionDirectory = Environment.CurrentDirectory;

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
}