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

        [Theory]
        [InlineData("m", Geschlecht.M)]
        [InlineData("M", Geschlecht.M)]
        [InlineData("w", Geschlecht.W)]
        [InlineData("W", Geschlecht.W)]
        [InlineData("s", Geschlecht.S)]
        [InlineData("u", Geschlecht.U)]        
        [InlineData("", Geschlecht.NotSpecified)]
        [InlineData(null, Geschlecht.NotSpecified)]
        public void TryParseAsEnumOrThrowDefault_Test<T>(string value, Geschlecht expected)           
        {
            var parsed = value.TryParseAsEnumOrThrow<Geschlecht>("", "");
            Assert.Equal<Geschlecht>(expected, parsed);
        }

       

        [Theory]
        [InlineData("1.", BestrahlungZielgebiet.Item1)]
        [InlineData("2.2.-", BestrahlungZielgebiet.Item2_2Minus)]
        public void TryParseAsEnumOrThrow_DeepParseByAttribute_Test<T>(string value, BestrahlungZielgebiet expected)
        {
            var parsed = value.TryParseAsEnumOrThrow<BestrahlungZielgebiet>("", "");
            Assert.Equal<BestrahlungZielgebiet>(expected, parsed);
        }

        [Theory]
        [InlineData("M", false)]
        [InlineData("m", true)]
        [InlineData("W", false)]
        [InlineData("w", true)]
        public void TryParseAsEnumOrThrow_CaseSensivity_Test<T>(string value, bool throwsExceptionAsExpected)
        {
            Action tryParse = () =>  value.TryParseAsEnumOrThrow<Geschlecht>("", "", true, false);
            var exActuallyThrown = false;
            try
            {
                tryParse();
            }
            catch (ArgumentException)
            {
                exActuallyThrown = true;
            }
            Assert.Equal(throwsExceptionAsExpected, exActuallyThrown);            
        }



        /// <summary>
        /// ToDo: Möglichkeit zur Rückgaben eines NULL-Werts
        /// dafür sollte dieser Test gelten
        /// </summary>
        [Fact]               
        public void TryParseAsEnumOrThrow_AllowNonEmpty_Test()
        {
            Action tryParseEmptyValue = () => "".TryParseAsEnumOrThrow<Geschlecht>("", "", false);
            // Geht komischerweise nicht (Exception wird nicht geworfen)
            //Assert.Throws<ArgumentException>(() => tryParseEmptyValue); 
            var argEx = false;
            try
            {
                tryParseEmptyValue();
            }
            catch (ArgumentException)
            {
                argEx = true;
            }
            Assert.True(argEx);
        }

        [Fact]
        public void TryParseAsEnumOrThrow_AllowEmpty_Test()
        {            
            // Geht komischerweise nicht (Exception wird nicht geworfen)
            //Assert.Throws<ArgumentException>(() => tryParseEmptyValue); 
            
            TnmSymbolA? nullSymbolA = TnmSymbolA.NotSpecified;
            try
            {
                nullSymbolA = "".TryParseAsEnumOrThrow<TnmSymbolA>("", "", true);
            }
            catch (ArgumentException)
            {                
            }
            Assert.False(nullSymbolA.HasValue);
        }

        [Fact]
        public void TryParseAsEnumOrThrow_InvalidValue_Test()
        {
            Action tryParseInvalidValue = () => "x".TryParseAsEnumOrThrow<Geschlecht>("", "", false);
            // Geht komischerweise nicht (Exception wird nicht geworfen)
            //Assert.Throws<ArgumentException>(() => tryParseInvalidValue);

            var argEx = false;
            try
            {
                tryParseInvalidValue();
            }
            catch (ArgumentException)
            {
                argEx = true;
            }
            Assert.True(argEx);
        }

        [Theory]
        [InlineData("SO")]
        [InlineData("WS")]
        [InlineData("CH")]
        [InlineData("HO")]
        [InlineData("IM")]
        public void TryParseAsEnumCollectionOrThrow_TherapieArt_Test(string therapieArt)
        {
            var collection = new System.Collections.ObjectModel.Collection<string>()
            {
                therapieArt
            };

            var enumCollection = collection.TryParseAsEnumCollectionOrThrow<SystemTherapieart>();

            Assert.Equal(true, enumCollection.Any(e => e.ToString() == therapieArt));

            var sysTh = new SystemischeTherapie();
            //var adtThArten = collection.TryParseAsEnumCollectionOrThrow<SystemTherapieart>();
            sysTh.TherapieArten = collection;
        }
    }
}
