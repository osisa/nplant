// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="DiagramsElement.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using NAnt.Core;
using NAnt.Core.Attributes;

namespace NPlant.NAntTasks
{
    [Serializable]
    public class DiagramsElement : Element
    {
        #region Fields

        private DiagramElementCollection _diagrams = new ();

        #endregion

        #region Public Properties

        [BuildElementArray("diagram")]
        public virtual DiagramElementCollection Diagrams
        {
            get => _diagrams;
            set => _diagrams = value;
        }

        [TaskAttribute("in", Required = false)]
        public string In { get; set; }

        #endregion
    }
}