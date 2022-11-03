// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="Program.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using NPlant.Console.Exceptions;
using NPlant.Core;
using NPlant.Generation;
using NPlant.Generation.ClassDiagramming;

using Con = System.Console;

namespace NPlant.Console
{
    public class Program
    {
        #region Methods

        public static int Main(string[] args)
        {
            try
            {
                var arguments = new CommandLineArgs(args);
                var jarPath = arguments.Jar;

                if (jarPath.IsNullOrEmpty())
                    jarPath = PlantUmlJarExtractor.TryExtractTo(ConsoleEnvironment.ExecutionDirectory);

                if (arguments.Debugger)
                {
                    Debugger.Launch();
                    Debugger.Break();
                }

                var assemblyLoader = new NPlantAssemblyLoader();
                var assembly = assemblyLoader.Load(arguments.Assembly);

                var diagramLoader = new NPlantDiagramLoader();
                var diagrams = diagramLoader.Load(assembly);

                IEnumerable<DiscoveredDiagram> matchingDiagrams = diagrams;

                if (!arguments.Diagram.IsNullOrEmpty())
                    matchingDiagrams = matchingDiagrams.Where(diagram => diagram.Diagram.Name.StartsWith(arguments.Diagram));

                foreach (var matchingDiagram in matchingDiagrams)
                {
                    if (arguments.Output.IsNullOrEmpty())
                    {
                        Con.WriteLine("    {0}", matchingDiagram.Diagram.Name);
                    }
                    else
                    {
                        var diagramText = BufferedClassDiagramGenerator.GetDiagramText(matchingDiagram.Diagram);
                        var model = new ImageFileGenerationModel(diagramText, matchingDiagram.Diagram.Name, arguments.Java, jarPath);

                        var outputDirectory = new DirectoryInfo(arguments.Output);

                        if (!outputDirectory.Exists)
                            outputDirectory.Create();

                        var path = Path.Combine(outputDirectory.FullName, $"{model.DiagramName}.{arguments.Format}");
                        var format = arguments.GetImageFormat();

                        if (format == null)
                        {
                            File.WriteAllText(path, diagramText);
                        }
                        else
                        {
                            var plantImage = new NPlantImage(model.JavaPath, model.Invocation);
                            var image = plantImage.Create(model.DiagramText, model.DiagramName);
                            image.Save(path, format);
                        }
                    }
                }

                return 0;
            }
            catch (ConsoleUsageException usageException)
            {
                Con.WriteLine("Fatal Error:");
                Con.WriteLine(usageException.Message);
                Con.WriteLine();
                Con.WriteLine("NPlant.Console.exe Usage");
                Con.WriteLine("------------------------");
            }
            catch (Exception consoleException)
            {
                Con.WriteLine("Fatal Error:");
                Con.WriteLine(consoleException);
            }

            return 1;
        }

        #endregion
    }
}