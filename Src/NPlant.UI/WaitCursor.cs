// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="WaitCursor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace NPlant.UI
{
    public class WaitCursor : IDisposable
    {
        #region Fields

        private readonly Cursor _old;

        #endregion

        #region Constructors and Destructors

        public WaitCursor()
        {
            _old = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            Cursor.Current = _old;
        }

        #endregion
    }
}