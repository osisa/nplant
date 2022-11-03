// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDiagramPackage.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NPlant.MetaModel.ClassDiagramming;

namespace NPlant
{
    public class ClassDiagramPackage
    {
        #region Fields

        private readonly List<Func<ClassDescriptor, bool>> _matcher = new List<Func<ClassDescriptor, bool>>();

        #endregion

        #region Constructors and Destructors

        internal ClassDiagramPackage(string name, ClassDiagram diagram)
        {
            Diagram = diagram;
            Name = name;
        }

        #endregion

        #region Public Properties

        public ClassDiagram Diagram { get; }

        public string Name { get; }

        #endregion

        #region Public Methods and Operators

        public ClassDiagram IncludeAll()
        {
            IncludeClassesWhere(descriptor => true);

            return Diagram;
        }

        public ClassDiagramPackage IncludeClassesWhere(Func<ClassDescriptor, bool> filter)
        {
            _matcher.Add(filter);

            return this;
        }

        public bool IsMatch(ClassDescriptor classDescriptor)
        {
            return _matcher.Any(matcher => matcher(classDescriptor));
        }

        #endregion
    }
}