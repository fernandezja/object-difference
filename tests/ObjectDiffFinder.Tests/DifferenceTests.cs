using ObjectDiffFinder.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ObjectDiffFinder.Tests
{
    public class DifferenceTests
    {
        [Fact]
        public void SimpleDiffNameToObject()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.Name = "Jedi X";

            var result = jedi1.Difference(jedi2);

            Assert.Equal("{ JediId = 0, Name = Jedi 1, Years = 0, BirthDate =  }", result.ToString());
        }

        [Fact]
        public void SimpleDiffNameToDictionary()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.Name = "Jedi X";

            var result = jedi1.DifferenceToDictionary(jedi2);

            Assert.Equal("Name", result.Keys.First());
            Assert.Equal("Jedi X", result.Values.First());
        }


        [Fact]
        public void SimpleDiffNameToList()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.Name = "Jedi X";

            var result = jedi1.DifferenceToList(jedi2);

            Assert.Single(result);
            Assert.Equal("Name", result[0].PropertyName);
            Assert.Equal("Jedi 1", result[0].Value1);
            Assert.Equal("Jedi X", result[0].Value2);
        }

        /*

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

            var result = jediList1.DifferenceToList(jediList2);

            Assert.Single(result);
            Assert.Equal("Name", result[0].PropertyName);
            Assert.Equal("Jedi 1", result[0].Value1);
            Assert.Equal("Jedi X", result[0].Value2);
        }
        */


        private Jedi JediBuilder(int index)
        {
            return new Jedi()
            {
                JediId = index,
                Name = $"Jedi {index}",
                Years = index*100,
                BirthDate = ""
            };
        }
    }
}
