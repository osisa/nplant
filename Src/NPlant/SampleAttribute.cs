// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="SampleAttribute.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant
{
    public sealed class SampleAttribute : Attribute
    {
        #region Constructors and Destructors

        public SampleAttribute()
        {
        }

        public SampleAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Public Properties

        public string Description { get; private set; }

        public string Name { get; private set; }

        #endregion
    }
}