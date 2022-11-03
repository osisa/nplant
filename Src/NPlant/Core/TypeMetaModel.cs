// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="TypeMetaModel.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace NPlant.Core
{
    public class TypeMetaModel
    {
        #region Fields

        private readonly TypeNote _note = new TypeNote();

        private readonly Type _type;

        private bool _isPrimitive;

        #endregion

        #region Constructors and Destructors

        public TypeMetaModel(Type type)
        {
            _type = type;

            if (IsDefaultPrimitive())
            {
                this.IsPrimitive = true;
            }
            else
            {
                this.IsComplexType = IsDefactoComplexType(_type);
                this.IsPrimitive = !this.IsComplexType;
            }

            this.Name = GetFriendlyDataType(type);
            this.HideAsBaseClass = _type == typeof(object);
        }

        #endregion

        #region Public Properties

        public bool Hidden { get; internal set; }

        public bool HideAsBaseClass { get; internal set; }

        public bool IsComplexType { get; internal set; }

        public bool IsPrimitive
        {
            get
            {
                return _isPrimitive;
            }
            internal set
            {
                _isPrimitive = value;

                this.IsComplexType = !this.IsPrimitive;
            }
        }

        public string Name { get; internal set; }

        public TypeNote Note
        {
            get
            {
                return _note;
            }
        }

        public bool TreatAllMembersAsPrimitives { get; internal set; }

        #endregion

        #region Public Methods and Operators

        public static bool IsDefactoComplexType(Type type)
        {
            if (type.IsString())
                return false;

            if (type.IsEnumerable())
                return true;

            return (type.IsClass || type.IsInterface || type.IsEnum);
        }

        #endregion

        #region Methods

        private static string GetFriendlyDataType(Type type)
        {
            if (type.IsGenericType)
            {
                var def = type.GetGenericTypeDefinition();
                var genericArguments = type.GetGenericArguments();

                string outerName;
                string innerName;

                if (typeof(Nullable<>) == def)
                {
                    outerName = "Nullable";
                    var nullableType = genericArguments[0];
                    innerName = nullableType.Name;
                }
                else
                {
                    outerName = def.Name.Substring(0, def.Name.IndexOf("`", StringComparison.Ordinal));

                    var builder = new StringBuilder();

                    for (var x = 0; x < genericArguments.Length; x++)
                    {
                        if (x > 0)
                            builder.Append(", ");

                        var genericArgument = genericArguments[x];

                        builder.Append(GetFriendlyDataType(genericArgument));
                    }

                    innerName = builder.ToString();
                }

                return "{0}<{1}>".FormatWith(outerName, innerName);
            }

            return type.Name;
        }

        private bool IsDefaultPrimitive()
        {
            return !_type.IsEnumerable() && _type.IsMsCoreLibType() && IsDefactoComplexType(_type);
        }

        #endregion
    }

    public class TypeNote
    {
        #region Fields

        private readonly List<string> _lines = new List<string>();

        private NoteDirection _direction;

        #endregion

        #region Public Methods and Operators

        public TypeNote AddLine(string line)
        {
            _lines.Add(line);

            return this;
        }

        public TypeNote DisplayBottom()
        {
            _direction = NoteDirection.bottom;
            return this;
        }

        public TypeNote DisplayLeft()
        {
            _direction = NoteDirection.left;
            return this;
        }

        public TypeNote DisplayRight()
        {
            _direction = NoteDirection.right;
            return this;
        }

        public TypeNote DisplayTop()
        {
            _direction = NoteDirection.top;
            return this;
        }

        public override string ToString()
        {
            if (_lines.Count > 0)
            {
                return "note {0}: {1}".FormatWith(_direction, string.Join("\\n", _lines));
            }

            return string.Empty;
        }

        #endregion
    }

    public enum NoteDirection
    {
        // ReSharper disable InconsistentNaming
        left,

        right,

        top,

        bottom
        // ReSharper restore InconsistentNaming
    }
}