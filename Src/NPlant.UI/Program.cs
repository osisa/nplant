// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="Program.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

using NPlant.Core;

namespace NPlant.UI
{
    public static class Program
    {
        #region Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (sender, args) =>
            {
                if (args.Exception != null)
                {
                    var @event = new UserNotificationEvent(args.Exception.Message, UserNotificationType.Error);
                    EventDispatcher.Raise(@event);
                }
            };

            UnpackPlantUML();

            Application.Run(new MainScreen());
        }

        private static void UnpackPlantUML()
        {
            try
            {
                PlantUmlJarExtractor.TryExtractTo(SystemEnvironment.ExecutionDirectory);
            }
            catch (Exception exception)
            {
                EventDispatcher.Raise(new UserNotificationEvent("Error occurred while trying to unpack the plantuml.jar file", exception));
            }
        }

        #endregion
    }
}