using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;
using AdtGekid;
using AdtGekid.Validation;

namespace AdtGekid.Tests
{
    public class IkNrValidatorTests
    {
        [Theory]
        [InlineData("101575519")]
        [InlineData("104940005")]
        public void Positive_Test(string value)
        {
            var validator = IkNrValidator.Instance;
            var actual = validator.GetValidatedValueOrThrow(value, "Patient","KrankenkassenNr");

            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData("105175519")]
        [InlineData("104904005")]
        public void Negative_Test(string value)
        {
            var validator = IkNrValidator.Instance;
            
            Assert.ThrowsDelegate p = () => validator.GetValidatedValueOrThrow(value, "Patient", "KrankenkassenNr");
            Assert.Throws<ArgumentException>(p);
        }
    }
}
