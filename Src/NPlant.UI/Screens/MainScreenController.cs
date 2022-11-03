// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="MainScreenController.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using NPlant.UI.Screens;

namespace NPlant.UI
{
    public class MainScreenController
    {
        #region Fields

        private readonly CommandLineArguments _args;

        private readonly IMainScreen _screen;

        #endregion

        #region Constructors and Destructors

        public MainScreenController(IMainScreen screen, CommandLineArguments args)
        {
            _screen = screen;
            _args = args;
        }

        #endregion

        #region Public Methods and Operators

        public void OpenFile(FileDialogResult result)
        {
            if (result.UserApproved)
            {
                this.OpenFile(result.FilePath);
            }
        }

        public void Start()
        {
            if (_args.HasFilePath)
                OpenFile(_args.FilePath);
            else
                _screen.Title = "NPlant UI";

            EventDispatcher.Register<UserNotificationEvent>(LogUserNotificationToConsole);
        }

        public void Stop(Action action)
        {
            action();
        }

        #endregion

        #region Methods

        private void LogUserNotificationToConsole(UserNotificationEvent @event)
        {
            if (@event != null)
                _screen.DisplayUserNotification(@event);
        }

        private void OpenFile(string filePath)
        {
            _screen.Title = "NPlant UI - {0}".FormatWith(filePath);

            if (filePath.IsNPlantFilePath())
                _screen.AddFileView(FileViewType.NPlantFile, filePath);
            else if (filePath.IsAssemblyFilePath())
                _screen.AddFileView(FileViewType.AssemblyFile, filePath);
        }

        #endregion
    }
}