// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="FileViewController.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Generation;

namespace NPlant.UI.Screens.FileViews
{
    public abstract class FileViewController
    {
        #region Fields

        private readonly IFileView _view;

        #endregion

        #region Constructors and Destructors

        protected FileViewController(IFileView view)
        {
            _view = view;
        }

        #endregion

        #region Public Methods and Operators

        public void Generate()
        {
            var model = ImageFileGenerationModel.Create(GetDiagramText(), GetDiagramName());

            var npImage = new NPlantImage(model.JavaPath, model.Invocation)
                          {
                              Logger = msg => EventDispatcher.Raise(new UserNotificationEvent(msg))
                          };

            _view.Image = npImage.Create(model.DiagramText, model.DiagramName);
        }

        #endregion

        #region Methods

        protected abstract string GetDiagramName();

        protected abstract string GetDiagramText();

        #endregion
    }
}