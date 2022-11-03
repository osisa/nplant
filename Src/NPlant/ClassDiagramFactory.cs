// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDiagramFactory.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace NPlant
{
    internal abstract class DiagramFactory<T> : IDiagramFactory
        where T : ClassDiagram
    {
        #region Public Methods and Operators

        public abstract IEnumerable<ClassDiagram> GetDiagrams();

        #endregion
    }
}