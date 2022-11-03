// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IMainScreen.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.UI
{
    public interface IMainScreen
    {
        #region Public Properties

        string Title { get; set; }

        #endregion

        #region Public Methods and Operators

        void AddFileView(FileViewType type, string filePath);

        void DisplayUserNotification(UserNotificationEvent @event);

        #endregion
    }
}