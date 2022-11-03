// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="PlantUmlJarExtractor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Reflection;

namespace NPlant.Core
{
    public static class PlantUmlJarExtractor
    {
        #region Public Methods and Operators

        public static string TryExtractTo(string dir)
        {
            string jarPath = Path.Combine(dir, "plantuml.jar");

            if (!File.Exists(jarPath))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                const string embeddedResourceFullName = "NPlant.plantuml.jar";

                using (Stream input = assembly.GetManifestResourceStream(embeddedResourceFullName))
                {
                    if (input == null) throw new NPlantException(string.Format("Failed to find embedded resource '{0}' in assembly '{1}'", embeddedResourceFullName, assembly.FullName));

                    using (Stream output = File.Create(jarPath))
                    {
                        CopyStream(input, output);
                    }
                }
            }

            return jarPath;
        }

        #endregion

        #region Methods

        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        #endregion
    }
}