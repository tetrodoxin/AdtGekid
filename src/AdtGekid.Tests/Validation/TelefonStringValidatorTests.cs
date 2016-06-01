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
    public class TelefonStringValidatorTests
    {
        [Theory]
        [InlineData("030-71165432")]
        [InlineData("0044/194-83129")]
        [InlineData("+7 495 371 74 95")]
        [InlineData("0030187382")]
        [InlineData("        0800   31   32 800   -      1")]
        public void Positive_Test(string phoneString)
        {
            var validator = TelefonStringValidator.Instance;
            var actual = validator.GetValidatedValueOrThrow(phoneString);

            Assert.NotEmpty(actual);
        }

        [Theory]
        [InlineData("1234567890123456789012345")]
        [InlineData("555-noletter")]
        [InlineData("0341.80 80 266")]
        [InlineData("(0177) 12367876")]
        [InlineData("+")]
        public void Negative_Test(string phoneString)
        {
            var validator = TelefonStringValidator.Instance;
            string actual;
            Assert.Throws<ArgumentException>(() => actual = validator.GetValidatedValueOrThrow(phoneString));
        }
    }
}
