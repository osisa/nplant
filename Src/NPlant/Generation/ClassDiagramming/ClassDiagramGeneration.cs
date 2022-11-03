using System;

namespace NPlant.Generation.ClassDiagramming
{
    public class ClassDiagramGeneration : IDisposable
    {
        private readonly ClassDiagramVisitorContext _previousVisitorContext;

        public ClassDiagramGeneration(ClassDiagram diagram) : this(new ClassDiagramVisitorContext(diagram))
        {
        }

        public ClassDiagramGeneration(ClassDiagramVisitorContext context)
        {
            _previousVisitorContext = ClassDiagramVisitorContext.Current;
            ClassDiagramVisitorContext.Current = context;
        }

        public void Dispose()
        {
            ClassDiagramVisitorContext.Current = _previousVisitorContext;
        }
    }
}