using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagramming
{
    public interface IDescriptorWriter
    {
        string Write(ClassDiagramVisitorContext context);
    }
}