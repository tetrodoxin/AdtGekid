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
    public class EmailStringValidatorTests
    {
        [Theory]
        [InlineData("someone@somewhere.com")]
        [InlineData("magnus.spencer@sub.net.com")]
        [InlineData("myriam-hilfinger_craft@krikkit-cloud.com")]
        [InlineData("full-17-load.net_adress@intra.krikkit-cloud.com")]
        public void Positive_Test(string mailAdress)
        {
            var validator = EmailStringValidator.Instance;
            var actual = validator.GetValidatedValueOrThrow(mailAdress);

            Assert.Equal(mailAdress, actual);
        }

        [Theory]
        [InlineData("no space@gmx.ch")]
        [InlineData("in/valid@aol.com")]
        [InlineData("missing-net")]
        [InlineData("missing_tld@t-online")]
        public void Negative_Test(string mailAdress)
        {
            var validator = EmailStringValidator.Instance;
            string actual;
            Assert.Throws<ArgumentException>(() => actual = validator.GetValidatedValueOrThrow(mailAdress));
        }
    }
}
