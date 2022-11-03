// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="StubClassDiagramVisitorContext.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Core;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.Tests.Diagraming
{
    public class StubClassDiagramVisitorContext : ClassDiagramVisitorContext
    {
        #region Constructors and Destructors

        public StubClassDiagramVisitorContext(ClassDiagramScanModes scanMode)
        {
            TypeMetaModelSet = new TypeMetaModelSet();
            ScanMode = scanMode;
            ShowMembers = true;
            ShowMembersBindingFlags = ClassDiagramOptions.ShowMembersBindingFlagsDefault;
            ShowMethods = false;
            ShowMethodsBindingFlags = ClassDiagramOptions.ShowMethodsBindingFlagsDefault;
        }

        #endregion
    }
}