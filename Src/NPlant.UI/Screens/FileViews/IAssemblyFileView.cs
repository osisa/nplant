// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IAssemblyFileView.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.UI.Screens.FileViews
{
    public interface IAssemblyFileView : IFileView
    {
        #region Public Properties

        LoadedDiagram[] Diagrams { get; set; }

        LoadedDiagram SelectedDiagram { get; }

        #endregion
    }
}