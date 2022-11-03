// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IDiagramFactory.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace NPlant
{
    public interface IDiagramFactory
    {
        #region Public Methods and Operators

        IEnumerable<ClassDiagram> GetDiagrams();

        #endregion
    }
}