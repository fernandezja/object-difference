using ObjectDiffFinder.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ObjectDiffFinder.Tests
{
    public class CompareNETObjectsAdapterTests
    {
        [Fact]
        public void SimpleFieldString()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.Name = "Jedi X";

            var result = CompareNETObjectsAdapter.Compare(jedi1, jedi2);

            Assert.False(result.AreEqual);
            Assert.Single(result.Differences);
            Assert.Equal("\r\nBegin Differences (1 differences):\r\nTypes [String,String], Item Expected.Name != Actual.Name, Values (Jedi 1,Jedi X)\r\nEnd Differences (Maximum of 1 differences shown).", result.DifferencesString);

            var firtDiff = result.Differences[0];
            Assert.Equal("Actual", firtDiff.ActualName);
            Assert.Equal("Expected", firtDiff.ExpectedName);
            Assert.Equal("String", firtDiff.Object1TypeName);
            Assert.Equal("Jedi 1", firtDiff.Object1Value);
            Assert.Equal("String", firtDiff.Object2TypeName);
            Assert.Equal("Jedi X", firtDiff.Object2Value);
            Assert.Equal("Name", firtDiff.PropertyName);


        }

        [Fact]
        public void SimpleFieldInt()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.JediId = 2;

            var result = CompareNETObjectsAdapter.Compare(jedi1, jedi2);

            Assert.False(result.AreEqual);
            Assert.Single(result.Differences);
            Assert.Equal("\r\nBegin Differences (1 differences):\r\nTypes [Int32,Int32], Item Expected.JediId != Actual.JediId, Values (1,2)\r\nEnd Differences (Maximum of 1 differences shown).", result.DifferencesString);

            var firtDiff = result.Differences[0];
            Assert.Equal("Actual", firtDiff.ActualName);
            Assert.Equal("Expected", firtDiff.ExpectedName);
            Assert.Equal("Int32", firtDiff.Object1TypeName);
            Assert.Equal("1", firtDiff.Object1Value);
            Assert.Equal("Int32", firtDiff.Object2TypeName);
            Assert.Equal("2", firtDiff.Object2Value);
            Assert.Equal("JediId", firtDiff.PropertyName);
        }

        [Fact]
        public void SimpleFieldsStringAndInt()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(2);

            var result = CompareNETObjectsAdapter.Compare(jedi1, jedi2);

            Assert.False(result.AreEqual);
            Assert.Single(result.Differences);
            Assert.Equal("\r\nBegin Differences (1 differences):\r\nTypes [Int32,Int32], Item Expected.JediId != Actual.JediId, Values (1,2)\r\nEnd Differences (Maximum of 1 differences shown).", result.DifferencesString);

            var firtDiff = result.Differences[0];
            Assert.Equal("Actual", firtDiff.ActualName);
            Assert.Equal("Expected", firtDiff.ExpectedName);
            Assert.Equal("Int32", firtDiff.Object1TypeName);
            Assert.Equal("1", firtDiff.Object1Value);
            Assert.Equal("Int32", firtDiff.Object2TypeName);
            Assert.Equal("2", firtDiff.Object2Value);
            Assert.Equal("JediId", firtDiff.PropertyName);


            //var secondDiff = result.Differences[1];
            //Assert.Equal("Actual", secondDiff.ActualName);
            //Assert.Equal("Expected", secondDiff.ExpectedName);
            //Assert.Equal("String", secondDiff.Object1TypeName);
            //Assert.Equal("Jedi 1", secondDiff.Object1Value);
            //Assert.Equal("String", secondDiff.Object2TypeName);
            //Assert.Equal("Jedi X", secondDiff.Object2Value);
            //Assert.Equal("Name", secondDiff.PropertyName);

        }




        [Fact]
        public void ListDiffNameToList()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(2);

            var jediList1 = new List<Jedi>();
            var jediList2 = new List<Jedi>();

            for (int i = 0; i < 10; i++)
            {
                jediList1.Add(jedi1);
                jediList2.Add(jedi2);
            }


            var result = CompareNETObjectsAdapter.Compare(jediList1, jediList2);

            Assert.False(result.AreEqual);
            Assert.Single(result.Differences);
            Assert.Equal("\r\nBegin Differences (1 differences):\r\nTypes [Int32,Int32], Item Expected[0].JediId != Actual[0].JediId, Values (1,2)\r\nEnd Differences (Maximum of 1 differences shown).", result.DifferencesString);

            var firtDiff = result.Differences[0];
            Assert.Equal("Actual", firtDiff.ActualName);
            Assert.Equal("Expected", firtDiff.ExpectedName);
            Assert.Equal("Int32", firtDiff.Object1TypeName);
            Assert.Equal("1", firtDiff.Object1Value);
            Assert.Equal("Int32", firtDiff.Object2TypeName);
            Assert.Equal("2", firtDiff.Object2Value);
            Assert.Equal("[0].JediId", firtDiff.PropertyName);
        }
       


        private Jedi JediBuilder(int index)
        {
            return new Jedi()
            {
                JediId = index,
                Name = $"Jedi {index}",
                Years = index * 100,
                BirthDate = ""
            };
        }
    }
}
