// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDiagramNote.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace NPlant
{
    public class ClassDiagramNote
    {
        #region Fields

        private readonly List<Type> _connectedTypes = new();

        private readonly List<string> _lines = new();

        #endregion

        #region Constructors and Destructors

        public ClassDiagramNote(string line, ClassDiagram diagram)
        {
            Diagram = diagram;
            _lines.Add(line);
        }

        #endregion

        #region Public Properties

        public ClassDiagram Diagram { get; }

        #endregion

        #region Properties

        internal IEnumerable<Type> ConnectedClasses => _connectedTypes;

        internal IEnumerable<string> Lines => _lines;

        #endregion

        #region Public Methods and Operators

        public ClassDiagramNote AddLine(string line)
        {
            _lines.Add(line);
            return this;
        }

        public ClassDiagramNote ConnectedToClass<T>()
        {
            if (!_connectedTypes.Contains(typeof(T)))
                _connectedTypes.Add(typeof(T));

            return this;
        }

        #endregion
    }
}