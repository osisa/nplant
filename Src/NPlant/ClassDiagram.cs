// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

using NPlant.Core;
using NPlant.Generation.ClassDiagramming;
using NPlant.MetaModel.ClassDiagramming;

namespace NPlant
{
    public class ClassDiagram
    {
        #region Fields

        private readonly KeyedList<AssemblyDescriptor> _assemblyDescriptors = new();

        private readonly List<ClassDiagramNote> _notes = new();

        private readonly List<ClassDiagramPackage> _packages = new();

        #endregion

        #region Constructors and Destructors

        public ClassDiagram(IEnumerable<Type> types)
            : this()
        {
            if (types != null)
            {
                foreach (var t in types)
                    this.AddClass(t.GetReflected());
            }
        }

        public ClassDiagram(Type type, params Type[] types)
            : this(types)
        {
            type.CheckForNullArg("type");

            this.AddClass(type.GetReflected());
        }

        public ClassDiagram()
        {
            GenerationOptions = new ClassDiagramOptions(this);
            Name = this.GetType().Name;
        }

        #endregion

        #region Public Properties

        public int? DepthLimit { get; internal set; }

        public ClassDiagramOptions GenerationOptions { get; }

        public string Name { get; private set; }

        public KeyedList<ClassDescriptor> RootClasses { get; } = new();

        public string Title { get; private set; }

        #endregion

        #region Properties

        internal ClassDiagramLegend Legend { get; private set; }

        internal IEnumerable<ClassDiagramNote> Notes => _notes;

        internal IEnumerable<ClassDiagramPackage> Packages => _packages;

        internal ClassDiagramScanModes ScanMode { get; set; }

        internal bool ShowMembers { get; set; }

        internal BindingFlags ShowMembersBindingFlags { get; set; }

        internal bool ShowMethods { get; set; }

        internal BindingFlags ShowMethodsBindingFlags { get; set; }

        internal TypeMetaModelSet Types { get; } = new();

        #endregion

        #region Public Methods and Operators

        public void AddClass(ClassDescriptor descriptor, bool addAssembly = true)
        {
            RootClasses.Add(descriptor.CheckForNullArg("descriptor"));

            if (addAssembly)
                AddAssembly(descriptor.ReflectedType.Assembly);
        }

        public ClassDiagramNote AddNote(string line)
        {
            var note = new ClassDiagramNote(line, this);

            _notes.Add(note);

            return note;
        }

        public ClassDiagramLegend LegendOf(string legend)
        {
            Legend = new ClassDiagramLegend(this, legend);
            return Legend;
        }

        public ClassDiagram Named(string name)
        {
            Name = name;

            return this;
        }

        public ClassDiagram Titled(string title)
        {
            Title = title;

            return this;
        }

        #endregion

        #region Methods

        internal void AddReflectedClass(int level, Type type)
        {
            var descriptor = type.GetReflected();

            descriptor.SetLevel(level);
            this.AddClass(descriptor);
        }

        internal IDiagramFormatter CreateFormatter(ClassDiagramVisitorContext context)
        {
            return new ClassDiagramFormatter(this, context);
        }

        internal string GetClassColor(ClassDescriptor @class)
        {
            return null;
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

        protected ClassDiagram AddAssembly(Assembly assembly)
        {
            _assemblyDescriptors.Add(new AssemblyDescriptor(assembly));
            return this;
        }

        protected ClassDiagram AddAssemblyOf<T>()
        {
            return AddAssembly(typeof(T).Assembly);
        }

        protected RootClassDescriptor<T> AddClass<T>()
        {
            var classDescriptor = new RootClassDescriptor<T>();

            this.AddClass(classDescriptor);
            RootClasses.Add(classDescriptor);

            return classDescriptor;
        }

        protected RootEnumDescriptor AddEnum<T>()
        {
            var classDescriptor = new RootEnumDescriptor(typeof(T));

            this.AddClass(classDescriptor);
            RootClasses.Add(classDescriptor);

            return classDescriptor;
        }

        protected ClassDiagramPackage AddPackage(string packageName)
        {
            packageName.CheckForNullOrEmptyArg("packageName");

            var package = new ClassDiagramPackage(packageName, this);
            _packages.Add(package);

            return package;
        }

        #endregion
    }
}