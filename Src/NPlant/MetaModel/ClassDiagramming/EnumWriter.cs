// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="EnumWriter.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;

using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class EnumWriter : IDescriptorWriter
    {
        #region Fields

        private readonly Type _enumType;

        #endregion

        #region Constructors and Destructors

        public EnumWriter(Type enumType)
        {
            _enumType = enumType;
        }

        #endregion

        #region Public Methods and Operators

        public string Write(ClassDiagramVisitorContext context)
        {
            StringBuilder buffer = new StringBuilder();

            var names = Enum.GetNames(_enumType);

            buffer.AppendLine("enum \"{0}\" {1}".FormatWith(_enumType.Name, "{"));

            if (context.ShowMembers)
            {
                foreach (var name in names)
                {
                    buffer.AppendLine("     {0}".FormatWith(name));
                }
            }

            buffer.AppendLine("}");

            return buffer.ToString();
        }

        #endregion
    }
}