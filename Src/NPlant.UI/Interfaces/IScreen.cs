// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IScreen.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Windows.Forms;

namespace NPlant.UI
{
    public interface IScreen
    {
        #region Public Methods and Operators

        DialogResult ShowDialog(IWin32Window owner);

        #endregion
    }
}