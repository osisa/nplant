// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="HasManyScenarios.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using NPlant.MetaModel.ClassDiagramming;

using NUnit.Framework;

namespace NPlant.Tests.ClassDiagrams
{
    [TestFixture]
    public class HasManyScenarios
    {
        #region Public Methods and Operators

        [Test]
        public void TestPersonHasHands()
        {
            var simulation = new SimulatedClassDiagramGenerator(new SimpleHasManyDiagram());
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(2));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("Person"));
            Assert.That(simulation.Classes[1].Name, Is.EqualTo("Hand"));
        }

        #endregion

        public class Hand
        {
        }

        public class Person
        {
            #region Public Properties

            public IList<Hand> Hands { get; set; }

            #endregion
        }

        public class SimpleHasManyDiagram : ClassDiagram
        {
            #region Constructors and Destructors

            public SimpleHasManyDiagram()
            {
                this.AddClass<Person>();
            }

            #endregion
        }
    }
}