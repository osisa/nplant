// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SimulatedClassDiagramGenerator.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Core;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class SimulatedClassDiagramGenerator : ClassDiagramGenerator
    {
        #region Fields

        private readonly KeyedList<ClassDescriptor> _classes = new KeyedList<ClassDescriptor>();

        private readonly ClassDiagram _definition;

        #endregion

        #region Constructors and Destructors

        public SimulatedClassDiagramGenerator(ClassDiagram diagram)
            : base(diagram)
        {
            _definition = diagram;
        }

        #endregion

        #region Public Properties

        public KeyedList<ClassDescriptor> Classes 
            => _classes;

        public TypeMetaModelSet Types 
            => _definition.Types;

        #endregion

        #region Methods

        protected override void Finalize(ClassDiagramVisitorContext current)
        {
            _classes.AddRange(current.VisitedRelatedClasses);
        }

        protected override void OnRootClassVisited(ClassDescriptor rootClass)
        {
            _classes.Add(rootClass);
        }

        #endregion
    }
}