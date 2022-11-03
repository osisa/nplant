// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="NPlantRunnerOptions.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.Generation
{
    public interface INPlantRunnerOptions
    {
        #region Public Properties

        string AssemblyToScan { get; set; }

        string Categorize { get; set; }

        string Clean { get; set; }

        string JavaPath { get; set; }

        string OutputDirectory { get; set; }

        NPlantCategorizations ParsedCategorized { get; set; }

        string PlantUml { get; set; }

        #endregion
    }

    public enum NPlantCategorizations
    {
        ByNamespace
    }
}