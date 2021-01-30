using ObjectDiffFinder.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ObjectDiffFinder.Tests
{
    public class ObjectComparerTest
    {
        [Fact]
        public void SimpleFieldString()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.Name = "Jedi X";

            var result = ObjectComparer.GetChangedValues(jedi1, jedi2);

            Assert.Equal("Name: Jedi 1 –> Jedi X\r\n", result.ToString());
        }

        [Fact]
        public void SimpleFieldInt()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.JediId = 2;

            var result = ObjectComparer.GetChangedValues(jedi1, jedi2);

            Assert.Equal("JediId: 1 –> 2\r\n", result.ToString());
        }

        [Fact]
        public void SimpleFieldsStringAndInt()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(2);

            var result = ObjectComparer.GetChangedValues(jedi1, jedi2);

            Assert.Equal("JediId: 1 –> 2\r\nName: Jedi 1 –> Jedi 2\r\nYears: 100 –> 200\r\n", result.ToString());
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

            var result = ObjectComparer.GetChangedValues(jediList1, jediList2);

            Assert.Single(result);
            Assert.Equal("Name", result.ToString());
        }
        */
       


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
