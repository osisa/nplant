// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;

using NPlant.Core;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public abstract class ClassDescriptor : IKeyedItem
    {
        #region Fields

        protected internal readonly IDictionary<string, bool> MemberVisibility = new Dictionary<string, bool>();

        #endregion

        #region Constructors and Destructors

        protected ClassDescriptor(Type reflectedType)
        {
            this.RenderInheritance = true;
            this.ReflectedType = reflectedType;
            this.Name = this.ReflectedType.GetFriendlyGenericName();
        }

        #endregion

        #region Public Properties

        public string Color { get; private set; }

        public int Level { get; protected set; }

        public KeyedList<ClassMemberDescriptor> Members { get; } = new ();

        public TypeMetaModel MetaModel { get; private set; }

        public KeyedList<ClassMethodDescriptor> Methods { get; } = new ();

        public string Name { get; protected set; }

        public Type ReflectedType { get; private set; }

        public bool RenderInheritance { get; set; }

        #endregion

        #region Explicit Interface Properties

        string IKeyedItem.Key => Name;

        #endregion

        #region Public Methods and Operators

        public override bool Equals(object obj)
        {
            ClassDescriptor descriptor = obj as ClassDescriptor;

            if (descriptor == null)
                return false;

            return descriptor.ReflectedType == ReflectedType;
        }

        public override int GetHashCode()
            => ReflectedType.GetHashCode();
        
        public virtual bool GetMemberVisibility(string name)
            => !MemberVisibility.TryGetValue(name, out var visibility) || visibility;
            // default to visible (i.e. if no specification is present, assume visible)
        
        public abstract IDescriptorWriter GetWriter(ClassDiagram diagram);

        public void Visit()
        {
            var context = ClassDiagramVisitorContext.Current;
            this.MetaModel = context.GetTypeMetaModel(this.ReflectedType);

            if (context.ShowMembers)
                LoadMembers(context);

            if (context.ShowMethods)
                LoadMethods(context);

            var showInheritance = ShouldShowInheritance(context);

            if (!this.MetaModel.Hidden)
            {
                foreach (ClassMemberDescriptor member in this.Members.InnerList)
                {
                    if (this.MetaModel.TreatAllMembersAsPrimitives)
                        member.TreatAsPrimitive = true;

                    if (member.IsVisible && !member.TreatAsPrimitive)
                    {
                        // if not showing inheritance then show all members
                        // otherwise, only show member that aren't inherited
                        if (!showInheritance || !member.IsInherited)
                        {
                            if (member.MetaModel.IsComplexType && this.GetMemberVisibility(member.Key))
                            {
                                var nextLevel = this.Level + 1;

                                if (member.MemberType.IsEnumerable())
                                {
                                    var enumeratorType = member.MemberType.GetEnumeratorType();
                                    var enumeratorTypeMetaModel = context.GetTypeMetaModel(enumeratorType);

                                    if (enumeratorTypeMetaModel.IsComplexType)
                                    {
                                        context.AddRelated(this, enumeratorType.GetDescriptor(context), ClassDiagramRelationshipTypes.HasMany, nextLevel, member.Name);
                                    }
                                }
                                else
                                {
                                    context.AddRelated(this, member.MemberType.GetDescriptor(context), ClassDiagramRelationshipTypes.HasA, nextLevel, member.Name);
                                }
                            }
                        }
                    }
                }
            }

            if (showInheritance)
            {
                context.AddRelated(this, this.ReflectedType.BaseType.GetDescriptor(context), ClassDiagramRelationshipTypes.Base, this.Level - 1);
            }

            foreach (Type type in this.ReflectedType.GetInterfaces())
            {
                if (this.ReflectedType.BaseType == null || this.ReflectedType.BaseType.GetInterfaces().ToList().Exists(p => p.Equals(type)))
                    continue;
                if (ShouldShowInheritanceInterface(context, type))
                    context.AddRelated(this, type.GetDescriptor(context), ClassDiagramRelationshipTypes.Base, this.Level - 1);
            }
        }

        #endregion

        #region Methods

        protected virtual void LoadMembers(ClassDiagramVisitorContext context)
        {
            switch (context.ScanMode)
            {
                case ClassDiagramScanModes.SystemServiceModelMember:
                    Members.AddRange(
                        this.ReflectedType.GetFields()
                            .Where(x => x.HasAttribute<DataMemberAttribute>() || x.HasAttribute<MessageBodyMemberAttribute>())
                            .Where(x => !x.IsDefined(typeof(CompilerGeneratedAttribute), false))
                            .Select(field => new ClassMemberDescriptor(this, field))
                    );
                    Members.AddRange(
                        this.ReflectedType.GetProperties()
                            .Where(x => x.HasAttribute<DataMemberAttribute>() || x.HasAttribute<MessageBodyMemberAttribute>())
                            .Where(x => !x.IsDefined(typeof(CompilerGeneratedAttribute), false))
                            .Select(property => new ClassMemberDescriptor(this, property))
                    );
                    break;
                case ClassDiagramScanModes.AllMembers:
                    Members.AddRange(
                        this.ReflectedType.GetFields(context.ShowMembersBindingFlags)
                            .Where(x => !x.IsDefined(typeof(CompilerGeneratedAttribute), false))
                            .Select(field => new ClassMemberDescriptor(this, field))
                    );
                    Members.AddRange(
                        this.ReflectedType.GetProperties(context.ShowMembersBindingFlags)
                            .Where(x => !x.IsDefined(typeof(CompilerGeneratedAttribute), false))
                            .Select(property => new ClassMemberDescriptor(this, property))
                    );
                    break;
                default:
                    Members.AddRange(
                        this.ReflectedType.GetFields()
                            .Where(x => !x.IsDefined(typeof(CompilerGeneratedAttribute), false))
                            .Select(field => new ClassMemberDescriptor(this, field))
                    );
                    Members.AddRange(
                        this.ReflectedType.GetProperties()
                            .Where(x => !x.IsDefined(typeof(CompilerGeneratedAttribute), false))
                            .Select(property => new ClassMemberDescriptor(this, property))
                    );
                    break;
            }
        }

        private void LoadMethods(ClassDiagramVisitorContext context)
        {
            var methods = this.ReflectedType.GetMethods(context.ShowMethodsBindingFlags).OrderBy(method => method.Name);

            foreach (var method in methods)
            {
                if (!method.IsProperty()) // weed up the compiler generated methods for properties
                    Methods.Add(new ClassMethodDescriptor(method));
            }
        }

        private bool ShouldShowInheritance(ClassDiagramVisitorContext context)
        {
            bool showInheritance = this.RenderInheritance && this.ReflectedType.BaseType != null;

            if (showInheritance)
            {
                var baseTypeMetaModel = context.GetTypeMetaModel(this.ReflectedType.BaseType);

                showInheritance = !baseTypeMetaModel.HideAsBaseClass && !baseTypeMetaModel.Hidden;
            }

            return showInheritance;
        }

        private bool ShouldShowInheritanceInterface(ClassDiagramVisitorContext context, Type type)
        {
            bool showInheritance = this.RenderInheritance && this.ReflectedType.BaseType != null;

            if (showInheritance)
            {
                var interfaceModel = context.GetTypeMetaModel(type);
                showInheritance |= !interfaceModel.HideAsBaseClass && !interfaceModel.Hidden;
            }

            return showInheritance;
        }

        #endregion
    }
}