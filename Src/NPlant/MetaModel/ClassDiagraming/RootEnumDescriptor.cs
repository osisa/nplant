﻿using System;
using NPlant.Core;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class RootEnumDescriptor : ClassDescriptor
    {
        public RootEnumDescriptor(Type reflectedType) : base(reflectedType)
        {
            if(!reflectedType.IsEnum)
                throw new NPlantException("Expected the type '{0}' to be an enum".FormatWith(reflectedType.FullName));

            this.RenderInheritance = false;
        }

        protected override void LoadMembers(ClassDiagramVisitorContext context)
        {
            // don't load any member for enums
        }

        public override IDescriptorWriter GetWriter(ClassDiagram diagram)
        {
            return new EnumWriter(this.ReflectedType);
        }
    }
}