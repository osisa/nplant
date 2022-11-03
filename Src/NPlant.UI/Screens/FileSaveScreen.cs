// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="FileSaveScreen.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public class FileSaveScreen : IResultScreen<FileDialogResult>
    {
        #region Fields

        private FileDialogResult _result;

        #endregion

        #region Public Methods and Operators

        public FileDialogResult GetResult()
        {
            return _result;
        }

        public DialogResult ShowDialog(IWin32Window owner)
        {
            var dialog = new SaveFileDialog { AddExtension = true, OverwritePrompt = true, RestoreDirectory = true };

            var result = dialog.ShowDialog(owner);

            _result = new FileDialogResult(result, dialog.FileName);

            return result;
        }

        #endregion
    }
}