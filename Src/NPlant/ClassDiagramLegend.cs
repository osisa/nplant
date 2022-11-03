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

        #endregion

        #region Constructors and Destructors

        public ClassDiagramLegend(ClassDiagram diagram, string text)
        {
            Text = text;
            _diagram = diagram;
        }

        #endregion

        #region Properties

        internal string Position { get; private set; } = "center";

        internal string Text { get; }

        #endregion

        #region Public Methods and Operators

        public ClassDiagram DisplayCenter()
        {
            Position = "center";
            return _diagram;
        }

        public ClassDiagram DisplayLeft()
        {
            Position = "left";
            return _diagram;
        }

        public ClassDiagram DisplayRight()
        {
            Position = "right";
            return _diagram;
        }

        #endregion
    }
}