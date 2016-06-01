using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;
using AdtGekid;

namespace AdtGekid.Tests
{
    public class JnuTypTests
    {
        [Fact]
        public void ImplicitCastToString_Test()
        {
            testStringCast("J", JnuTyp.Ja);
            testStringCast("N", JnuTyp.Nein);
            testStringCast("U", JnuTyp.Unbekannt);
            testStringCast(null, new JnuTyp());
        }

        [Fact]
        public void CodeProperty_Test()
        {
            testCodeProperty("J", JnuTyp.Ja);
            testCodeProperty("N", JnuTyp.Nein);
            testCodeProperty("U", JnuTyp.Unbekannt);
            testCodeProperty(null, new JnuTyp());
        }

        [Fact]
        public void StaticEquals_Negative_Test()
        {
            testStaticEqualsNeg(JnuTyp.Ja, JnuTyp.Nein);
            testStaticEqualsNeg(JnuTyp.Ja, JnuTyp.Unbekannt);
            testStaticEqualsNeg(JnuTyp.Ja, new JnuTyp());
            testStaticEqualsNeg(JnuTyp.Nein, JnuTyp.Unbekannt);
            testStaticEqualsNeg(JnuTyp.Nein, new JnuTyp());
            testStaticEqualsNeg(JnuTyp.Unbekannt, new JnuTyp());
        }

        [Fact]
        public void InstanceEquals_Negative_Test()
        {
            testStaticEqualsNeg(JnuTyp.Ja, JnuTyp.Nein);
            testStaticEqualsNeg(JnuTyp.Ja, JnuTyp.Unbekannt);
            testStaticEqualsNeg(JnuTyp.Ja, new JnuTyp());
            testStaticEqualsNeg(JnuTyp.Nein, JnuTyp.Unbekannt);
            testStaticEqualsNeg(JnuTyp.Nein, new JnuTyp());
            testStaticEqualsNeg(JnuTyp.Unbekannt, new JnuTyp());
        }

        [Fact]
        public void EqualityOperator_Negative_Test()
        {
            testStaticEqualsNeg(JnuTyp.Ja, JnuTyp.Nein);
            testStaticEqualsNeg(JnuTyp.Ja, JnuTyp.Unbekannt);
            testStaticEqualsNeg(JnuTyp.Ja, new JnuTyp());
            testStaticEqualsNeg(JnuTyp.Nein, JnuTyp.Unbekannt);
            testStaticEqualsNeg(JnuTyp.Nein, new JnuTyp());
            testStaticEqualsNeg(JnuTyp.Unbekannt, new JnuTyp());
        }

        [Fact]
        public void StaticEquals_Test()
        {
            testStaticEquals(JnuTyp.Ja, JnuTyp.Ja);
            testStaticEquals(JnuTyp.Nein, JnuTyp.Nein);
            testStaticEquals(JnuTyp.Unbekannt, JnuTyp.Unbekannt);
            testStaticEquals(new JnuTyp(), new JnuTyp());
        }

        [Fact]
        public void InstanceEquals_Test()
        {
            testInstanceEquals(JnuTyp.Ja, JnuTyp.Ja);
            testInstanceEquals(JnuTyp.Nein, JnuTyp.Nein);
            testInstanceEquals(JnuTyp.Unbekannt, JnuTyp.Unbekannt);
            testInstanceEquals(new JnuTyp(), new JnuTyp());
        }

        [Fact]
        public void EqualityOperator_Test()
        {
            testEqualityOperator(JnuTyp.Ja, JnuTyp.Ja);
            testEqualityOperator(JnuTyp.Nein, JnuTyp.Nein);
            testEqualityOperator(JnuTyp.Unbekannt, JnuTyp.Unbekannt);
            testEqualityOperator(new JnuTyp(), new JnuTyp());
        }

        private static void testStaticEquals(JnuTyp j1, JnuTyp j2)
        {
            Assert.True(JnuTyp.Equals(j1, j2), String.Format("JnuTyp.Equals(JnuTyp.{0}, JnuTyp.{1}) schlug fehl.", j1.ToString(), j2.ToString()));
            Assert.True(JnuTyp.Equals(j2, j1), String.Format("JnuTyp.Equals(JnuTyp.{1}, JnuTyp.{0}) schlug fehl, obwohl der umgekehrte Fall TRUE ergab.", j1.ToString(), j2.ToString()));
        }

        private static void testInstanceEquals(JnuTyp j1, JnuTyp j2)
        {
            Assert.True(j1.Equals(j2), String.Format("JnuTyp.{0}.Equals(JnuTyp.{1}) schlug fehl.", j1.ToString(), j2.ToString()));
            Assert.True(j2.Equals(j1), String.Format("JnuTyp.{1}.Equals(JnuTyp.{0}) schlug fehl, obwohl der umgekehrte Fall TRUE ergab.", j1.ToString(), j2.ToString()));
        }

        private static void testEqualityOperator(JnuTyp j1, JnuTyp j2)
        {
            Assert.True(j1 == j2, String.Format("JnuTyp.{0}==JnuTyp.{1} schlug fehl.", j1.ToString(), j2.ToString()));
            Assert.True(j2 == j1, String.Format("JnuTyp.{1}==JnuTyp.{0} schlug fehl, obwohl der umgekehrte Fall TRUE ergab.", j1.ToString(), j2.ToString()));
        }

        private static void testStaticEqualsNeg(JnuTyp j1, JnuTyp j2)
        {
            Assert.False(JnuTyp.Equals(j1, j2), String.Format("JnuTyp.Equals(JnuTyp.{0}, JnuTyp.{1}) ergab TRUE.", j1.ToString(), j2.ToString()));
            Assert.False(JnuTyp.Equals(j2, j1), String.Format("JnuTyp.Equals(JnuTyp.{1}, JnuTyp.{0}) ergab TRUE, obwohl der umgekehrte Fall FALSE ergab.", j1.ToString(), j2.ToString()));
        }

        private static void testInstanceEqualsNeg(JnuTyp j1, JnuTyp j2)
        {
            Assert.False(j1.Equals(j2), String.Format("JnuTyp.{0}.Equals(JnuTyp.{1}) ergab TRUE.", j1.ToString(), j2.ToString()));
            Assert.False(j2.Equals(j1), String.Format("JnuTyp.{1}.Equals(JnuTyp.{0}) ergab TRUE, obwohl der umgekehrte Fall FALSE ergab.", j1.ToString(), j2.ToString()));
        }

        private static void testEqualityOperatorNeg(JnuTyp j1, JnuTyp j2)
        {
            Assert.False(j1==j2, String.Format("JnuTyp.{0}==JnuTyp.{1} ergab TRUE.", j1.ToString(), j2.ToString()));
            Assert.False(j2==j1, String.Format("JnuTyp.{1}==JnuTyp.{0} ergab TRUE, obwohl der umgekehrte Fall FALSE ergab.", j1.ToString(), j2.ToString()));
        }

        private static void testCodeProperty(string expectedCode, JnuTyp jnu)
        {
            Assert.Equal(expectedCode, jnu.Code);
        }

        private static void testStringCast(string expectedCode, JnuTyp jnu)
        {
            string actualCode = jnu;
            Assert.Equal(expectedCode, actualCode);
        }
    }
}
