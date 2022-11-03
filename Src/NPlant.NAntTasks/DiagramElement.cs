// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="DiagramElement.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using NAnt.Core;
using NAnt.Core.Attributes;

namespace NPlant.NAntTasks
{
    [Serializable]
    public class DiagramElement : Element
    {
        #region Public Properties

        [TaskAttribute("named", Required = true)]
        public string Named { get; set; }

        [TaskAttribute("output", Required = false)]
        public string Output { get; set; }

        #endregion
    }
}