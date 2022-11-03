// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleHidingClassDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.Samples.Hiding
{
    public class SimpleHidingClassDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleHidingClassDiagram()
        {
            AddClass<SimpleEntity>().ForMember(x => x.Bar).Hide();
            AddClass<SimpleEntity2>();
            GenerationOptions.ForType<BahEntity>().Hide();

            AddNote("Bar is not displayed as a member of SimpleEntity.  Bah is no where.");
        }

        #endregion

        public class BahEntity
        {
            #region Fields

            public string Something;

            #endregion
        }

        public class SimpleEntity
        {
            #region Fields

            public BahEntity Bah;

            public string Bar;

            public string Baz;

            public string Foo;

            #endregion
        }

        public class SimpleEntity2
        {
            #region Fields

            public BahEntity Bah;

            public string Foo;

            #endregion
        }
    }
}