// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="NPlantException.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace NPlant.Core
{
    [Serializable]
    public class NPlantException : Exception
    {
        #region Constructors and Destructors

        public NPlantException()
        {
        }

        public NPlantException(string message)
            : base(message)
        {
        }

        public NPlantException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected NPlantException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}