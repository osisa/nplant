// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleEnumDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Samples.Enums
{
    [Sample]
    public class SimpleEnumDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleEnumDiagram()
        {
            AddClass<Foo>();
            AddEnum<RandomEnum>();
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

        public BarTypes Type;

        #endregion
    }

    public enum RandomEnum
    {
        Member1,

        Member2,

        Member3
    }

    public enum BarTypes
    {
        HighBar,

        LowBar
    }

    public class Baz
    {
        #region Fields

        public Foo TheFoo;

        #endregion
    }
}