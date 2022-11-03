// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="EnumJoiner.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Core
{
    public static class EnumJoiner
    {
        #region Public Methods and Operators

        public static string Join<T>()
        {
            var names = Enum.GetNames(typeof(T));
            return string.Join(",", names);
        }

        #endregion
    }
}