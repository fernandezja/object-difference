using ObjectDiffFinder.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ObjectDiffFinder.Tests
{
    public class JsonSimpleCompareTests
    {
        [Fact]
        public void SimpleFieldString()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.Name = "Jedi X";

            var result = JsonSimpleCompare.Diff(jedi1, jedi2);

            Assert.Equal("field=JediId > value1=1 value2=1 | field=Name > value1=Jedi 1 value2=Jedi X | field=Years > value1=100 value2=100 | ", result);
        }

        [Fact]
        public void SimpleFieldInt()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(1);

            jedi2.JediId = 2;

            var result = JsonSimpleCompare.Diff(jedi1, jedi2);

            Assert.Equal("field=JediId > value1=1 value2=2 | field=Name > value1=Jedi 1 value2=Jedi 1 | field=Years > value1=100 value2=100 | ", result);
        }
   

        [Fact]
        public void SimpleFieldsStringAndInt()
        {
            var jedi1 = JediBuilder(1);
            var jedi2 = JediBuilder(2);

            var result = JsonSimpleCompare.Diff(jedi1, jedi2);

            Assert.Equal("field=JediId > value1=1 value2=2 | field=Name > value1=Jedi 1 value2=Jedi 2 | field=Years > value1=100 value2=200 | ", 
                result);

        }



        /*
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


            var result = JsonSimpleCompare.Diff(jediList1, jediList2);

            Assert.Equal("{\r\n  \"_t\": \"a\",\r\n  \"0\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"1\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"2\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"3\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"4\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"5\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"6\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"7\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"8\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  },\r\n  \"9\": {\r\n    \"JediId\": [\r\n      1,\r\n      2\r\n    ],\r\n    \"Name\": [\r\n      \"Jedi 1\",\r\n      \"Jedi 2\"\r\n    ],\r\n    \"Years\": [\r\n      100,\r\n      200\r\n    ]\r\n  }\r\n}", 
                        result);
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
