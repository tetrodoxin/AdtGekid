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
        private DateTime[] _testDateList;

        public FruehereTumorerkrankungTests()
        {
            _testDateList = new DateTime[]
            {
                new DateTime(2020,9,13),
                new DateTime(2018,10,18),
                new DateTime(2019,1,23),
                new DateTime(2012,8,3)
            };          
        }

        [Fact]
        public void SortAscending_Test()
        {
            var fruehereErkrank = getListToCheck();
            var dateListOrdered = _testDateList.OrderBy(d => d).ToList();
            var ordererList = fruehereErkrank.OrderBy(f => f).ThenBy(f => f.IcdCode);

            testSortationWithAssertions(dateListOrdered, ordererList);
        }

        [Fact]
        public void SortDescending_Test()
        {
            var fruehereErkrank = getListToCheck();
            var dateListOrdered = _testDateList.OrderByDescending(d => d).ToList();
            var ordererList = fruehereErkrank.OrderByDescending(f => f).ThenBy(f => f.IcdCode);

            testSortationWithAssertions(dateListOrdered, ordererList);
        }

        private List<FruehereTumorerkrankung> getListToCheck()
        {
            var fruehereErkrank = new List<FruehereTumorerkrankung>();

            _testDateList.ToList().ForEach(
              date => fruehereErkrank.Add(new FruehereTumorerkrankung() { Diagnosedatum = date }
            ));

            return fruehereErkrank;
        }

        private void testSortationWithAssertions(List<DateTime> dateCheckList, IOrderedEnumerable<FruehereTumorerkrankung> orderedList)
        {          
            var currIndex = 0;
            foreach (var e in orderedList)
            {
                var listEnum = orderedList.GetEnumerator();
                var expected = dateCheckList[currIndex];
                var actual = e.Diagnosedatum;

                currIndex++;

                Assert.Equal<DatumTyp>(expected, actual);
            }
        }
    }
}