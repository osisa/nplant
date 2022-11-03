// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleNotesDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.Samples.Notes
{
    public class SimpleNotesDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleNotesDiagram()
        {
            this.AddClass<Foo>();
            this.AddClass<Bar>();
            this.AddNote("this is a note");
            this.AddNote("this is another note")
                .AddLine("with another line");
            this.AddNote("this is connected note")
                .AddLine("with another line")
                .ConnectedToClass<Foo>()
                .ConnectedToClass<Bar>();
        }

        #endregion
    }

    public class Foo
    {
    }

    public class Bar
    {
    }
}