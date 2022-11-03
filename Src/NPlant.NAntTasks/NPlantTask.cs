// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="NPlantTask.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NAnt.Core;
using NAnt.Core.Attributes;

using NPlant.Core;
using NPlant.Generation;

namespace NPlant.NAntTasks
{
    [TaskName("nplant")]
    public class NPlantTask : Task, INPlantRunnerOptions
    {
        #region Fields

        private string _categorize;

        #endregion

        #region Public Properties

        [TaskAttribute("assembly", Required = true)]
        public string AssemblyToScan { get; set; }

        [TaskAttribute("categorize", Required = false)]
        public string Categorize
        {
            get => _categorize;
            set
            {
                switch (value)
                {
                    case "namespace":
                        this.ParsedCategorized = NPlantCategorizations.ByNamespace;
                        break;
                    default:
                        throw new NPlantException("{0} is not a recognized categorization type.  Must be one of the following: {1}".FormatWith(value, EnumJoiner.Join<NPlantCategorizations>()));
                }

                _categorize = value;
            }
        }

        [TaskAttribute("clean", Required = false)]
        public string Clean { get; set; }

        [TaskAttribute("delim", Required = false)]
        public string Delimiter { get; set; }

        [BuildElement("diagrams", Required = false)]
        public DiagramsElement DiagramsElement { get; set; } = new ();

        [TaskAttribute("java", Required = false)]
        public string JavaPath { get; set; }

        [TaskAttribute("dir", Required = false)]
        public string OutputDirectory { get; set; }

        public NPlantCategorizations ParsedCategorized { get; set; }

        [TaskAttribute("plantuml", Required = false)]
        public string PlantUml { get; set; }

        [TaskAttribute("property", Required = false)]
        public string Property { get; set; }

        #endregion

        #region Methods

        protected override void ExecuteTask()
        {
            var old = Project.Threshold;
            AssignLogLevel(Level.Debug);

            var runner = new NPlantRunner(this, () => new NAntRunnerRecorder(this, Property, Delimiter));
            runner.Run();

            AssignLogLevel(old);
        }

        private void AssignLogLevel(Level newLevel)
        {
            foreach (var listener in Project.BuildListeners)
            {
                if (listener is IBuildLogger logger)
                    logger.Threshold = newLevel;
            }
        }

        #endregion
    }
}