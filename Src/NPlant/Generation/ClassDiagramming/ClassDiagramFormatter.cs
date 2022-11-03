// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDiagramFormatter.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPlant.Core;
using NPlant.MetaModel.ClassDiagramming;

namespace NPlant.Generation.ClassDiagramming
{
    public class ClassDiagramFormatter : IDiagramFormatter
    {
        #region Fields

        private readonly StringBuilder _buffer = new StringBuilder();

        private readonly ClassDiagramVisitorContext _context;

        private readonly ClassDiagram _diagram;

        #endregion

        #region Constructors and Destructors

        public ClassDiagramFormatter(ClassDiagram diagram, ClassDiagramVisitorContext context)
        {
            _diagram = diagram;
            _context = context;
        }

        #endregion

        #region Public Methods and Operators

        public string Format()
        {
            _buffer.AppendLine("@startuml");

            WriteTitle(_diagram.Title);

            var unpacked = new List<string>();
            var packageMap = new Dictionary<string, List<string>>();

            WriteRootClasses(packageMap, unpacked);

            WriteRelatedClasses(packageMap, unpacked);

            WritePackages(packageMap, unpacked);

            WriteRelationships();

            WriteLegend(_diagram.Legend);

            WriteNotes(_diagram.Notes);

            _buffer.AppendLine("@enduml");

            return _buffer.ToString();
        }

        #endregion

        #region Methods

        private void AppendClassToPackageMap(
            Dictionary<string, List<string>> packageMap,
            string package,
            string classDefinition)
        {
            List<string> classDefinitions;

            if (!packageMap.TryGetValue(package, out classDefinitions))
            {
                classDefinitions = new List<string>();
                packageMap.Add(package, classDefinitions);
            }

            classDefinitions.Add(classDefinition);
        }

        private void AssignToPackage(
            ClassDescriptor rootClass,
            Dictionary<string, List<string>> packageMap,
            string classDefinition,
            List<string> unpackaged)
        {
            foreach (var package in _diagram.Packages)
            {
                if (package.IsMatch(rootClass))
                {
                    AppendClassToPackageMap(packageMap, package.Name, classDefinition);
                    return;
                }
            }

            unpackaged.Add(classDefinition);
        }

        private string WriteClassDefinition(ClassDescriptor @class)
        {
            IDescriptorWriter writer = @class.GetWriter(_diagram);
            return writer.Write(_context);
        }

        private void WriteClassRelationship(ClassDiagramRelationship relationship)
        {
            string suffix = "";
            string arrow = "";

            switch (relationship.RelationshipType)
            {
                case ClassDiagramRelationshipTypes.Base:
                    arrow = "-up-|>";
                    suffix = " : Extends";
                    break;
                case ClassDiagramRelationshipTypes.HasA:
                    arrow = "*--";
                    suffix = (relationship.Name.StartsWithAVowel() ? " : Has An \\n{0}" : " : Has A \\n{0}").FormatWith(relationship.Name);
                    break;
                case ClassDiagramRelationshipTypes.HasMany:
                    arrow = "*--";
                    suffix = " : Has Many \\n{0}".FormatWith(relationship.Name);
                    break;
                default:
                    throw new NPlantException("Unrecognized relationship type:  {0}".FormatWith(relationship));
            }

            _buffer.AppendLine(
                "\"{0}\" {1} \"{2}\"{3}".FormatWith(
                    relationship.Party1.Name,
                    arrow,
                    relationship.Party2.Name,
                    suffix));
        }

        private void WriteLegend(ClassDiagramLegend legend)
        {
            if (legend != null)
            {
                _buffer.AppendLine("legend {0}".FormatWith(legend.Position));
                _buffer.AppendLine("    {0}".FormatWith(legend.Text));
                _buffer.AppendLine("endlegend");
            }
        }

        private void WriteNotes(IEnumerable<ClassDiagramNote> notes)
        {
            if (notes != null)
            {
                var notesArray = notes.ToArray();

                for (int i = 0; i < notesArray.Length; i++)
                {
                    ClassDiagramNote note = notesArray[i];

                    string lines = string.Join("\\n", note.Lines);

                    string key = "N" + i;
                    _buffer.AppendLine("note \"{0}\" as {1}".FormatWith(lines, key));

                    foreach (var connection in note.ConnectedClasses)
                    {
                        ClassDescriptor descriptor = connection.GetReflected();

                        _buffer.AppendLine("{0} .. {1}".FormatWith(descriptor.Name, key));
                    }
                }
            }
        }

        private void WritePackages(Dictionary<string, List<string>> packageMap, IEnumerable<string> unpackaged)
        {
            foreach (var key in packageMap.Keys)
            {
                var package = packageMap[key];

                _buffer.AppendLine(string.Format("package \"{0}\"{1}", key, "{"));

                foreach (var classDefinition in package)
                {
                    _buffer.AppendLine(classDefinition);
                }

                _buffer.AppendLine(string.Format("{0}", "}"));
            }

            foreach (var item in unpackaged)
            {
                _buffer.AppendLine(item);
            }
        }

        private void WriteRelatedClasses(Dictionary<string, List<string>> packageMap, List<string> unpackaged)
        {
            foreach (var relatedClass in _context.VisitedRelatedClasses)
            {
                string classDefinition = WriteClassDefinition(relatedClass);

                AssignToPackage(relatedClass, packageMap, classDefinition, unpackaged);
            }
        }

        private void WriteRelationships()
        {
            _buffer.AppendLine();
            _buffer.AppendLine();

            foreach (var relationship in _context.Relationships)
            {
                WriteClassRelationship(relationship);
            }
        }

        private void WriteRootClasses(Dictionary<string, List<string>> packageMap, List<string> unpackaged)
        {
            foreach (var rootClass in _diagram.RootClasses.InnerList)
            {
                string classDefinition = WriteClassDefinition(rootClass);

                AssignToPackage(rootClass, packageMap, classDefinition, unpackaged);
            }
        }

        private void WriteTitle(string title)
        {
            if (!title.IsNullOrEmpty())
            {
                _buffer.AppendLine("title {0}".FormatWith(title));
            }
        }

        #endregion
    }
}