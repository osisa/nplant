// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="FileOpenScreen.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public class FileOpenScreen : IResultScreen<FileDialogResult>
    {
        #region Fields

        private readonly string _filter;

        private FileDialogResult _result;

        #endregion

        #region Constructors and Destructors

        public FileOpenScreen(string filter)
        {
            _filter = filter;
        }

        #endregion

        #region Public Methods and Operators

        public FileDialogResult GetResult()
        {
            return _result;
        }

        public DialogResult ShowDialog(IWin32Window owner)
        {
            var dialog = new OpenFileDialog
                         {
                             RestoreDirectory = true,
                             Filter = _filter
                         };

            var result = dialog.ShowDialog(owner);

            _result = new FileDialogResult(result, dialog.FileName);

            return result;
        }

        #endregion
    }
}