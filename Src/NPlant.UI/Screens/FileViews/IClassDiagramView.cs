// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IClassDiagramView.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.UI.Screens.FileViews
{
    public interface IClassDiagramView : IFileView
    {
        #region Public Properties

        LoadedDiagram[] Diagrams { get; set; }

        string DiagramText { get; set; }

        LoadedDiagram SelectedDiagram { get; }

        bool ShowDiagramClassesPanel { get; set; }

        #endregion
    }
}