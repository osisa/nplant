// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="NPlantDiagramLoader.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NPlant.Core;

namespace NPlant.Generation
{
    public class NPlantDiagramLoader
    {
        #region Static Fields

        private static readonly Type _diagramFactoryInterface = typeof(IDiagramFactory);

        private static readonly Type _diagramInterface = typeof(ClassDiagram);

        #endregion

        #region Fields

        private readonly IRunnerRecorder _recorder = NullRecorder.Instance;

        #endregion

        #region Constructors and Destructors

        public NPlantDiagramLoader()
        {
        }

        public NPlantDiagramLoader(IRunnerRecorder recorder)
        {
            _recorder = recorder;
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerable<DiscoveredDiagram> Load(Assembly assembly)
        {
            return Load(assembly, type => true);
        }

        private IEnumerable<DiscoveredDiagram> Load(Assembly assembly, Func<Type, bool> matcher)
        {
            _recorder.Log("Starting Stage: Diagram Instantiation...");

            var diagrams = LoadFromAssembly(assembly, _recorder.Log);

            _recorder.Log("Finished Stage: Diagram Instantiation (diagrams instantiated={0})...".FormatWith(diagrams.Length));

            return diagrams;
        }

        #endregion

        #region Methods

        private static DiscoveredDiagram InstantiateDiagram(Type exportedType)
        {
            if (!exportedType.TryGetPublicParameterlessConstructor(out var ctor))
                throw new NPlantException("Diagrams are expected to have a public parameterless constructor.  '{0}' does not meet this expectation.".FormatWith(exportedType.FullName));

            return new DiscoveredDiagram(exportedType.Namespace, (ClassDiagram)ctor.Invoke(new object[0]));
        }

        private static IDiagramFactory InstantiateDiagramFactory(Type exportedType)
        {
            if (!exportedType.TryGetPublicParameterlessConstructor(out var ctor))
                throw new NPlantException("ClassDiagramFactory's are expected to have a public parameterless constructor.  '{0}' does not meet this expectation.".FormatWith(exportedType.FullName));

            return (IDiagramFactory)ctor.Invoke(new object[0]);
        }

        private static DiscoveredDiagram[] LoadFromAssembly(Assembly assembly, Action<string> logger = null)
        {
            logger ??= (msg) => { };

            if (assembly == null)
            {
                logger("Assembly was null");

                return new DiscoveredDiagram[0];
            }

            var diagrams = new List<DiscoveredDiagram>();

            var exportedTypes = assembly.GetExportedTypes().Where(x => !x.HasAttribute<HideDiagramAttribute>()).ToArray();

            logger("ExportedTypes Count from '{0}':  {1}".FormatWith(assembly.FullName, exportedTypes.Length));

            foreach (var exportedType in exportedTypes)
            {
                if (!exportedType.IsAbstract)
                {
                    if (_diagramInterface.IsAssignableFrom(exportedType))
                    {
                        logger("ExportedType '{0}' found to be assignable to {1}.".FormatWith(exportedType.FullName, _diagramInterface.FullName));

                        diagrams.Add(InstantiateDiagram(exportedType));
                    }
                    else if (_diagramFactoryInterface.IsAssignableFrom(exportedType))
                    {
                        logger("ExportedType '{0}' found to be assignable to {1}.".FormatWith(exportedType.FullName, _diagramFactoryInterface.FullName));

                        var factory = InstantiateDiagramFactory(exportedType);

                        var classDiagrams = factory.GetDiagrams();

                        if (classDiagrams != null)
                            diagrams.AddRange(classDiagrams.Select(d => new DiscoveredDiagram(exportedType.Namespace, d)));
                    }
                }
            }

            return diagrams.ToArray();
        }

        #endregion
    }
}