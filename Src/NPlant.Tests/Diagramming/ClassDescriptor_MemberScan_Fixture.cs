// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ClassDescriptor_MemberScan_Fixture.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.ServiceModel;

using NPlant.Generation.ClassDiagramming;
using NPlant.MetaModel.ClassDiagramming;
using NPlant.Tests.Diagraming;

using NUnit.Framework;

namespace NPlant.Tests.Diagramming
{
    [TestFixture]
    public class ClassDescriptor_MemberScan_Fixture
    {
        #region Public Methods and Operators

        [TestCase(typeof(PublicMembersOnly), ClassDiagramScanModes.PublicMembersOnly, new string[] { "Foo" })]
        [TestCase(typeof(AllMembers), ClassDiagramScanModes.AllMembers, new[] { "Foo", "Baz", "Moo", "Bar" })]
        [TestCase(typeof(DataContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Baz", "Foo" })]
        [TestCase(typeof(MessageContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Baz", "Foo" })]
        [TestCase(typeof(FieldDataContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Baz","Foo" })]
        [TestCase(typeof(FieldMessageContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Baz", "Foo" })]
        [TestCase(typeof(FieldPropertyHybridDataContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Foo", "Baz" })]
        [TestCase(typeof(FieldPropertyHybridMessageContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Foo", "Baz" })]
        public void Scan_Suite(Type subjectType, ClassDiagramScanModes scanMode, string[] expectations)
        {
            using (new ClassDiagramGeneration(new StubClassDiagramVisitorContext(scanMode)))
            {
                ClassDescriptor descriptor = new ReflectedClassDescriptor(subjectType);
                descriptor.Visit();
                Assert.That(descriptor.Members.Count, Is.EqualTo(expectations.Length));

                for (var index = 0; index < expectations.Length; index++)
                {
                    Assert.That(descriptor.Members[index].Name, Is.EqualTo(expectations[index]));
                }
            }
        }

        #endregion

        public class AllMembers
        {
            #region Fields

            public string Foo;

            internal string Baz = "";

            protected string Moo;

            private string Bar = "";

            #endregion

            #region Public Methods and Operators

            public override string ToString()
            {
                // use Bar to get rid of "Warning as Error ... is never used" errors. 
                return Bar;
            }

            #endregion
        }

        [DataContract]
        public class DataContractSubject
        {
            #region Public Properties

            // should not be scanned in
            public string Bar { get; set; }

            [DataMember] public string Baz { get; set; }

            [DataMember] public string Foo { get; set; }

            #endregion
        }

        [DataContract]
        public class FieldDataContractSubject
        {
            #region Fields

            // should not be scanned in
            public string Bar;

            [DataMember]
            public string Baz;

            [DataMember]
            public string Foo;

            #endregion
        }

        [MessageContract]
        public class FieldMessageContractSubject
        {
            #region Fields

            // should not be scanned in
            public string Bar;

            [MessageBodyMember]
            public string Baz;

            [MessageBodyMember]
            public string Foo;

            #endregion
        }

        [DataContract]
        public class FieldPropertyHybridDataContractSubject
        {
            #region Fields

            // should not be scanned in
            public string Bar;

            [DataMember]
            public string Foo;

            #endregion

            #region Public Properties

            [DataMember] public string Baz { get; set; }

            #endregion
        }

        [MessageContract]
        public class FieldPropertyHybridMessageContractSubject
        {
            #region Fields

            // should not be scanned in
            public string Bar;

            [MessageBodyMember]
            public string Foo;

            #endregion

            #region Public Properties

            [MessageBodyMember] public string Baz { get; set; }

            #endregion
        }

        [MessageContract]
        public class MessageContractSubject
        {
            #region Public Properties

            // should not be scanned in
            public string Bar { get; set; }

            [MessageBodyMember] public string Baz { get; set; }

            [MessageBodyMember] public string Foo { get; set; }

            #endregion
        }

        public class PublicMembersOnly
        {
            #region Fields

            public string Foo;

            // should not be scanned in
            internal string Baz = "";

            // should not be scanned in
            protected string Moo;

            // should not be scanned in
            private string Bar = "";

            #endregion

            #region Public Methods and Operators

            public override string ToString()
            {
                // use Bar to get rid of "Warning as Error ... is never used" error. 
                return Bar;
            }

            #endregion
        }
    }
}