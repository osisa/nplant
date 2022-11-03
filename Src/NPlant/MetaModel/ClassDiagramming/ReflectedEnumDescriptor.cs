// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ReflectedEnumDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class ReflectedEnumDescriptor : ReflectedClassDescriptor
    {
        #region Constructors and Destructors

        public ReflectedEnumDescriptor(Type enumType)
            : base(enumType)
        {
            this.RenderInheritance = false;
        }

        #endregion

        #region Public Methods and Operators

        public override IDescriptorWriter GetWriter(ClassDiagram diagram)
        {
            return new EnumWriter(this.ReflectedType);
        }

        #endregion

        #region Methods

        protected override void LoadMembers(ClassDiagramVisitorContext context)
        {
            // don't load members if it's an enum
        }

        #endregion
    }
}