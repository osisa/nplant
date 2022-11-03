// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimpleAddAllSubClassesOfClassDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.Samples.AddAllSubClassesOf
{
    public class SimpleAddAllSubClassesOfClassDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public SimpleAddAllSubClassesOfClassDiagram()
        {
            AddAllSubClassesOff<Animal>();
            GenerationOptions.ForType<Thing>().HideAsBase();
            GenerationOptions.ShowMethods();
        }

        #endregion
    }

    public abstract class Thing
    {
        #region Public Properties

        public string Id { get; set; }

        #endregion
    }

    public class MiddleMan : Thing
    {
    }

    public class Animal : MiddleMan
    {
        #region Public Properties

        public string Name { get; set; }

        #endregion
    }

    public class Gorilla : Animal
    {
        #region Public Properties

        public string ChestSize { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Fight()
        {
        }

        #endregion
    }

    public class Elephant : Animal
    {
        #region Public Properties

        public string TuskSize { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Charge()
        {
        }

        #endregion
    }

    public class Chair
    {
    }
}