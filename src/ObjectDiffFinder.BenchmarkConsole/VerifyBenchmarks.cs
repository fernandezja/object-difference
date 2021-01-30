using BenchmarkDotNet.Attributes;
using ObjectDiffFinder.BenchmarkConsole.Verifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDiffFinder.BenchmarkConsole
{
    [MemoryDiagnoser]
    //ClrJob(baseline: true), CoreJob, MonoJob, CoreRtJob]
    //[RPlotExporter, RankColumn]
    public class VerifyBenchmarks
    {

        [Benchmark(Baseline = true)]
        public void AnyDiffSimple()
        {
            var verify = new AnyDiffVerify();
            verify.Simple();
        }

        [Benchmark]
        public void CompareNETObjectsSimple()
        {
            var verify = new CompareNETObjectsVerify();
            verify.Simple();
        }

        [Benchmark]
        public void DifferenceSimple()
        {
            var verify = new DifferenceVerify();
            verify.Simple();
        }

        [Benchmark]
        public void JsonDiffPatchSimple()
        {
            var verify = new JsonDiffPatchVerify();
            verify.Simple();
        }

        [Benchmark]
        public void JsonSimpleCompareSimple()
        {
            var verify = new JsonSimpleCompareVerify();
            verify.Simple();
        }


        [Benchmark]
        public void ObjectComparerSimple()
        {
            var verify = new ObjectComparerVerify();
            verify.Simple();
        }
    }
}
