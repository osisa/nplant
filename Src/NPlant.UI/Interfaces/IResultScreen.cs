// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IResultScreen.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.UI
{
    public interface IResultScreen<T> : IScreen
    {
        #region Public Methods and Operators

        T GetResult();

        #endregion
    }
}