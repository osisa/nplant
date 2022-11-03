// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="CommandLineArguments.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Linq;

using NPlant.Core;

namespace NPlant.UI
{
    public class CommandLineArguments
    {
        #region Constructors and Destructors

        protected CommandLineArguments(string[] args)
        {
            if (args == null || args.Length < 1)
                return;

            var path = args[0].CheckForNull();

            ValidateFilePath(path);

            this.FilePath = path;
        }

        #endregion

        #region Public Properties

        public string FilePath { get; protected set; }

        public bool HasFilePath => !this.FilePath.IsNullOrEmpty();

        #endregion

        #region Public Methods and Operators

        public static CommandLineArguments Load(string[] args, bool filterVSExecutable = false)
        {
            if (filterVSExecutable)
                args = Filter(args);

            return new CommandLineArguments(args);
        }

        #endregion

        #region Methods

        private static string[] Filter(string[] args)
        {
            if (args == null || args.Length < 1)
                return args;

            return args.Where(x => !x.IsVSHostExe() && !x.IsSelf()).ToArray();
        }

        private static void ValidateFilePath(string path)
        {
            if (path.IsAssemblyFilePath())
                return;

            if (path.IsNPlantFilePath())
                return;

            var extension = Path.GetExtension(path);

            throw new NPlantException("'{0}' is not a recognized/supported file extension.  This version of NPlant.UI.exe supports .nplant text files or .NET assembly files (.dll/.exe)".FormatWith(extension));
        }

        #endregion
    }
}