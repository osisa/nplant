// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="AggregationScenarios.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NPlant.MetaModel.ClassDiagramming;

using NUnit.Framework;

namespace NPlant.Tests.ClassDiagrams
{
    [TestFixture]
    public class AggregationScenarios
    {
        #region Interfaces

        public interface IBar
        {
        }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void GIVEN_A_Simple_Single_Entity_Diagram_WHEN_Rendered_With_Defaults_THEN_Diagram_Has_One_Class_With_Members()
        {
            var simulation = new SimulatedClassDiagramGenerator(new ClassDiagram(typeof(AggregationEntity)));
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(5));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("AggregationEntity"));
        }

        #endregion

        public class AggregationEntity
        {
            #region Fields

            public BahList TheBahs;

            public Bah[] TheBahsArray;

            public Bar TheBar;

            public Foo TheFoo;

            #endregion
        }

        public class Bah
        {
            #region Fields

            public DateTime? SomeNullableDateTime;

            public string SomeProperty;

            #endregion
        }

        public class BahList : List<Bah>
        {
        }

        public class Bar
        {
        }

        public class Baz
        {
            #region Fields

            public Foo TheFoo;

            #endregion
        }

        public class Foo
        {
            #region Fields

            public IBar TheBar;

            #endregion
        }
    }
}