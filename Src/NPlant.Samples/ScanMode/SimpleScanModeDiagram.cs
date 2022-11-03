// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleScanModeDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using NPlant.Generation.ClassDiagramming;

namespace NPlant.Samples.ScanMode
{
    public class SimpleScanModeDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleScanModeDiagram()
        {
            base.GenerationOptions.ScanModeOf(ClassDiagramScanModes.AllMembers);
            AddClass<Foo>();
        }

        #endregion
    }

    public class Foo
    {
        #region Fields

        public short IAmPublic;

        public Bar TheBar;

        public Baz TheBaz;

        internal bool IAmInternal;

        protected string IAmProtected;

        private int IAmPrivate;

        #endregion
    }

    public class Bar : Foo
    {
        #region Fields

        public DateTime? SomeDate;

        #endregion
    }

    public class Baz
    {
        #region Fields

        public Foo TheFoo;

        #endregion
    }
}