// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ImageFileGenerationModel.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Generation;

namespace NPlant.UI.Screens.FileViews
{
    public class ImageFileGenerationModel
    {
        #region Constructors and Destructors

        private ImageFileGenerationModel(string diagramText, string diagramName)
        {
            var settings = SystemEnvironment.GetSettings();
            JavaPath = settings.JavaPath;

            DiagramText = diagramText;
            DiagramName = diagramName;
        }

        #endregion

        #region Public Properties

        public string DiagramName { get; private set; }

        public string DiagramText { get; private set; }

        public PlantUmlInvocation Invocation { get; } = new(SystemEnvironment.ExecutionDirectory);

        public string JavaPath { get; private set; }

        #endregion

        #region Public Methods and Operators

        public static ImageFileGenerationModel Create(string diagramText, string diagramName)
            => new(diagramText, diagramName);

        #endregion
    }
}