// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="NAntRunnerRecorder.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;

using NAnt.Core;

using NPlant.Generation;

namespace NPlant.NAntTasks
{
    public class NAntRunnerRecorder : IRunnerRecorder
    {
        #region Fields

        private readonly StringBuilder _builder = new();

        private readonly string _delimiter;

        private readonly string _propertyName;

        private readonly Task _task;

        private int _recordedCount;

        #endregion

        #region Constructors and Destructors

        public NAntRunnerRecorder(Task task, string propertyName, string delimiter)
        {
            _task = task;
            _propertyName = propertyName;
            _delimiter = delimiter.IsNullOrEmpty() ? ";" : delimiter;
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            if (_recordedCount > 0)
            {
                var delimited = _builder.ToString();

                if (!_propertyName.IsNullOrEmpty())
                    _task.Properties[_propertyName] = delimited;

                _task.Log(Level.Debug, "Recording (Count: {0}): {1}".FormatWith(_recordedCount, delimited));
            }

            GC.SuppressFinalize(this);
        }

        public void Log(string message)
        {
            _task.Log(Level.Debug, message);
        }

        public void Record(string filePath)
        {
            if (_recordedCount > 0)
                _builder.Append(_delimiter);

            _builder.Append(string.Concat("\"", filePath, "\""));

            _recordedCount++;
        }

        #endregion
    }
}