﻿namespace NPlant.MetaModel.ClassDiagramming
{
    public class ClassDiagramRelationship
    {
        public ClassDiagramRelationship(string name, ClassDescriptor party1, ClassDescriptor party2, ClassDiagramRelationshipTypes relationshipType)
        {
            Name = name;
            Party1 = party1;
            Party2 = party2;
            RelationshipType = relationshipType;
        }

        public string Name { get; private set; }

        public ClassDescriptor Party1 { get; private set; }

        public ClassDescriptor Party2 { get; private set; }

        public ClassDiagramRelationshipTypes RelationshipType { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj is not ClassDiagramRelationship relationship)
                return false;

            return object.Equals(relationship.Party1, this.Party1) &&
                   object.Equals(relationship.Party2, this.Party2) &&
                   string.Equals(relationship.Name, this.Name);
        }

        public override int GetHashCode()
        {
            return Party1.GetHashCode() + Party2.GetHashCode() + Name.GetHashCode();
        }
    }

    public enum ClassDiagramRelationshipTypes
    {
        HasA,
        HasMany,
        Base,
        Dependency
    }
}