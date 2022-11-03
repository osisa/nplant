﻿using System;
using System.Collections.Generic;
using System.Reflection;
using NPlant.Core;
using NPlant.Generation.ClassDiagramming;
using NPlant.MetaModel.ClassDiagramming;

namespace NPlant
{
    public class ClassDiagram
    {
        private readonly TypeMetaModelSet _types = new TypeMetaModelSet();
        private string _name;
        private string _title;
        private readonly KeyedList<AssemblyDescriptor> _assemblyDescriptors = new KeyedList<AssemblyDescriptor>();
        private readonly KeyedList<ClassDescriptor> _classDescriptors = new KeyedList<ClassDescriptor>();
        private readonly ClassDiagramOptions _generationOptions;
        private ClassDiagramLegend _legend;
        private readonly List<ClassDiagramNote> _notes = new List<ClassDiagramNote>();
        private readonly List<ClassDiagramPackage> _packages = new List<ClassDiagramPackage>();

        public ClassDiagram(IEnumerable<Type> types) : this()
        {
            if (types != null)
            {
                foreach (var t in types)
                    this.AddClass(t.GetReflected());
            }
        }

        public ClassDiagram(Type type, params Type[] types) : this(types)
        {
            type.CheckForNullArg("type");

            this.AddClass(type.GetReflected());
        }

        public ClassDiagram()
        {
            _generationOptions = new ClassDiagramOptions(this);
            _name = this.GetType().Name;
        }

        internal TypeMetaModelSet Types { get { return _types; } }

        protected RootClassDescriptor<T> AddClass<T>()
        {
            var classDescriptor = new RootClassDescriptor<T>();

            this.AddClass(classDescriptor);
            _classDescriptors.Add(classDescriptor);

            return classDescriptor;
        }

        protected RootEnumDescriptor AddEnum<T>()
        {
            var classDescriptor = new RootEnumDescriptor(typeof(T));

            this.AddClass(classDescriptor);
            _classDescriptors.Add(classDescriptor);

            return classDescriptor;
        }

        protected ClassDiagram AddAssemblyOf<T>()
        {
            return AddAssembly(typeof(T).Assembly);
        }

        protected ClassDiagram AddAssembly(Assembly assembly)
        {
            _assemblyDescriptors.Add(new AssemblyDescriptor(assembly));
            return this;
        }

        protected ClassDiagram AddAllSubClassesOff<T>()
        {
            this.AddAssemblyOf<T>();

            foreach (var assembly in _assemblyDescriptors.InnerList)
            {
                var types = assembly.Assembly.GetTypesExtending<T>();

                foreach (var type in types)
                    this.AddClass(type.GetReflected(), false);
            }

            return this;
        }

        internal void AddReflectedClass(int level, Type type)
        {
            var descriptor = type.GetReflected();

            descriptor.SetLevel(level);
            this.AddClass(descriptor);
        }

        public void AddClass(ClassDescriptor descriptor, bool addAssembly = true)
        {
            _classDescriptors.Add(descriptor.CheckForNullArg("descriptor"));

            if(addAssembly)
                AddAssembly(descriptor.ReflectedType.Assembly);
        }

        public KeyedList<ClassDescriptor> RootClasses { get { return _classDescriptors; } }

        internal IDiagramFormatter CreateFormatter(ClassDiagramVisitorContext context)
        {
            return new ClassDiagramFormatter(this, context);
        }

        public string Name { get { return _name; } }

        public ClassDiagram Named(string name)
        {
            _name = name;

            return this;
        }

        public string Title { get { return _title; } }

        public ClassDiagram Titled(string title)
        {
            _title = title;

            return this;
        }

        public ClassDiagramOptions GenerationOptions
        {
            get { return _generationOptions; }
        }

        public int? DepthLimit { get; internal set; }

        public ClassDiagramLegend LegendOf(string legend)
        {
            _legend = new ClassDiagramLegend(this, legend);
            return _legend;
        }

        internal ClassDiagramLegend Legend { get { return _legend; } }

        internal IEnumerable<ClassDiagramNote> Notes
        {
            get { return _notes; }
        }

        public ClassDiagramNote AddNote(string line)
        {
            var note = new ClassDiagramNote(line, this);

            _notes.Add(note);

            return note;
        }

        internal string GetClassColor(ClassDescriptor @class)
        {
            return null;
        }

        internal IEnumerable<ClassDiagramPackage> Packages
        {
            get { return _packages; }
        }

        internal ClassDiagramScanModes ScanMode { get; set; }

        internal bool ShowMembers { get; set; }

        internal BindingFlags ShowMembersBindingFlags { get; set; }

        internal bool ShowMethods { get; set; }
        
        internal BindingFlags ShowMethodsBindingFlags { get; set; }

        protected ClassDiagramPackage AddPackage(string packageName)
        {
            packageName.CheckForNullOrEmptyArg("packageName");

            var package = new ClassDiagramPackage(packageName, this);
            _packages.Add(package);
            
            return package;
        }
    }
}
