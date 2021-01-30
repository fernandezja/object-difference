# object-difference
Performance tests to compare alternative methods to get properties difference between objects and return them in .NET


![CI .NET](https://github.com/fernandezja/object-difference/workflows/CI%20.NET/badge.svg)

 ### Methods to compare object
... description each method (soon)
 - AnyDiff
 - CompareNETObjects
 - Difference
 - JsonDiffPatch
 - JsonSimpleCompare
 - ObjectComparer


 ### Benchmark Results 

##### NET 5 (5.0.200-preview.20614.14)

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
```

|                  Method |       Mean |      Error |      StdDev |     Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------ |-----------:|-----------:|------------:|-----------:|------:|--------:|-------:|-------:|------:|----------:|
|           AnyDiffSimple | 113.085 us |  2.2540 us |   5.7371 us | 112.920 us |  1.00 |    0.00 | 4.0283 |      - |     - |   16893 B |
| CompareNETObjectsSimple |   7.463 us |  0.4107 us |   1.1916 us |   7.079 us |  0.06 |    0.01 | 1.0376 |      - |     - |    4360 B |
|        DifferenceSimple |   2.954 us |  0.2199 us |   0.6380 us |   2.926 us |  0.03 |    0.01 | 0.2136 |      - |     - |     904 B |
|     JsonDiffPatchSimple |  19.344 us |  1.1110 us |   3.2231 us |  18.747 us |  0.17 |    0.03 | 3.4180 |      - |     - |   14352 B |
| JsonSimpleCompareSimple | 746.165 us | 48.4112 us | 142.7416 us | 736.433 us |  6.30 |    1.35 | 5.8594 | 2.9297 |     - |   27626 B |
|    ObjectComparerSimple |   3.640 us |  0.1973 us |   0.5786 us |   3.606 us |  0.03 |    0.01 | 0.2213 |      - |     - |     936 B |
