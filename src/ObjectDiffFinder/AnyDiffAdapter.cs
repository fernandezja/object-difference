using AnyDiff.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectDiffFinder
{
    /// <summary>
    /// https://github.com/replaysMike/AnyDiff
    /// </summary>
    public static class AnyDiffAdapter
    {
        public static ICollection<AnyDiff.Difference> Diff(object object1, object object2) {
            return object1.Diff(object2);
        }
        
    }
}
