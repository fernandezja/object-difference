using ObjectDiffFinder.BenchmarkConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDiffFinder.BenchmarkConsole.Builders
{
    public abstract class JediBuilder
    {
        public static Jedi Build(int index)
        {
            return new Jedi()
            {
                JediId = index,
                Name = $"Jedi {index}",
                Years = index * 100,
                BirthDate = $"Date {index}"
            };
        }
    }
}
