using NPlant.Generation.ClassDiagramming;

namespace NPlant.Generation
{
    public interface IBuilder
    {
        void Build(ClassDiagramVisitorContext context);
    }
}