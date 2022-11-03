// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="CommandLineArgsFixture.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.Console;

using NUnit.Framework;

namespace NPlant.Tests.Console
{
    [TestFixture]
    public class CommandLineArgsFixture
    {
        #region Public Methods and Operators

        [Test]
        public void No_Args_Should_Do_Nothing()
        {
            var args = new CommandLineArgs(new string[] { "--assembly:Foo.dll" });
            Assert.That(args, Is.Not.Null);
        }

        #endregion
    }
}