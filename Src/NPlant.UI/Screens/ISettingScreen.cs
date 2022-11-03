// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ISettingScreen.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.UI.Screens
{
    public interface ISettingScreen : IScreen
    {
        #region Public Properties

        string JavaPath { get; set; }

        #endregion
    }
}