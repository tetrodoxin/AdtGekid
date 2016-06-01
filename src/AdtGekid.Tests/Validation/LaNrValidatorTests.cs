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
    public class LaNrValidatorTests
    {
        [Theory]
        [InlineData("117824388")]
        [InlineData("725380512")]
        [InlineData("839704601")]
        [InlineData("012792601")]
        public void Positive_Test(string value)
        {
            var validator = LaNrValidator.Instance;
            var actual = validator.GetValidatedValueOrThrow(value);

            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData("171862388")]
        [InlineData("117324388")]
        public void Negative_Test(string value)
        {
            var validator = LaNrValidator.Instance;

            Assert.ThrowsDelegate p = () => validator.GetValidatedValueOrThrow(value);
            Assert.Throws<ArgumentException>(p);
        }
    }
}
