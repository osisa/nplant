// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="LoadedDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.UI
{
    public class LoadedDiagram
    {
        #region Fields

        private readonly string _name;

        #endregion

        #region Constructors and Destructors

        public LoadedDiagram(ClassDiagram diagram)
        {
            Diagram = diagram;
            _name = Diagram.Name;
        }

        #endregion

        #region Public Properties

        public ClassDiagram Diagram { get; }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
            => _name;

        #endregion
    }
}