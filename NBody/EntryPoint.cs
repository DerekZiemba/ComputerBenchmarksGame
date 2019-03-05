using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using System.Collections.ObjectModel;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using System.Diagnostics;

public class NBodyTest {

  //[Benchmark] public static void NBodyStruct_Vector() => NBody_Vector.Main(args);

  //[Benchmark] public static void StructPtrNext_3() => NBody_StructPtr_Official3.Main(args);


  //[Benchmark] public void FixedArrays_Test() => NBody_FixedArrays.Main(args);

  //[Benchmark] public void ArrayPtr_Test() => NBody_ArrayPtr.Main(args);

  //[Benchmark] public void SSE2_Test() => NBody_SSE2.Main(args);

  //[Benchmark] public void CPP7_Test() => NBody_CPP7_Translated.Main(args);

  //[Benchmark] public void NBody_StructPtr2_Test() => NBody_StructPtr2.Main(args);

  [Benchmark] public void Baseline() => NBody_Baseline.Main(EntryPoint.Input);


  //[Benchmark] public void StructPtrOptimized2() => NBody_StructPtr_Optimized2.Main(args);
  [Benchmark] public void StructPtrOptimized() => NBody_StructPtr_Optimized.Main(EntryPoint.Input);
  [Benchmark(Baseline = true)] public void StructPtrOffical() => NBody_StructPtr_Official.Main(EntryPoint.Input);


}


public class StructPtrTest {

  [Benchmark] public void StructPtrGoTo() => NBody_StructPtr_Goto.Main(EntryPoint.Input);
  [Benchmark] public void StructPtrOptimized() => NBody_StructPtr_Optimized.Main(EntryPoint.Input);
  [Benchmark(Baseline = true)] public void StructPtrOffical() => NBody_StructPtr_Official.Main(EntryPoint.Input);
}

public class SSETest {

  [Benchmark] public void SSE2_Test() => NBody_SSE2.Main(EntryPoint.Input);
  [Benchmark(Baseline = true)] public void StructPtrOffical() => NBody_StructPtr_Official.Main(EntryPoint.Input);
}

public class Config : ManualConfig {
  public Config() {
    Add(ConsoleLogger.Default);
    Add(EnvironmentAnalyser.Default);
    Add(new Job(EnvironmentMode.Core, RunMode.Dry) {
      Infrastructure = { Toolchain = CsProjCoreToolchain.NetCoreApp22 },
      Environment = { Runtime = Runtime.Core },
      Run = {
        LaunchCount = EntryPoint.LaunchCount,
        WarmupCount = 0,
        IterationCount = EntryPoint.IterationCount,
        RunStrategy = RunStrategy.ColdStart },
      Accuracy = { MaxRelativeError = 0.01 }
    });
    UnionRule = ConfigUnionRule.AlwaysUseLocal;
    Add(BenchmarkDotNet.Columns.StatisticColumn.AllStatistics);
  }
}

public class Config30: ManualConfig {
  public Config30() {
    Add(ConsoleLogger.Default);
    Add(EnvironmentAnalyser.Default);
    Add(new Job(EnvironmentMode.Core, RunMode.Dry) {
      Infrastructure = { Toolchain = CsProjCoreToolchain.NetCoreApp30 },
      Environment = { Runtime = Runtime.Core },
      Run = {
        LaunchCount = EntryPoint.LaunchCount,
        WarmupCount = 0,
        IterationCount = EntryPoint.IterationCount,
        RunStrategy = RunStrategy.ColdStart },
      Accuracy = { MaxRelativeError = 0.01 }
    });
    UnionRule = ConfigUnionRule.AlwaysUseLocal;
    Add(BenchmarkDotNet.Columns.StatisticColumn.AllStatistics);
  }
}

public static class EntryPoint {
  public static string[] Input = new string[] { 50000000.ToString() };
  public static int IterationCount = 10;
  public static int LaunchCount = 1;

  private static Action Test;


  public static ArgCollection Options { get; set; } =
      new ArgCollection {
        new ArgOption("-h", "List options", (args) => {
          foreach(var option in Options) {
            Console.WriteLine($"\t{option.Flag} = {option.Description}");
          }
          Console.WriteLine("Enter Command:");
          Main(Console.ReadLine().Split());
        }),
        new ArgOption("-i", "Set number of times NBody Advance() is called.", (args) => {
          if (Int64.TryParse(args[0], out var val)) {
            Input = new string[] { val.ToString() }; Console.Out.WriteLine("Iterations set to: " + String.Join(", ", Input));
          }
        }),
        new ArgOption("-c", "Count: Number of times each individual test is run.", (args) => {
          if (Int32.TryParse(args[0], out var val)) { IterationCount = val; Console.Out.WriteLine("IterationCount: ", IterationCount); }
        }),
        new ArgOption("-l", "Launches: Number of times each test is launched.", (args) => {
          if (Int32.TryParse(args[0], out var val)) { LaunchCount = val; Console.Out.WriteLine("LaunchCount: ", LaunchCount); }
        }),
        new ArgOption("structptr", "Run the StructPtr Test Bench", (args) => {
          Test = ()=> BenchmarkRunner.Run<StructPtrTest>(new Config());
        }),
        new ArgOption("net30", "Run the SSETest on newcore 3.0", (args) => {
          Test = ()=> BenchmarkRunner.Run<SSETest>(new Config30());
        }),
        new ArgOption("opt", nameof(NBody_StructPtr_Optimized), (args) => {
          Test = ()=> NBody_StructPtr_Optimized.Main(Input);
        }),
        new ArgOption("goto", nameof(NBody_StructPtr_Goto), (args) => {
          Test = ()=> NBody_StructPtr_Goto.Main(Input);
        }),
        new ArgOption("FixedArrays", nameof(NBody_FixedArrays), (args) => {
          Test = ()=> NBody_FixedArrays.Main(Input);
        }),
        new ArgOption("cpp7", nameof(NBody_CPP7_Translated), (args) => {
          Test = ()=> NBody_CPP7_Translated.Main(Input);
        }),
        new ArgOption("SSE2", nameof(NBody_SSE2), (args) => {
          Test = ()=> NBody_SSE2.Main(Input);
        }),
        new ArgOption("o1", nameof(NBody.Original.NBodyNum1), (args) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum1.Main(EntryPoint.Input));
        }),
        new ArgOption("o2", nameof(NBody.Original.NBodyNum2), (args) => {
          Test =()=> Console.WriteLine(NBody.Original.NBodyNum2.Main(EntryPoint.Input));
        }),
        new ArgOption("o3", nameof(NBody.Original.NBodyNum3), (args) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum3.Main(EntryPoint.Input)); 
        }),
        new ArgOption("o4", nameof(NBody.Original.NBodyNum4), (args) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum4.Main(EntryPoint.Input));
        }),
        new ArgOption("o5", nameof(NBody.Original.NBodyNum5), (args) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum8.Main(EntryPoint.Input));
        }),
        new ArgOption("o6", nameof(NBody.Original.NBodyNum6), (args) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum6.Main(EntryPoint.Input));
        }),
        new ArgOption("o8", nameof(NBody.Original.NBodyNum8), (args) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum8.Main(EntryPoint.Input));
        })
      };


  public static void Main(string[] args) {
    Options.Evaluate(args);
    if(Test != null) {
      var sw = Stopwatch.StartNew();
      Test();
      Console.Out.WriteLine("Millis: " + sw.ElapsedMilliseconds);
    } else {
      var summary = BenchmarkRunner.Run<NBodyTest>(new Config());
    }
    Test = null;
    Console.WriteLine("Finished");
    Main(Console.ReadLine().Split());
  }



}



