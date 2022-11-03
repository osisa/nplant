// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="RootEnumDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using NPlant.Core;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class RootEnumDescriptor : ClassDescriptor
    {
        #region Constructors and Destructors

        public RootEnumDescriptor(Type reflectedType)
            : base(reflectedType)
        {
            if (!reflectedType.IsEnum)
                throw new NPlantException("Expected the type '{0}' to be an enum".FormatWith(reflectedType.FullName));

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
            // don't load any member for enums
        }

        #endregion
    }
}