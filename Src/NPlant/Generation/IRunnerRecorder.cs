// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="IRunnerRecorder.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Generation
{
    public interface IRunnerRecorder : IDisposable
    {
        #region Public Methods and Operators

        void Log(string message);

        void Record(string filePath);

        #endregion
    }
}