// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDiagramLegend.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant
{
    public class ClassDiagramLegend
    {
        #region Fields

        private readonly ClassDiagram _diagram;

        private readonly string _text;

        private string _position = "center";

        #endregion

        #region Constructors and Destructors

        public ClassDiagramLegend(ClassDiagram diagram, string text)
        {
            _text = text;
            _diagram = diagram;
        }

        #endregion

        #region Properties

        internal string Position
        {
            get
            {
                return _position;
            }
        }

        internal string Text
        {
            get
            {
                return _text;
            }
        }

        #endregion

        #region Public Methods and Operators

        public ClassDiagram DisplayCenter()
        {
            _position = "center";
            return _diagram;
        }

        public ClassDiagram DisplayLeft()
        {
            _position = "left";
            return _diagram;
        }

        public ClassDiagram DisplayRight()
        {
            _position = "right";
            return _diagram;
        }

        #endregion
    }
}