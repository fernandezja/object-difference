using AnyDiff.Extensions;
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectDiffFinder
{
    /// <summary>
    /// https://github.com/GregFinzer/Compare-Net-Objects
    /// https://www.nuget.org/packages/CompareNETObjects/
    /// </summary>
    public static class CompareNETObjectsAdapter
    {
        public static ComparisonResult Compare(object object1, object object2) {
            var compareLogic = new CompareLogic();
            return compareLogic.Compare(object1, object2);
        }
        
    }
}
