// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="NPlantRunner.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

using NPlant.Generation.ClassDiagramming;

namespace NPlant.Generation
{
    public class NPlantRunner
    {
        #region Fields

        private readonly INPlantRunnerOptions _options;

        private readonly Func<IRunnerRecorder> _recorder;

        #endregion

        #region Constructors and Destructors

        public NPlantRunner(INPlantRunnerOptions options)
            : this(options, () => NullRecorder.Instance)
        {
        }

        public NPlantRunner(INPlantRunnerOptions options, Func<IRunnerRecorder> recorder)
        {
            _recorder = recorder;
            _options = options;
        }

        #endregion

        #region Public Methods and Operators

        public void Run()
        {
            using (var recorder = _recorder())
            {
                recorder.Log("NPlantRunner Started...");
                recorder.Log(SummarizeConfiguration());

                var loader = new NPlantAssemblyLoader(recorder);
                var assembly = loader.Load(_options.AssemblyToScan);

                var diagramLoader = new NPlantDiagramLoader(recorder);
                var diagrams = diagramLoader.Load(assembly);
                var outputDirectory = RunInitializeOutputDirectoryStage();

                RunGenerateDiagramImagesStage(outputDirectory, diagrams, recorder);

                recorder.Log("NPlantRunner Finished...");
            }
        }

        #endregion

        #region Methods

        private string Categorize(DiscoveredDiagram diagram, string dir)
        {
            if (_options.ParsedCategorized == NPlantCategorizations.ByNamespace)
            {
                var ns = diagram.Namespace;

                if (!string.IsNullOrEmpty(ns))
                {
                    dir = Path.Combine(dir, ns);
                }
            }

            return dir;
        }

        private void RunGenerateDiagramImagesStage(FileSystemInfo outputDirectory, IEnumerable<DiscoveredDiagram> diagrams, IRunnerRecorder recorder)
        {
            recorder.Log("Starting Stage: Diagram Rendering (output={0})...".FormatWith(outputDirectory.FullName));

            foreach (var diagram in diagrams)
            {
                var text = BufferedClassDiagramGenerator.GetDiagramText(diagram.Diagram);
                var javaPath = _options.JavaPath ?? "java.exe";
                var plantUml = _options.PlantUml ?? Assembly.GetExecutingAssembly().Location;

                var npImage = new NPlantImage(javaPath, new PlantUmlInvocation(plantUml))
                              {
                                  Logger = recorder.Log
                              };

                var image = npImage.Create(text, diagram.Diagram.Name);

                if (image != null)
                {
                    var dir = outputDirectory.FullName;

                    dir = Categorize(diagram, dir);
                    var fileName = diagram.Diagram.Name.ReplaceIllegalPathCharacters('_');

                    image.SaveNPlantImage(dir, fileName);
                }
            }

            recorder.Log("Finished Stage: Diagram Rendering...");
        }

        private DirectoryInfo RunInitializeOutputDirectoryStage()
        {
            var outputDirectory = new DirectoryInfo(_options.OutputDirectory.IfIsNullOrEmpty("."));

            outputDirectory.EnsureExists();

            if (this.ShouldClean())
            {
                var files = outputDirectory.GetFiles("*.*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    file.Delete();
                }
            }

            return outputDirectory;
        }

        private bool ShouldClean()
            => !_options.Clean.IsNullOrEmpty() &&
               _options.Clean.ToBool();
        
        private string SummarizeConfiguration()
        {
            var summary = new StringBuilder();

            summary.AppendLine("Task Attributes:");

            IEnumerable<PropertyInfo> properties = _options.GetType().GetProperties();

            foreach (var property in properties)
            {
                summary.AppendLine("    [{0}]: {1}".FormatWith(property.Name, property.GetValue(_options, null)));
            }

            summary.AppendLine();

            return summary.ToString();
        }

        #endregion
    }
}