using NPlant.Core;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ForTypeDescriptor<T>
    {
        private readonly ClassDiagram _diagram;
        private readonly TypeMetaModel _typeMetaModel;

        public ForTypeDescriptor(ClassDiagram diagram)
        {
            _diagram = diagram;
            _typeMetaModel = _diagram.Types[typeof(T)];
        }

        public ClassDiagramOptions TreatAsPrimitive()
        {
            _typeMetaModel.IsPrimitive = true;
            return _diagram.GenerationOptions;
        }

        public ForTypeDescriptor<T> ShowAsBase()
        {
            _typeMetaModel.HideAsBaseClass = false;

            return this;
        }

        public ForTypeDescriptor<T> NeverShowRelationshipsFromHere()
        {
            _typeMetaModel.TreatAllMembersAsPrimitives = true;
            return this;
        }

        public ForTypeDescriptor<T> HideAsBase()
        {
            _typeMetaModel.HideAsBaseClass = true;

            return this;
        }

        public TypeNote Note()
        {
            return _typeMetaModel.Note;
        }

        public void Hide()
        {
            // breaking the chain here... but what else makes sense to do?  continue to 
            // configure a hidden thing?  does that make sense?
            _typeMetaModel.Hidden = true;
        }
    }
}