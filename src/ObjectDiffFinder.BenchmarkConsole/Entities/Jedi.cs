using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDiffFinder.BenchmarkConsole.Entities
{
    public class Jedi
    {
        public int JediId { get; set; }
        public string Name { get; set; }
        public int Years { get; set; }
        public string BirthDate { get; set; }

        public override string ToString()
        {
            return $"{{ JediId = {JediId}, Name = {Name}, Years = {Years}, BirthDate = {BirthDate} }}";
        }
    }
}
