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
    public class KvNrValidatorTests
    {
        [Theory]
        [InlineData("N360117751")]
        [InlineData("Z629410041")]
        [InlineData("A000500015")]
        public void PositiveT_Test(string value)
        {
            var validator = KvNrValidator.Instance;
            var actual = validator.GetValidatedValueOrThrow(value);

            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData("N123456789")]
        [InlineData("Z629400041")]
        public void Negative_Test(string value)
        {
            var validator = KvNrValidator.Instance;

            Assert.ThrowsDelegate p = () => validator.GetValidatedValueOrThrow(value);
            Assert.Throws<ArgumentException>(p);
        }
    }
}
