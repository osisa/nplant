// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="BufferedClassDiagramGenerator.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Text;

namespace NPlant.Generation.ClassDiagramming
{
    public class BufferedClassDiagramGenerator : ClassDiagramGenerator
    {
        #region Fields

        private readonly StringBuilder _buffer;

        #endregion

        #region Constructors and Destructors

        public BufferedClassDiagramGenerator(ClassDiagram definition, StringBuilder buffer)
            : base(definition)
        {
            _buffer = buffer;
        }

        #endregion

        #region Public Methods and Operators

        public static string GetDiagramText(ClassDiagram diagram)
        {
            var buffer = new StringBuilder();
            var generator = new BufferedClassDiagramGenerator(diagram, buffer);

            generator.Generate();

            return buffer.ToString();
        }

        #endregion

        #region Methods

        protected override void Finalize(ClassDiagramVisitorContext current)
        {
            var formatter = Definition.CreateFormatter(ClassDiagramVisitorContext.Current);
            _buffer.Append(formatter.Format());
        }

        #endregion
    }
}