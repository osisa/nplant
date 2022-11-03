﻿using System;
using System.Text;
using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class EnumWriter : IDescriptorWriter
    {
        private readonly Type _enumType;

        public EnumWriter(Type enumType)
        {
            _enumType = enumType;
        }

        public string Write(ClassDiagramVisitorContext context)
        {
            StringBuilder buffer = new StringBuilder();

            var names = Enum.GetNames(_enumType);

            buffer.AppendLine("enum \"{0}\" {1}".FormatWith(_enumType.Name, "{"));

            if(context.ShowMembers)
            {
                foreach (var name in names)
                {
                    buffer.AppendLine("     {0}".FormatWith(name));
                }
            }

            buffer.AppendLine("}");

            return buffer.ToString();
        }
    }
}