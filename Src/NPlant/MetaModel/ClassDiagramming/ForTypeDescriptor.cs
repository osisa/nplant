// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ForTypeDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Core;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class ForTypeDescriptor<T>
    {
        #region Fields

        private readonly ClassDiagram _diagram;

        private readonly TypeMetaModel _typeMetaModel;

        #endregion

        #region Constructors and Destructors

        public ForTypeDescriptor(ClassDiagram diagram)
        {
            _diagram = diagram;
            _typeMetaModel = _diagram.Types[typeof(T)];
        }

        #endregion

        #region Public Methods and Operators

        public void Hide()
        {
            // breaking the chain here... but what else makes sense to do?  continue to 
            // configure a hidden thing?  does that make sense?
            _typeMetaModel.Hidden = true;
        }

        public ForTypeDescriptor<T> HideAsBase()
        {
            _typeMetaModel.HideAsBaseClass = true;

            return this;
        }

        public ForTypeDescriptor<T> NeverShowRelationshipsFromHere()
        {
            _typeMetaModel.TreatAllMembersAsPrimitives = true;
            return this;
        }

        public TypeNote Note()
        {
            return _typeMetaModel.Note;
        }

        public ForTypeDescriptor<T> ShowAsBase()
        {
            _typeMetaModel.HideAsBaseClass = false;

            return this;
        }

        public ClassDiagramOptions TreatAsPrimitive()
        {
            _typeMetaModel.IsPrimitive = true;
            return _diagram.GenerationOptions;
        }

        #endregion
    }
}