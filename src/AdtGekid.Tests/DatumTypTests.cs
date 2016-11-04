using AdtGekid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace AdtGekid.Tests
{
    public class DatumTypTests
    {
        private FieldInfo _dateFieldInfo;

        public DatumTypTests()
        {
            _dateFieldInfo = typeof(DatumTyp).GetField("_date", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic);
        }

        [Fact]
        public void Ctor1_Test()
        {
            var expected = DateTime.Now;
            var o = new DatumTyp(expected);
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Ctor1a_WithUnknownMonth_Test()
        {
            var expected = DateTime.Now;
            var o = new DatumTyp(expected, true, false);
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.True(o.MonthUnknown, "Erwartet: Monat unbekannt.");
            Assert.False(o.DayUnknown, "Erwartet: Tag nicht unbekannt.");
        }

        [Fact]
        public void Ctor1a_WithUnknownDay_Test()
        {
            var expected = DateTime.Now;
            var o = new DatumTyp(expected, false, true);
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.False(o.MonthUnknown, "Erwartet: Monat nicht unbekannt.");
            Assert.True(o.DayUnknown, "Erwartet: Tag unbekannt.");
        }

        [Fact]
        public void Ctor1a_WithUnknownMonthDay_Test()
        {
            var expected = DateTime.Now;
            var o = new DatumTyp(expected, true, true);
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.True(o.MonthUnknown, "Erwartet: Monat unbekannt.");
            Assert.True(o.DayUnknown, "Erwartet: Tag unbekannt.");
        }

        [Fact]
        public void Ctor2_Test()
        {
            var expected = DateTime.Today;
            var o = new DatumTyp(expected.Year, expected.Month, expected.Day);
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Ctor2_WithZeroMonth_Test()
        {
            var expected = new DateTime(DateTime.Now.Year, 1, DateTime.Today.Day);
            var o = new DatumTyp(expected.Year, 0, expected.Day);
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.True(o.MonthUnknown, "Erwartet: Monat unbekannt.");
            Assert.False(o.DayUnknown, "Erwartet: Tag nicht unbekannt.");
        }

        [Fact]
        public void Ctor2_WithZeroDay_Test()
        {
            var expected = new DateTime(DateTime.Now.Year, DateTime.Today.Month, 1);
            var o = new DatumTyp(expected.Year, expected.Month, 0);
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.False(o.MonthUnknown, "Erwartet: Monat nicht unbekannt.");
            Assert.True(o.DayUnknown, "Erwartet: Tag unbekannt.");
        }

        [Fact]
        public void Ctor2_WithZeroMonthDay_Test()
        {
            var expected = new DateTime(DateTime.Now.Year, 1, 1);
            var o = new DatumTyp(expected.Year, 0, 0);
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.True(o.MonthUnknown, "Erwartet: Monat unbekannt.");
            Assert.True(o.DayUnknown, "Erwartet: Tag unbekannt.");
        }

        [Fact]
        public void Ctor3_Test()
        {
            var expected = new DateTime(2016, 07, 19);
            var o = new DatumTyp("19.07.2016");
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Ctor3_WithZeroDay_Test()
        {
            var expected = new DateTime(2016, 07, 1);
            var o = new DatumTyp("00.07.2016");
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.False(o.MonthUnknown, "Erwartet: Monat nicht unbekannt.");
            Assert.True(o.DayUnknown, "Erwartet: Tag unbekannt.");
        }

        [Fact]
        public void Ctor3_WithZeroMonth_Test()
        {
            var expected = new DateTime(2016, 1, 19);
            var o = new DatumTyp("19.00.2016");
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.True(o.MonthUnknown, "Erwartet: Monat unbekannt.");
            Assert.False(o.DayUnknown, "Erwartet: Tag nicht unbekannt.");
        }

        [Fact]
        public void Ctor3_WithZeroMonthDay_Test()
        {
            var expected = new DateTime(2016, 1, 1);
            var o = new DatumTyp("00.00.2016");
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.True(o.MonthUnknown, "Erwartet: Monat unbekannt.");
            Assert.True(o.DayUnknown, "Erwartet: Tag unbekannt.");
        }

        [Fact]
        public void ImplicitCastFromDateTime_Test()
        {
            var expected = new DateTime(2016, 07, 19);
            DatumTyp o = expected;
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void ImplicitCastFromNullableDateTime_Test()
        {
            var expected = new DateTime?(new DateTime(2016, 07, 19));
            DatumTyp o = expected;
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void ImplicitCastFromString_Test()
        {
            var expected = new DateTime(2016, 07, 19);
            DatumTyp o = "19.07.2016";
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void ImplicitCastFromString_WithUnknownMonth_Test()
        {
            var expected = new DateTime(2016, 1, 19);
            DatumTyp o = "19.00.2016";
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.True(o.MonthUnknown, "Erwartet: Monat unbekannt.");
            Assert.False(o.DayUnknown, "Erwartet: Tag nicht unbekannt.");
        }

        [Fact]
        public void ImplicitCastFromString_WithUnknownDay_Test()
        {
            var expected = new DateTime(2016, 07, 1);
            DatumTyp o = "00.07.2016";
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.False(o.MonthUnknown, "Erwartet: Monat nicht unbekannt.");
            Assert.True(o.DayUnknown, "Erwartet: Tag unbekannt.");
        }

        [Fact]
        public void ImplicitCastFromString_WithUnknownMonthDay_Test()
        {
            var expected = new DateTime(2016, 1, 1);
            DatumTyp o = "00.00.2016";
            var actual = _dateFieldInfo.GetValue(o) as DateTime?;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Assert.True(o.MonthUnknown, "Erwartet: Monat unbekannt.");
            Assert.True(o.DayUnknown, "Erwartet: Tag unbekannt.");
        }

        [Fact]
        public void ImplicitCastToDateTime_Test()
        {
            var expected = new DateTime(2016, 07, 19);
            DatumTyp o = new DatumTyp(expected);
            DateTime actual = o;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImplicitCastToNullableDateTime_Test()
        {
            var expected = new DateTime(2016, 07, 19);
            DatumTyp o = new DatumTyp(expected);
            DateTime? actual = o;

            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void ImplicitCastToString_Test()
        {
            var o = new DatumTyp(2016, 07, 19);
            var expected = "19.07.2016";
            string actual = o;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImplicitCastToString_WithUnknownMonth_Test()
        {
            var o = new DatumTyp(2016, 0, 19);
            var expected = "19.00.2016";
            string actual = o;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImplicitCastToString_WithUnknownDay_Test()
        {
            var o = new DatumTyp(2016, 07, 0);
            var expected = "00.07.2016";
            string actual = o;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImplicitCastToString_WithUnknownMonthDay_Test()
        {
            var o = new DatumTyp(2016, 0, 0);
            var expected = "00.00.2016";
            string actual = o;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_Test()
        {
            var o = new DatumTyp(2016, 07, 19);
            var expected = "19.07.2016";
            string actual = o.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_WithUnknownMonth_Test()
        {
            var o = new DatumTyp(2016, 0, 19);
            var expected = "19.00.2016";
            string actual = o.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_WithUnknownDay_Test()
        {
            var o = new DatumTyp(2016, 7, 0);
            var expected = "00.07.2016";
            string actual = o.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_WithUnknownMonthDay_Test()
        {
            var o = new DatumTyp(2016, 0, 0);
            var expected = "00.00.2016";
            string actual = o.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OperatorEquality_Test()
        {
            var o1 = new DatumTyp(2016, 07, 19);
            var o2 = new DatumTyp(2016, 07, 19);

            Assert.True(o1 == o2);
        }

        [Fact]
        public void OperatorEquality_Negative_Test()
        {
            var o1 = new DatumTyp(2016, 07, 19);
            var o2 = new DatumTyp(16, 07, 19);

            Assert.False(o1 == o2);
        }

        [Fact]
        public void OperatorEquality_NegativeDueToUnknownMonth_Test()
        {
            var o1 = new DatumTyp(2016, 0, 19);
            var o2 = new DatumTyp(16, 1, 19);

            Assert.False(o1 == o2);
        }

        [Fact]
        public void OperatorEquality_NegativeDueToUnknownDay_Test()
        {
            var o1 = new DatumTyp(2016, 07, 1);
            var o2 = new DatumTyp(16, 07, 0);

            Assert.False(o1 == o2);
        }

        [Fact]
        public void OperatorInequality_Test()
        {
            var o1 = new DatumTyp(2016, 07, 19);
            var o2 = new DatumTyp(2016, 07, 17);

            Assert.True(o1 != o2);
        }

        [Fact]
        public void OperatorInequality_UnknownMonth_Test()
        {
            var o1 = new DatumTyp(2016, 1, 19);
            var o2 = new DatumTyp(2016, 0, 17);

            Assert.True(o1 != o2);
        }

        [Fact]
        public void OperatorInequality_UnknownDay_Test()
        {
            var o1 = new DatumTyp(2016, 07, 1);
            var o2 = new DatumTyp(2016, 07, 0);

            Assert.True(o1 != o2);
        }

        [Fact]
        public void OperatorInequality_Negative_Test()
        {
            var o1 = new DatumTyp(2016, 01, 1);
            var o2 = new DatumTyp(2016, 01, 1);

            Assert.False(o1 != o2);
        }
    }
}