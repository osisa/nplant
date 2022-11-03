// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="MainScreenControllerFixture.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.UI;
using NPlant.UI.Screens;

using NUnit.Framework;

namespace NPlant.Tests.UI
{
    [TestFixture]
    public class MainScreenControllerFixture
    {
        #region Public Methods and Operators

        [Test]
        public void Launching_The_App_With_A_File_Path_Should_Append_The_Path_To_The_Window_Title()
        {
            var screen = new StubMainScreen();
            var controller = new MainScreenController(screen, new StubCommandLineArguments("Foo.nplant"));
            controller.Start();

            Assert.That(screen.Title, Is.EqualTo("NPlant UI - Foo.nplant"));
        }

        [Test]
        public void Launching_The_App_With_No_File_Path_Should_Not_Append_The_Path_To_The_Window_Title()
        {
            var screen = new StubMainScreen();
            var controller = new MainScreenController(screen, new StubCommandLineArguments());
            controller.Start();

            Assert.That(screen.Title, Is.EqualTo("NPlant UI"));
        }

        #endregion
    }

    public class StubCommandLineArguments : CommandLineArguments
    {
        #region Constructors and Destructors

        public StubCommandLineArguments(string filePath)
            : base(new[] { filePath })
        {
        }

        public StubCommandLineArguments()
            : base(new string[] { })
        {
        }

        #endregion
    }

    public class StubMainScreen : IMainScreen
    {
        #region Public Properties

        public string Title { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AddFileView(FileViewType type, string filePath)
        {
        }

        public void DisplayUserNotification(UserNotificationEvent @event)
        {
        }

        #endregion
    }
}