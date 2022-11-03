// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IBuilder.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Generation.ClassDiagramming;

namespace NPlant.Generation
{
    public interface IBuilder
    {
        #region Public Methods and Operators

        void Build(ClassDiagramVisitorContext context);

        #endregion
    }
}