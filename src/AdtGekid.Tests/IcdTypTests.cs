using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;
using AdtGekid;

namespace AdtGekid.Tests
{
    public class IcdTypTests
    {
        [Theory]
        [InlineData("C80.0")]
        [InlineData("D13.2")]
        [InlineData("C79.88")]
        [InlineData("C20")]
        [InlineData("c80.0")]
        [InlineData("d13.2")]
        [InlineData("c79.88  ")]
        [InlineData("  c20 ")]
        public void Constructor_Test(string value)
        {
            var actual = new IcdTyp(value).ToString();
            var expected = value.Trim().ToUpper();
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("C80.0")]
        [InlineData("D13.2")]
        [InlineData("C79.88")]
        [InlineData("C20")]
        [InlineData("c80.0")]
        [InlineData("d13.2")]
        [InlineData("c79.88  ")]
        [InlineData("  c20 ")]
        public void ImplicitConversion_Test(string value)
        {
            IcdTyp icdObj = value;
            var actual = icdObj.ToString();
            var expected = value.Trim().ToUpper();

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("Z12.1")]
        [InlineData("C80 .0")]
        [InlineData("C80.0/3")]
        [InlineData("C80-0")]
        [InlineData("C80:0")]
        [InlineData("D81.2y")]
        [InlineData("A00.1")]
        [InlineData("B21.1")]
        [InlineData("C200")]
        [InlineData("C20.999")]
        public void ImplicitConversion_Negative_Test(string value)
        {
            IcdTyp icdObj;
            Assert.ThrowsDelegate delgt = () => icdObj = value;
            Assert.Throws<ArgumentException>(delgt);
        }

        [Fact]
        public void StaticEquals_Test()
        {
            var code = "C80.9";
            var o1 = new IcdTyp(code);
            var o2 = new IcdTyp(code);

            Assert.True(IcdTyp.Equals(o1, o2), "IcdTyp.Equals(x, y) für \"C80.9\" und \"C80.9\" schlug fehl.");

            o2 = new IcdTyp(code.ToLower());
            Assert.True(IcdTyp.Equals(o1, o2), "IcdTyp.Equals(x, y) für \"C80.9\" und \"c80.9\" schlug fehl.");


            o2 = new IcdTyp("C79.88");
            Assert.False(IcdTyp.Equals(o1, o2), "IcdTyp.Equals(x, y) für \"C80.9\" und \"C79.88\" ergab TRUE.");
        }

        [Fact]
        public void InstanceEquals_Test()
        {
            var code = "C80.9";
            var o1 = new IcdTyp(code);
            var o2 = new IcdTyp(code);

            Assert.True(o1.Equals(o2), "IcdTyp.Equals(x, y) für \"C80.9\" und \"C80.9\" schlug fehl.");

            o2 = new IcdTyp(code.ToLower());
            Assert.True(o1.Equals(o2), "IcdTyp.Equals(x, y) für \"C80.9\" und \"c80.9\" schlug fehl.");


            o2 = new IcdTyp("C79.88");
            Assert.False(o1.Equals(o2), "IcdTyp.Equals(x, y) für \"C80.9\" und \"C79.88\" ergab TRUE.");
        }


        [Fact]
        public void EqualityOperator_Test()
        {
            var code = "C80.9";
            var o1 = new IcdTyp(code);
            var o2 = new IcdTyp(code);

            Assert.True(o1==o2, "== Operator für \"C80.9\" und \"C80.9\" schlug fehl.");

            o2 = new IcdTyp(code.ToLower());
            Assert.True(o1 == o2, "== Operator für \"C80.9\" und \"c80.9\" schlug fehl.");

            o2 = new IcdTyp("C79.88");
            Assert.False(o1 == o2, "== Operator für \"C80.9\" und \"C79.88\" ergab TRUE.");
        }


        [Fact]
        public void InequalityOperator_Test()
        {
            var code = "C80.9";
            var o1 = new IcdTyp(code);
            var o2 = new IcdTyp(code);

            Assert.False(o1 != o2, "!= Operator für \"C80.9\" und \"C80.9\" ergab TRUE.");

            o2 = new IcdTyp(code.ToLower());
            Assert.False(o1 != o2, "!= Operator für \"C80.9\" und \"c80.9\" ergab TRUE.");

            o2 = new IcdTyp("C79.88");
            Assert.True(o1 != o2, "!= Operator für \"C80.9\" und \"C79.88\" ergab FALSE.");
        }
    }
}
