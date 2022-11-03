// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SuppressionFixture.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using NPlant.MetaModel.ClassDiagramming;

using NUnit.Framework;

namespace NPlant.Tests.Diagrams.ClassDiagrams
{
    [TestFixture]
    public class SuppressionFixture
    {
        #region Public Methods and Operators

        [Test]
        public void Can_Completely_Hide_A_Type_From_The_Diagram()
        {
            var diagram = new Diagram();
            diagram.GenerationOptions.ForType<Child3>().Hide();

            var simulation = new SimulatedClassDiagramGenerator(diagram);
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(3));

            Assert.That(simulation.Classes["Subject"], Is.Not.Null);
            Assert.That(simulation.Classes["Child1"], Is.Not.Null);
            Assert.That(simulation.Classes["Child2"], Is.Not.Null);
            Assert.That(simulation.Classes["Child3", false], Is.Null);

            Assert.That(simulation.Classes["Subject"].Members["Child"].MetaModel.Hidden, Is.False);
            Assert.That(simulation.Classes["Child1"].Members["Child"].MetaModel.Hidden, Is.False);
            Assert.That(simulation.Classes["Child2"].Members["Child"].MetaModel.Hidden, Is.True);
        }

        [Test]
        public void Can_Treat_Type_As_Primitive()
        {
            var diagram = new Diagram();
            diagram.GenerationOptions.ForType<Child3>().TreatAsPrimitive();

            var simulation = new SimulatedClassDiagramGenerator(diagram);
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(3));

            Assert.That(simulation.Classes["Subject"], Is.Not.Null);
            Assert.That(simulation.Classes["Child1"], Is.Not.Null);
            Assert.That(simulation.Classes["Child2"], Is.Not.Null);
            Assert.That(simulation.Classes["Child3", false], Is.Null);

            Assert.That(simulation.Classes["Subject"].Members["Child"].MetaModel.Hidden, Is.False);
            Assert.That(simulation.Classes["Child1"].Members["Child"].MetaModel.Hidden, Is.False);
            Assert.That(simulation.Classes["Child2"].Members["Child"].MetaModel.Hidden, Is.False);
        }

        #endregion

        public class Child1
        {
            #region Fields

            public Child2 Child;

            public StringCollection Codes;

            #endregion
        }

        public class Child2
        {
            #region Fields

            public Child3 Child;

            public StringCollection Codes;

            #endregion
        }

        public class Child3
        {
            #region Fields

            public StringCollection Codes;

            #endregion
        }

        public class StringCollection : List<string>
        {
        }

        public class Subject
        {
            #region Fields

            public Child1 Child;

            public string Code;

            public StringCollection Codes;

            #endregion
        }

        internal class Diagram : ClassDiagram
        {
            #region Constructors and Destructors

            public Diagram()
            {
                AddClass<Subject>();
            }

            #endregion
        }
    }
}