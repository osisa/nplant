// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SettingsScreenController.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.UI.Screens
{
    public class SettingsScreenController
    {
        #region Fields

        private readonly ISettingScreen _screen;

        #endregion

        #region Constructors and Destructors

        public SettingsScreenController(ISettingScreen screen)
        {
            _screen = screen;
        }

        #endregion

        #region Public Methods and Operators

        public void SaveChanges()
        {
            SystemEnvironment.SetSettings(new SystemSettings { JavaPath = _screen.JavaPath });
        }

        public void Start()
        {
            var settings = SystemEnvironment.GetSettings();
            _screen.JavaPath = settings.JavaPath;
        }

        #endregion
    }
}