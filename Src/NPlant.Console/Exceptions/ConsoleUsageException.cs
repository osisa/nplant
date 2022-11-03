// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ConsoleUsageException.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Console.Exceptions
{
    public class ConsoleUsageException : Exception
    {
        #region Constructors and Destructors

        public ConsoleUsageException(string message)
            : base(message)
        {
        }

        public ConsoleUsageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}