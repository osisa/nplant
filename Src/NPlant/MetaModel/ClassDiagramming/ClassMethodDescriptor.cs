// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassMethodDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Reflection;

using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class ClassMethodDescriptor : IKeyedItem
    {
        #region Constructors and Destructors

        public ClassMethodDescriptor(MethodInfo method)
        {
            Arguments = BuildArguments(method);
            Key = BuildKey(method, this.Arguments);
            Name = method.Name;
            AccessModifier = AccessModifier.GetAccessModifier(method);
        }

        #endregion

        #region Public Properties

        public AccessModifier AccessModifier { get; private set; }

        public string Arguments { get; private set; }

        public string Key { get; private set; }

        public string Name { get; private set; }

        #endregion

        #region Methods

        private static string BuildArguments(MethodInfo method)
        {
            var parameters = method.GetParameters();

            if (parameters.Length < 1)
                return null;

            return string.Join(", ", parameters.Select(x => x.ParameterType.GetFriendlyGenericName()));
        }

        private string BuildKey(MethodInfo method, string arguments)
        {
            return "{0}({1})".FormatWith(method.Name, arguments);
        }

        #endregion
    }
}