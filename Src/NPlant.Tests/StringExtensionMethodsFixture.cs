// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="StringExtensionMethodsFixture.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using NUnit.Framework;

namespace NPlant.Tests
{
    [TestFixture]
    public class StringExtensionMethodsFixture
    {
        #region Public Methods and Operators

        [TestCase(typeof(string), "String")]
        [TestCase(typeof(TestSubject1<Arg1>), "TestSubject1<Arg1>")]
        [TestCase(typeof(TestSubject2<Arg1, Arg2>), "TestSubject2<Arg1,Arg2>")]
        [TestCase(typeof(TestSubject3<Arg1, Arg2, Arg3>), "TestSubject3<Arg1,Arg2,Arg3>")]
        public void GetFriendlyGenericName_Suite(Type type, string expected)
        {
            Assert.That(type.GetFriendlyGenericName(), Is.EqualTo(expected));
        }

        [TestCase("abcdefghijk", "g", "abcdef")]
        [TestCase("abcdefghijk", "k", "abcdefghij")]
        [TestCase("abcdefghijk", "a", "")]
        [TestCase("abcdefghijk", "l", "abcdefghijk")]
        [TestCase("abcdefghijk", "", "abcdefghijk")]
        [TestCase("abcdefghijk", null, "abcdefghijk")]
        [TestCase(null, null, null)]
        [TestCase("abcd", null, "abcd")]
        [TestCase(null, "abcd", null)]
        public void SubstringTo_Suit(string source, string to, string expected)
        {
            Assert.That(source.SubstringTo(to), Is.EqualTo(expected));
        }

        #endregion

        public class Arg1
        {
        }

        public class Arg2
        {
        }

        public class Arg3
        {
        }

        public class TestSubject1<T1>
        {
            #region Fields

            public T1 Argument1;

            #endregion
        }

        public class TestSubject2<T1, T2>
        {
            #region Fields

            public T1 Argument1;

            public T2 Argument2;

            #endregion
        }

        public class TestSubject3<T1, T2, T3>
        {
            #region Fields

            public T1 Argument1;

            public T2 Argument2;

            public T3 Argument3;

            #endregion
        }
    }
}