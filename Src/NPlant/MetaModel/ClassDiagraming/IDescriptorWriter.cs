using NPlant.Generation.ClassDiagramming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public interface IDescriptorWriter
    {
        string Write(ClassDiagramVisitorContext context);
    }
}