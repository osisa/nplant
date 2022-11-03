// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IFileView.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Drawing;

namespace NPlant.UI.Screens.FileViews
{
    public interface IFileView
    {
        #region Public Properties

        Image Image { get; set; }

        #endregion
    }
}