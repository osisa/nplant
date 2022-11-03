// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="HideDiagramAttribute.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class HideDiagramAttribute : Attribute
    {
    }
}