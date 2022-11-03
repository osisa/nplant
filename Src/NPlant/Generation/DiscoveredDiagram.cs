﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="DiscoveredDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.Generation
{
    public class DiscoveredDiagram
    {
        #region Constructors and Destructors

        public DiscoveredDiagram(string @namespace, ClassDiagram diagram)
        {
            Namespace = @namespace;
            Diagram = diagram;
        }

        #endregion

        #region Public Properties

        public ClassDiagram Diagram { get; private set; }

        public string Namespace { get; private set; }

        #endregion
    }
}