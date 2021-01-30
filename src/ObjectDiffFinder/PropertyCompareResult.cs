using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectDiffFinder
{
    public class PropertyCompareResult
    {
        public string PropertyName { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public PropertyCompareResult()
        {

        }

        public PropertyCompareResult(string propertyName, string value1, string value2)
        {
            PropertyName = propertyName;
            Value1 = value1;
            Value2 = value2;
        }
    }
}
