// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="Class1.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Text;

using NUnit.Framework;
using PlantUml.Net;

namespace NPlant.Tests.UI
{
    [TestFixture]
    public class CreatePng
    {
        #region Public Methods and Operators

        [Test]
        public void TryCreatePng()
        {
            var factory = new RendererFactory();
            var renderer = factory.CreateRenderer(new PlantUmlSettings());

            var bytes = renderer.RenderAsync("Bob -> Alice : Hello", OutputFormat.Png).Result;
            File.WriteAllBytes(@"c:\png\out.png", bytes);
        }

        [Test]
        public void SomeSystem()
        {
            var factory = new RendererFactory();
            var settings = new PlantUmlSettings();
            settings.RenderingMode = RenderingMode.Local;
            var renderer = factory.CreateRenderer(settings);
            
            var txt = new StringBuilder();
            txt.AppendLine("skin rose");
            txt.AppendLine("nBob -> Alice : Hello");

            var bytes = renderer.RenderAsync(txt.ToString(), OutputFormat.Png).Result;
            File.WriteAllBytes(@"c:\png\SomeSystem.png", bytes);
        }

        #endregion
    }
}