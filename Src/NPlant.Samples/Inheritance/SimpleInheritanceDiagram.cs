// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleInheritanceDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Samples.Inheritance
{
    public class SimpleInheritanceDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleInheritanceDiagram()
        {
            GenerationOptions.ForType<AbstractBase>().HideAsBase();
            AddClass<Foo>();
        }

        #endregion
    }

    public abstract class AbstractBase
    {
        #region Fields

        public string SomeString;

        #endregion
    }

    public class Foo : AbstractBase
    {
        #region Fields

        public Bar TheBar;

        public Baz TheBaz;

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