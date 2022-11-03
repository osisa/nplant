// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="NullRecorder.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace NPlant.Generation
{
    public class NullRecorder : IRunnerRecorder
    {
        #region Static Fields

        public static readonly IRunnerRecorder Instance = new NullRecorder();

        #endregion

        #region Constructors and Destructors

        private NullRecorder()
        {
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public void Log(string message)
        {
        }

        public void Record(string filePath)
        {
        }

        #endregion
    }
}