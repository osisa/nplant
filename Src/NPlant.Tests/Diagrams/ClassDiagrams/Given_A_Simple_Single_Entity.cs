// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="Given_A_Simple_Single_Entity.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.MetaModel.ClassDiagramming;

using NUnit.Framework;

namespace NPlant.Tests.Diagrams.ClassDiagrams
{
    [TestFixture]
    public class Given_A_Simple_Single_Entity
    {
        #region Public Methods and Operators

        [Test]
        public void WHEN_Rendered_With_Defaults_THEN_Diagram_Has_One_Class_With_Members()
        {
            var simulation = new SimulatedClassDiagramGenerator(new ClassDiagram(typeof(SimpleEntity)));
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(1));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("SimpleEntity"));
            Assert.That(simulation.Classes[0].Members.Count, Is.EqualTo(3));
            Assert.That(simulation.Classes[0].Members[0].Key, Is.EqualTo("Bar"));
            Assert.That(simulation.Classes[0].Members[1].Key, Is.EqualTo("Baz"));
            Assert.That(simulation.Classes[0].Members[2].Key, Is.EqualTo("Foo"));
        }

        [Test]
        public void WHEN_Rendered_With_Hidden_Fields_THEN_Diagram_Has_One_Class_With_Members_Selectively_Hidden()
        {
            var simulation = new SimulatedClassDiagramGenerator(new HiddenFieldBarDiagram());
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(1));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("SimpleEntity"));
            Assert.That(simulation.Classes[0].Members.Count, Is.EqualTo(3));
            Assert.That(simulation.Classes[0].Members[0].Key, Is.EqualTo("Bar"));
            Assert.That(simulation.Classes[0].Members[0].IsHidden, Is.True);
            Assert.That(simulation.Classes[0].Members[1].Key, Is.EqualTo("Baz"));
            Assert.That(simulation.Classes[0].Members[1].IsHidden, Is.False);
            Assert.That(simulation.Classes[0].Members[2].Key, Is.EqualTo("Foo"));
            Assert.That(simulation.Classes[0].Members[2].IsHidden, Is.False);
        }

        #endregion

        public class HiddenFieldBarDiagram : ClassDiagram
        {
            #region Constructors and Destructors

            public HiddenFieldBarDiagram()
            {
                this.AddClass<SimpleEntity>().ForMember(x => x.Bar).Hide();
            }

            #endregion
        }

        public class SimpleEntity
        {
            #region Fields

            public string Bar;

            public string Baz;

            public string Foo;

            #endregion
        }
    }
}