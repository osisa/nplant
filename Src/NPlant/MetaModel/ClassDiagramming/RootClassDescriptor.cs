// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="RootClassDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Reflection;

using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class RootClassDescriptor<T> : ClassDescriptor
    {
        #region Constructors and Destructors

        public RootClassDescriptor()
            : base(typeof(T))
        {
        }

        #endregion

        #region Public Methods and Operators

        public ForMemberDescriptor<T> ForMember<TMember>(Expression<Func<T, TMember>> expression)
        {
            return new ForMemberDescriptor<T>(this, ReflectOn<T>.ForMember(expression));
        }

        public override IDescriptorWriter GetWriter(ClassDiagram diagram)
        {
            return new ClassWriter(diagram, this);
        }

        public RootClassDescriptor<T> HideInheritors()
        {
            this.RenderInheritance = false;
            return this;
        }

        public RootClassDescriptor<T> HideMember<TMember>(Expression<Func<T, TMember>> expression)
        {
            var member = ReflectOn<T>.ForMember(expression);
            MemberVisibility[member.Name] = false;

            return this;
        }

        public RootClassDescriptor<T> HideMember(string memberName)
        {
            MemberVisibility[memberName] = false;

            return this;
        }

        public RootClassDescriptor<T> Named(string name)
        {
            this.Name = name;
            return this;
        }

        public RootClassDescriptor<T> ShowInheritors()
        {
            this.RenderInheritance = true;
            return this;
        }

        #endregion

        public class ForMemberDescriptor<TMember>
        {
            #region Fields

            private readonly ClassDescriptor _descriptor;

            private readonly MemberInfo _member;

            #endregion

            #region Constructors and Destructors

            public ForMemberDescriptor(ClassDescriptor descriptor, MemberInfo member)
            {
                _descriptor = descriptor;
                _member = member;
            }

            #endregion

            #region Public Methods and Operators

            public ForMemberDescriptor<TMember> CustomerDiagram<TForMember>(Expression<Func<TMember, TForMember>> expression)
            {
                return new ForMemberDescriptor<TMember>(_descriptor, ReflectOn<TMember>.ForMember(expression));
            }

            public ForMemberDescriptor<TMember> Hide()
            {
                _descriptor.MemberVisibility[_member.Name] = false;
                return this;
            }

            #endregion
        }
    }
}