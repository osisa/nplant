// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="NPlantAssemblyLoader.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;

using NPlant.Core;

namespace NPlant.Generation
{
    public class NPlantAssemblyLoader
    {
        #region Fields

        private readonly IRunnerRecorder _recorder = NullRecorder.Instance;

        #endregion

        #region Constructors and Destructors

        public NPlantAssemblyLoader()
        {
        }

        public NPlantAssemblyLoader(IRunnerRecorder recorder)
        {
            _recorder = recorder;
        }

        #endregion

        #region Public Methods and Operators

        public Assembly Load(string path)
        {
            _recorder.Log("Starting Stage: Assembly Load (assembly={0})...".FormatWith(path));

            path.CheckForNull(() => new NPlantException("An 'assembly' attribute is required."));

            string loadMessage;

            Assembly assembly = LoadAssembly(path, out loadMessage);

            assembly.CheckForNull(
                () =>
                    new NPlantException(
                        "Failed to load assembly '{0}'.  Exception message detected:  {1}".FormatWith(path, loadMessage)));

            _recorder.Log("Finished Stage: Assembly Load...");

            return assembly;
        }

        #endregion

        #region Methods

        private Assembly LoadAssembly(string path, out string message)
        {
            Assembly assembly;

            try
            {
                if (Path.HasExtension(path))
                {
                    if (!File.Exists(path))
                        throw new FileNotFoundException("Attempting to load diagrams from an assembly via a file path.  The file path provided ('{0}') could not be found.".FormatWith(path));

                    assembly = Assembly.LoadFrom(path);
                }
                else
                    assembly = Assembly.Load(path);
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return null;
            }

            message = null;

            return assembly;
        }

        #endregion
    }
}