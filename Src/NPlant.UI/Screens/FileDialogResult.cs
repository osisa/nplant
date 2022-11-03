// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="FileDialogResult.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Windows.Forms;

namespace NPlant.UI.Screens
{
    public class FileDialogResult
    {
        #region Constructors and Destructors

        public FileDialogResult(DialogResult result, string fileName)
        {
            this.UserApproved = result == DialogResult.OK;
            this.FilePath = fileName;

            if (this.UserApproved && !this.FilePath.IsNullOrEmpty())
            {
                this.FileName = Path.GetFileName(this.FilePath);
                this.DirectoryName = Path.GetDirectoryName(this.FilePath);
            }
        }

        #endregion

        #region Public Properties

        public string DirectoryName { get; private set; }

        public string FileName { get; private set; }

        public string FilePath { get; private set; }

        public bool UserApproved { get; private set; }

        #endregion
    }
}