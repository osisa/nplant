// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="DeepOneEntityClassDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Samples.FullMonty
{
    public class DeepOneEntityClassDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public DeepOneEntityClassDiagram()
        {
            this.GenerationOptions.ShowMethods();
            AddClass<Foo>();
        }

        #endregion
    }

    public class Foo
    {
        #region Fields

        public string SomeString;

        public Bar TheBar;

        public Baz<Arg1, Arg2> TheBaz;

        public Baz2<Arg1, Arg2> TheBaz2;

        #endregion

        #region Public Methods and Operators

        public void DoSomethingOnFoo()
        {
        }

        public void DoSomethingOnFoo(string parm1)
        {
        }

        public void DoSomethingOnFoo(string parm1, DateTime? parm2, Bar parm3)
        {
        }

        #endregion
    }

    public class Bar
    {
        #region Fields

        public DateTime? SomeDate;

        #endregion

        #region Public Methods and Operators

        public void DoSomethingOnBar()
        {
        }

        public void DoSomethingOnBar(string parm1)
        {
        }

        public void DoSomethingOnBar(string parm1, DateTime? parm2, Baz<Arg1, Arg2> parm3)
        {
        }

        #endregion
    }

    public class Baz<T1, T2>
    {
        #region Fields

        public T1 Arg1;

        public T2 Arg2;

        public Foo TheFoo;

        #endregion
    }

    public class Baz2<T1, T2>
    {
        #region Fields

        public string Whatever;

        #endregion

        #region Constructors and Destructors

        public Baz2()
        {
            this.Whatever = typeof(T1).Name + typeof(T2).Name;
        }

        #endregion
    }

    public class Arg1
    {
    }

    public class Arg2
    {
    }
}