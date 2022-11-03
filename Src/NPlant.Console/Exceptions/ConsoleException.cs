// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ConsoleException.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace NPlant.Console.Exceptions
{
    public class ConsoleException : Exception
    {
        #region Constructors and Destructors

        public ConsoleException(string message)
            : base(message)
        {
        }

        public ConsoleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}