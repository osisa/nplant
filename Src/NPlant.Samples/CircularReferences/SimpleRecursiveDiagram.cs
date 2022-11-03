// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleRecursiveDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Samples.CircularReferences
{
    [Sample]
    public class SimpleRecursiveDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleRecursiveDiagram()
        {
            AddClass<Foo>();
        }

        #endregion
    }

    public class Foo
    {
        #region Fields

        public string SomeString;

        public Bar TheBar;

        public Baz TheBaz;

        #endregion
    }

    public class Bar
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