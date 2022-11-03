// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="AssemblyDescriptor.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Reflection;

using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagramming
{
    public class AssemblyDescriptor : IKeyedItem
    {
        #region Constructors and Destructors

        public AssemblyDescriptor(Assembly assembly)
        {
            Assembly = assembly;
        }

        #endregion

        #region Public Properties

        public Assembly Assembly { get; private set; }

        public string Key => Assembly.FullName;

        #endregion
    }
}