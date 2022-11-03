// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ReflectedClassDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class ReflectedClassDescriptor : ClassDescriptor
    {
        #region Constructors and Destructors

        public ReflectedClassDescriptor(Type type)
            : base(type)
        {
            this.Level = 1;
        }

        #endregion

        #region Public Methods and Operators

        public override bool GetMemberVisibility(string name)
        {
            return true;
        }

        public override IDescriptorWriter GetWriter(ClassDiagram diagram)
        {
            return new ClassWriter(diagram, this);
        }

        #endregion

        #region Methods

        internal void SetLevel(int level)
        {
            this.Level = level;
        }

        #endregion
    }
}