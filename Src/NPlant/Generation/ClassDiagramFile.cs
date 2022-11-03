// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDiagramFile.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.IO;

using NPlant.Generation.ClassDiagramming;

namespace NPlant.Generation
{
    public static class ClassDiagramFile
    {
        #region Public Methods and Operators

        public static string Save(string outputDirectory, ClassDiagram diagram, IRunnerRecorder recorder)
        {
            var filePath = Path.Combine(outputDirectory, "{0}.nplant".FormatWith(diagram.Name.ReplaceIllegalPathCharacters('_')));

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var file = File.CreateText(filePath))
            {
                var generator = new FileClassDiagramGenerator(diagram, file);
                generator.Generate();

                recorder.Log("Diagram '{0}' written...".FormatWith(diagram.GetType().FullName));
                recorder.Record(filePath);
            }

            return filePath;
        }

        #endregion
    }
}