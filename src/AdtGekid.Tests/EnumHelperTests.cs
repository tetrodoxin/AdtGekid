using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;
using Xunit.Extensions;


namespace AdtGekid.Tests
{
    public class EnumHelperTests
    {

        [Fact]
        public void ToXmlEnumAttributeName_Test()
        {

            var e1 = HistoGrading.Item0;
            var str1 = e1.ToXmlEnumAttributeName();
            var e2 = HistoGrading.Item3;
            var str2 = e2.ToXmlEnumAttributeName();
            var e3 = HistoGrading.X;
            var str3 = e3.ToXmlEnumAttributeName();
            var e4 = HistoGrading.B;
            var str4 = e4.ToXmlEnumAttributeName();
            var e5 = BestrahlungZielgebiet.Item2_8Minus;
            var str5 = e5.ToXmlEnumAttributeName();

           
            Assert.Equal("3", str2);
            Assert.Equal("X", str3);
            Assert.Equal("B", str4);
            Assert.Equal("2.8.-", str5);

        }
    }
}
