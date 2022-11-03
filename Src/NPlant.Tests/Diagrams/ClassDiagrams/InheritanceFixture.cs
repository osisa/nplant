// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="InheritanceFixture.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.MetaModel.ClassDiagramming;

using NUnit.Framework;

namespace NPlant.Tests.Diagrams.ClassDiagrams
{
    [TestFixture]
    public class InheritanceFixture
    {
        #region Interfaces

        public interface IBaz
        {
        }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void Base_Class_Will_Be_Rendered_By_Default()
        {
            var simulation = new SimulatedClassDiagramGenerator(new SimpleDiagram());

            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(4));
        }

        [Test]
        public void Interface_Class_Test()
        {
            var simulation = new SimulatedClassDiagramGenerator(new ClassDiagram(typeof(Foo)));
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(3));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("Foo"));
            Assert.That(simulation.Classes[1].Name, Is.EqualTo("Bar"));
            Assert.That(simulation.Classes[2].Name, Is.EqualTo("IBaz"));
        }

        #endregion

        public abstract class Bar : IBaz
        {
        }

        public class Foo : Bar
        {
        }

        public class SecondSubSimpleEntity : SimpleEntity
        {
        }

        public class SimpleDiagram : ClassDiagram
        {
            #region Constructors and Destructors

            public SimpleDiagram()
            {
                base.AddClass<SubSimpleEntity>();
                base.AddClass<SecondSubSimpleEntity>();
                base.AddClass<SubSubSimpleEntity>();
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

        public class SubSimpleEntity : SimpleEntity
        {
            #region Fields

            public string Bah;

            #endregion
        }

        public class SubSubSimpleEntity : SubSimpleEntity
        {
            #region Fields

            public string Boo;

            #endregion
        }
    }
}