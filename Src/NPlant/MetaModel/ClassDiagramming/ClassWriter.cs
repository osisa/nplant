// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassWriter.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class ClassWriter : IDescriptorWriter
    {
        #region Fields

        private readonly ClassDescriptor _class;

        private readonly ClassDiagram _diagram;

        #endregion

        #region Constructors and Destructors

        public ClassWriter(ClassDiagram diagram, ClassDescriptor @class)
        {
            _diagram = diagram;
            _class = @class;
        }

        #endregion

        #region Public Methods and Operators

        public string Write(ClassDiagramVisitorContext context)
        {
            var color = _class.Color ?? ClassDiagram.GetClassColor(_class);
            var buffer = new StringBuilder();

            if (_class.ReflectedType.IsInterface)
                buffer.AppendLine(string.Format("    interface \"{0}\"{1} {2}", _class.Name, color, "{"));

            else if (_class.ReflectedType.IsAbstract)
                buffer.AppendLine(string.Format("    abstract class \"{0}\"{1} {2}", _class.Name, color, "{"));

            else
                buffer.AppendLine(string.Format("    class \"{0}\"{1} {2}", _class.Name, color, "{"));

            var definedMembers = _class.Members.InnerList.Where(x => !x.IsInherited).OrderBy(x => x.Name).ToArray();

            if (!IsBaseClassVisible(_class, context))
            {
                var inheritedMembers = _class.Members.InnerList.Where(x => x.IsInherited).OrderBy(x => x.Name).ToArray();
                WriteClassMembers(inheritedMembers, buffer);

                if (definedMembers.Length > 0 && inheritedMembers.Length > 0)
                {
                    buffer.AppendLine("    --");
                }
            }

            WriteClassMembers(definedMembers, buffer);
            WriteClassMethods(_class.Methods.InnerList, buffer);

            buffer.AppendLine("    }");

            var note = _class.MetaModel.Note?.ToString();

            if (note != null)
            {
                buffer.AppendLine(note);
            }

            return buffer.ToString();
        }

        #endregion

        #region Methods

        private bool IsBaseClassVisible(ClassDescriptor @class, ClassDiagramVisitorContext context)
        {
            if (_diagram.RootClasses.InnerList.Any(x => x.ReflectedType == @class.ReflectedType.BaseType))
                return true;

            if (context.VisitedRelatedClasses.Any(x => x.ReflectedType == @class.ReflectedType.BaseType))
                return true;

            return false;
        }

        private static void WriteClassMembers(IEnumerable<ClassMemberDescriptor> members, StringBuilder buffer)
        {
            foreach (var member in members.Where(member => member.IsVisible))
            {
                if (member.MetaModel.IsPrimitive || member.TreatAsPrimitive)
                {
                    var accessModifier = member.AccessModifier.Notation;
                    var typeName = member.MetaModel.Name;
                    var memberName = member.Name;

                    buffer.AppendLine("    {0}{1} {2}".FormatWith(accessModifier, typeName, memberName));
                }
                else if (member.MetaModel.IsComplexType && member.MemberType.IsEnumerable())
                {
                    if (member.MemberType.GetEnumeratorType().IsPrimitive || member.MemberType.GetEnumeratorType() == typeof(string))
                    {
                        var accessModifier = member.AccessModifier.Notation;
                        var typeName = member.MetaModel.Name;
                        var memberName = member.Name;

                        buffer.AppendLine("    {0}{1} {2}".FormatWith(accessModifier, typeName, memberName));
                    }
                }
            }
        }

        private static void WriteClassMethods(IEnumerable<ClassMethodDescriptor> methods, StringBuilder buffer)
        {
            if (methods != null)
            {
                foreach (var method in methods)
                {
                    var accessModifier = method.AccessModifier.Notation;
                    var methodName = method.Name;
                    var args = method.Arguments;

                    buffer.AppendLine("    {0}{1}({2})".FormatWith(accessModifier, methodName, args));
                }
            }
        }

        #endregion
    }
}