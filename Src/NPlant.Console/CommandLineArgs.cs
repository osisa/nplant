// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="CommandLineArgs.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Drawing.Imaging;

namespace NPlant.Console
{
    public class CommandLineArgs
    {
        #region Fields

        private string _format;

        private ImageFormat _imageFormat;

        #endregion

        #region Constructors and Destructors

        public CommandLineArgs(string[] args)
        {
            this.Format = "png";

            CommandLineMapper.Map(this, args);
        }

        #endregion

        #region Public Properties

        [RequiredArgument] public string Assembly { get; protected set; }

        public bool Debugger { get; protected set; }

        public string Diagram { get; protected set; }

        public string Format
        {
            get => _format;
            protected set
            {
                _format = value;
                _imageFormat = null;

                if (!string.Equals("nplant", _format))
                {
                    var converter = TypeDescriptor.GetConverter(typeof(ImageFormat));
                    _imageFormat = (ImageFormat)converter.ConvertFromInvariantString(_format);
                }
            }
        }

        public string Jar { get; protected set; }

        public string Java { get; protected set; }

        public string Output { get; protected set; }

        #endregion

        #region Public Methods and Operators

        public ImageFormat GetImageFormat()
        {
            return _imageFormat;
        }

        #endregion
    }
}