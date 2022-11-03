// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleTitledClassDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.Samples.Titles
{
    public class SimpleTitledClassDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleTitledClassDiagram()
        {
            Titled("This is a big fat title using <i><b>html!</b></i>");
        }

        #endregion
    }

    public class Foo
    {
        #region Fields

        public string SomeField;

        #endregion
    }
}