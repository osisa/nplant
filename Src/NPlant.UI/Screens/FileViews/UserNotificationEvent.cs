// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="UserNotificationEvent.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.UI
{
    public class UserNotificationEvent
    {
        #region Constructors and Destructors

        public UserNotificationEvent(string message, UserNotificationType type = UserNotificationType.Info)
        {
            this.NotificationType = type;
            this.Message = message;
        }

        public UserNotificationEvent(string message, Exception exception)
        {
            this.Message = "{0} - {1}".FormatWith(message, exception.Message);
            this.NotificationType = UserNotificationType.Error;
        }

        #endregion

        #region Public Properties

        public string Message { get; set; }

        public UserNotificationType NotificationType { get; private set; }

        #endregion
    }

    public enum UserNotificationType
    {
        Info,

        Warning,

        Error
    }
}