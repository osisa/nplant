// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassMemberDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

using NPlant.Core;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class ClassMemberDescriptor : IKeyedItem
    {
        #region Fields

        private readonly ClassDescriptor _descriptor;

        #endregion

        #region Constructors and Destructors

        public ClassMemberDescriptor(ClassDescriptor descriptor, MemberInfo member)
        {
            var property = member as PropertyInfo;
            AccessModifier = AccessModifier.Public;

            if (property != null)
            {
                MemberType = property.PropertyType;
            }

            var field = member as FieldInfo;

            if (field != null)
            {
                MemberType = field.FieldType;
            }

            AccessModifier = AccessModifier.GetAccessModifier(member);

            if (MemberType == null)
                throw new NPlantException("Member's could not be interpreted as either a property or a field");

            _descriptor = descriptor;

            Name = member.Name;
            Key = Name;
            MetaModel = ClassDiagramVisitorContext.Current.GetTypeMetaModel(MemberType);
            IsInherited = member.DeclaringType != descriptor.ReflectedType;
        }

        #endregion

        #region Public Properties

        public AccessModifier AccessModifier { get; private set; }

        public bool IsHidden
        {
            get
            {
                if (MetaModel.Hidden)
                    return true;

                return !_descriptor.GetMemberVisibility(Name);
            }
        }

        public bool IsInherited { get; private set; }

        public bool IsVisible => !IsHidden;

        public string Key { get; private set; }

        public Type MemberType { get; private set; }

        public TypeMetaModel MetaModel { get; }

        public string Name { get; private set; }

        #endregion

        #region Properties

        internal bool TreatAsPrimitive { get; set; }

        #endregion
    }
}