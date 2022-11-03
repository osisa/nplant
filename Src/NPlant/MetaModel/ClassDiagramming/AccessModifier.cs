// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="AccessModifier.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Reflection;

namespace NPlant.MetaModel.ClassDiagramming
{
    public struct AccessModifier
    {
        #region Static Fields

        public static readonly AccessModifier Internal = new("Internal", "~");

        public static readonly AccessModifier Private = new("Private", "-");

        public static readonly AccessModifier Protected = new("Protected", "#");

        public static readonly AccessModifier Public = new("Public", "+");

        public static readonly AccessModifier Unknown = new("Public", "+");

        #endregion

        #region Constructors and Destructors

        private AccessModifier(string name, string notation)
            : this()
        {
            Name = name;
            Notation = notation;
        }

        #endregion

        #region Public Properties

        public string Name { get; private set; }

        public string Notation { get; private set; }

        #endregion

        #region Public Methods and Operators

        public static AccessModifier GetAccessModifier(MemberInfo memberInfo)
        {
            if (memberInfo.IsPrivate())
                return Private;

            if (memberInfo.IsPublic())
                return Public;

            if (memberInfo.IsInternal())
                return Internal;

            return Protected;
        }

        public static AccessModifier GetAccessModifier(MethodInfo methodInfo)
        {
            if (methodInfo.IsPrivate)
                return Private;

            if (methodInfo.IsPublic)
                return Public;

            if (methodInfo.IsAssembly)
                return Internal;

            return Protected;
        }

        #endregion
    }
}