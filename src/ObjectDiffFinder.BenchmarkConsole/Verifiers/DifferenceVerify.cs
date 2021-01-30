using ObjectDiffFinder.BenchmarkConsole.Builders;
using ObjectDiffFinder.BenchmarkConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDiffFinder.BenchmarkConsole.Verifiers
{
    public class DifferenceVerify : IVerifyBase
    {
        public bool Simple()
        {
            var jedi1 = JediBuilder.Build(1);
            var jedi2 = JediBuilder.Build(1);

            jedi2.JediId = 2;

            var result = jedi1.DifferenceToList(jedi2);

            if (result.Any())
            {
                return true;
            }

            return false;
        }
    }
}
