using ObjectDiffFinder.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ObjectDiffFinder.Tests
{
    public class AnyDiffAdapterTests
    {
        [Fact]
        public void SimpleDiffFieldString()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.Name = "Jedi X";

            var result = AnyDiffAdapter.Diff(jedi1, jedi2);

            Assert.Single(result);

            var firstDiff = result.First();

            Assert.Equal("Jedi 1", firstDiff.LeftValue);
            Assert.Equal(".Name", firstDiff.Path);
            Assert.Equal("Name", firstDiff.Property);
            Assert.Equal("String", firstDiff.PropertyType.Name);
        }

        [Fact]
        public void SimpleDiffFieldInt()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.JediId = 2;

            var result = AnyDiffAdapter.Diff(jedi1, jedi2);

            var firstDiff = result.First();

            Assert.Equal("1", firstDiff.LeftValue.ToString());
            Assert.Equal(".JediId", firstDiff.Path);
            Assert.Equal("JediId", firstDiff.Property);
            Assert.Equal("Int32", firstDiff.PropertyType.Name);
        }

        [Fact]
        public void SimpleFieldsStringAndInt()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(2);

            var result = AnyDiffAdapter.Diff(jedi1, jedi2);


            var firstDiff = result.ToList()[0];

            Assert.Equal("1", firstDiff.LeftValue.ToString());
            Assert.Equal(".JediId", firstDiff.Path);
            Assert.Equal("JediId", firstDiff.Property);
            Assert.Equal("Int32", firstDiff.PropertyType.Name);


            var secondDiff = result.ToList()[1];

            Assert.Equal("Jedi 1", secondDiff.LeftValue);
            Assert.Equal(".Name", secondDiff.Path);
            Assert.Equal("Name", secondDiff.Property);
            Assert.Equal("String", secondDiff.PropertyType.Name);



        }




        [Fact]
        public void ListDiffNameToList()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            var jediList1 = new List<Jedi>();
            var jediList2 = new List<Jedi>();

            for (int i = 0; i < 10; i++)
            {
                jediList1.Add(jedi1);
                jediList2.Add(jedi2);
            }

            jedi2.Name = "Jedi X";

            var result = AnyDiffAdapter.Diff(jediList1, jediList2);

            Assert.Equal(12, result.Count);
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
