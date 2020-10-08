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
    public class FruehereTumorerkrankungTests
    {
        private DateTime[] _testDateExpectedList;
        private DatumTyp[] _testDatumTypActualList;
       
        public FruehereTumorerkrankungTests()
        {
            _testDateExpectedList = new DateTime[]
            {
                new DateTime(2020, 9, 13),
                new DateTime(2018, 10, 18),
                new DateTime(2016, 4, 5),
                new DateTime(2019, 1 ,23),
                new DateTime(2012, 8, 3),
                new DateTime(2012, 8, 3)
            };

            _testDatumTypActualList = new DatumTyp[]
            {
                new DatumTyp(2020, 9, 13),
                new DatumTyp(2018, 10, 18),
                new DatumTyp(new DateTime(2016, 4, 5), true, true),
                new DatumTyp(2019, 1, 23),
                new DatumTyp(2012, 8, 3),
                new DatumTyp(new DateTime(2012, 8, 3), false, true)
            };
        }

        [Fact]
        public void SortAscending_Test()
        {
            var fruehereErkrank = getListToCheck();
            var dateListOrdered = _testDateExpectedList.OrderBy(d => d).ToList();
            var ordererList = fruehereErkrank.OrderBy(f => f.Diagnosedatum).ThenBy(f => f.IcdCode);

            testSortationWithAssertions(dateListOrdered, ordererList);
        }

        [Fact]
        public void SortDescending_Test()
        {
            var fruehereErkrank = getListToCheck();
            var dateListOrdered = _testDateExpectedList.OrderByDescending(d => d).ToList();
            var ordererList = fruehereErkrank.OrderByDescending(f => f.Diagnosedatum).ThenBy(f => f.IcdCode);

            testSortationWithAssertions(dateListOrdered, ordererList);
        }

        private List<FruehereTumorerkrankung> getListToCheck()
        {
            var fruehereErkrank = new List<FruehereTumorerkrankung>();

            _testDatumTypActualList.ToList().ForEach(
              date => fruehereErkrank.Add(new FruehereTumorerkrankung() { Diagnosedatum = date }
            ));

            return fruehereErkrank;
        }

        private void testSortationWithAssertions(List<DateTime> dateExpectedList, IOrderedEnumerable<FruehereTumorerkrankung> datumActualList)
        {          
            var currIndex = 0;
            foreach (var e in datumActualList)
            {
                var listEnum = datumActualList.GetEnumerator();
                var expected = dateExpectedList[currIndex];
                var actual = e.Diagnosedatum;

                currIndex++;

                Assert.Equal<DateTime>(expected, actual);
            }
        }
    }
}