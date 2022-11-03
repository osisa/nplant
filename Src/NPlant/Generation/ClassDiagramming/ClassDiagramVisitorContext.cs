﻿using System;
using System.Collections.Generic;
using System.Reflection;
using NPlant.Core;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagramming
{
    public class ClassDiagramVisitorContext
    {
        private readonly Queue<ClassDescriptor> _unvisitedRelatedClasses = new Queue<ClassDescriptor>();
        private readonly List<ClassDiagramRelationship> _relationships = new List<ClassDiagramRelationship>();
        private readonly List<ClassDescriptor> _visitedRelatedClasses = new List<ClassDescriptor>();

        public static ClassDiagramVisitorContext Current { get; set; }

        protected ClassDiagramVisitorContext() { }

        protected internal ClassDiagramVisitorContext(ClassDiagram diagram)
        {
            this.TypeMetaModelSet = diagram.Types;
            this.Diagram = diagram;
            this.ScanMode = this.Diagram.ScanMode;
            this.ShowMembers = this.Diagram.ShowMembers;
            this.ShowMembersBindingFlags = this.Diagram.ShowMembersBindingFlags;
            this.ShowMethods = this.Diagram.ShowMethods;
            this.ShowMethodsBindingFlags = this.Diagram.ShowMethodsBindingFlags;
        }

        protected TypeMetaModelSet TypeMetaModelSet { get; set; }

        public ClassDiagram Diagram { get; set; }

        public ClassDiagramScanModes ScanMode { get; protected set; }

        public bool ShowMembers { get; protected set; }

        public BindingFlags ShowMembersBindingFlags { get; protected set; }

        public bool ShowMethods { get; protected set; }

        public BindingFlags ShowMethodsBindingFlags { get; protected set; }

        public void AddRelated(ClassDescriptor parent, ClassDescriptor child, ClassDiagramRelationshipTypes relationshipType, int level, string name = null)
        {
            if (! AlreadyRegistered(child))
                _unvisitedRelatedClasses.Enqueue(child);

            var relationship = new ClassDiagramRelationship(name, parent, child, relationshipType);

            if (!_relationships.Contains(relationship))
                _relationships.Add(relationship);
        }

        private bool AlreadyRegistered(ClassDescriptor child)
        {
            if(_visitedRelatedClasses.Contains(child))
                return true;

            if (_unvisitedRelatedClasses.Contains(child))
                return true;
            
            if (this.Diagram.RootClasses.InnerList.Contains(child))
                return true;

            return false;
        }

        public IEnumerable<ClassDescriptor> VisitedRelatedClasses { get { return _visitedRelatedClasses; } }

        public IEnumerable<ClassDiagramRelationship> Relationships { get { return _relationships; } }

        public TypeMetaModel GetTypeMetaModel(Type type)
        {
            return this.TypeMetaModelSet[type];
        }

        public void VisitAllRelatedClasses()
        {
            while (_unvisitedRelatedClasses.Count > 0)
            {
                var descriptor = _unvisitedRelatedClasses.Dequeue();

                descriptor.Visit();

                _visitedRelatedClasses.Add(descriptor);
            }
        }
    }
}