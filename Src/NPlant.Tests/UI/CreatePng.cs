// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="Class1.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using NPlant.UI;

using NUnit.Framework;

namespace NPlant.Tests.UI
{
    [TestFixture]
    public class CreatePng
    {
        #region Public Methods and Operators

        [Test]
        public void Can_Load_Dll_File_Path()
        {
            var args = CommandLineArguments.Load(new[] { "Foo.dll" });
            Assert.That(args.FilePath, Is.EqualTo("Foo.dll"));
            Assert.That(args.FilePath.IsNPlantFilePath(), Is.False);
            Assert.That(args.FilePath.IsAssemblyFilePath(), Is.True);
        }

        #endregion
    }
}