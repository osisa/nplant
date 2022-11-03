// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleDiagramFactory.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace NPlant.Samples.Factories
{
    [Sample]
    public class SimpleDiagramFactory : IDiagramFactory
    {
        #region Public Methods and Operators

        public IEnumerable<ClassDiagram> GetDiagrams()
        {
            return new[]
                   {
                       new ClassDiagram(typeof(Foo)).Named("FactoriedFoo1"),
                       new ClassDiagram(typeof(Foo)).Named("FactoriedFoo2"),
                       new ClassDiagram(typeof(Foo)).Named("FactoriedFoo3")
                   };
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