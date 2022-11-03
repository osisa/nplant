// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IDescriptorWriter.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public interface IDescriptorWriter
    {
        #region Public Methods and Operators

        string Write(ClassDiagramVisitorContext context);

        #endregion
    }
}