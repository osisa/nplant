// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ImageFileGenerationModel.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Generation;

namespace NPlant.Console
{
    public class ImageFileGenerationModel
    {
        #region Constructors and Destructors

        public ImageFileGenerationModel(string diagramText, string diagramName, string javaPath, string jarPath)
        {
            if (javaPath.IsNullOrEmpty())
                javaPath = ConsoleEnvironment.GetSettings().JavaPath;

            this.JavaPath = javaPath;
            this.DiagramText = diagramText;
            this.DiagramName = diagramName;

            this.Invocation = new PlantUmlInvocation(jarPath);
        }

        #endregion

        #region Public Properties

        public string DiagramName { get; private set; }

        public string DiagramText { get; private set; }

        public PlantUmlInvocation Invocation { get; set; }

        public string JavaPath { get; private set; }

        #endregion
    }
}