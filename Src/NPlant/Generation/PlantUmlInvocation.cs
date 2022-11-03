// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="PlantUmlInvocation.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace NPlant.Generation
{
    public class PlantUmlInvocation
    {
        #region Constants

        private const string PlantUmlJarName = "plantuml.jar";

        #endregion

        #region Fields

        private readonly string _jarPath;

        #endregion

        #region Constructors and Destructors

        public PlantUmlInvocation(string jarPath)
        {
            _jarPath = jarPath.CheckForNullOrEmptyArg("jarPath");

            if (!_jarPath.EndsWith(PlantUmlJarName))
                _jarPath = Path.Combine(_jarPath, PlantUmlJarName);
        }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return "-splash:no -jar \"{0}\" -pipe".FormatWith(_jarPath);
        }

        #endregion
    }
}