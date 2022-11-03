// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleLegendDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.Samples.Legends
{
    public class SimpleLegendDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleLegendDiagram()
        {
            AddClass<Foo>();
            AddClass<Bar>();
            LegendOf("This is my legend")
                .DisplayLeft();
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