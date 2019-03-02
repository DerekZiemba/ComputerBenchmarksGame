using System;
using System.Collections.Generic;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using System.Collections.ObjectModel;


[Config(typeof(Config))]
[BenchmarkDotNet.Attributes.AllStatisticsColumn]
public class NBodyTest {
  public static string[] args = new string[] { 50000000.ToString() };
  public static void SetIterations(long count) => args = new string[] { count.ToString() };
  public static int Count { get; set; } = 10;
  public static int Launches { get; set; } = 1;


  //[Benchmark] public static void NBodyStruct_Vector() => NBody_Vector.Main(args);

  //[Benchmark] public static void StructPtrNext_3() => NBody_StructPtr_Official3.Main(args);


  //[Benchmark] public void FixedArrays_Test() => NBody_FixedArrays.Main(args);

  //[Benchmark] public void ArrayPtr_Test() => NBody_ArrayPtr.Main(args);

  //[Benchmark] public void SSE2_Test() => NBody_SSE2.Main(args);

  [Benchmark] public void CPP7_Test() => NBody_CPP7_Translated.Main(args);

  //[Benchmark] public void NBody_StructPtr2_Test() => NBody_StructPtr2.Main(args);

  //[Benchmark(Baseline = true)] public void Baseline() => NBody_Baseline.Main(args);


  //[Benchmark] public void StructPtrOptimized2() => NBody_StructPtr_Optimized2.Main(args);
  [Benchmark] public void StructPtrOptimized() => NBody_StructPtr_Optimized.Main(args);
  [Benchmark(Baseline = true)] public void StructPtrOffical() => NBody_StructPtr_Official.Main(args);



  public class Config: ManualConfig {
    public Config() {
      Add(new Job(EnvironmentMode.Core, RunMode.Dry) {
        Infrastructure = { Toolchain = CsProjCoreToolchain.NetCoreApp22 },
        Environment = { Runtime = Runtime.Core },
        Run = { LaunchCount = Launches, WarmupCount = 0, IterationCount = Count, RunStrategy = RunStrategy.ColdStart },
        Accuracy = { MaxRelativeError = 0.01 },
      });
    }
  }

}




public static class NBodyEntryPoint {


  private static Action Test;
  public static ArgCollection Options { get; set; } =
      new ArgCollection {
        new ArgOption("-h", "List options", (args, val) => {
          foreach(var option in Options) {
            Console.WriteLine($"\t{option.Flag} = {option.Description}");
          }
          Console.WriteLine("Enter Command:");
          Main(Console.ReadLine().Split());
        }),
        new ArgOption("-i", "Set number of times NBody Advance() is called.", (args, val) => {
          if (Int64.TryParse(val, out var iterations)) { NBodyTest.SetIterations(iterations); }
        }),
        new ArgOption("-c", "Count: Number of times each individual test is run.", (args, val) => {
          if (Int32.TryParse(val, out var count)) { NBodyTest.Count = count; }
        }),
        new ArgOption("-l", "Launches: Number of times each test is launched.", (args, val) => {
          if (Int32.TryParse(val, out var launches)) { NBodyTest.Launches = launches; }
        }),
        new ArgOption("opt", nameof(NBody_StructPtr_Optimized), (args, val) => {
          Test = ()=> NBody_StructPtr_Optimized.Main(NBodyTest.args);
        }),
        new ArgOption("FixedArrays", nameof(NBody_FixedArrays), (args, val) => {
          Test = ()=> NBody_FixedArrays.Main(NBodyTest.args);
        }),
        new ArgOption("cpp7", nameof(NBody_CPP7_Translated), (args, val) => {
          Test = ()=> NBody_CPP7_Translated.Main(NBodyTest.args);
        }),
        new ArgOption("SSE2", nameof(NBody_SSE2), (args, val) => {
          Test = ()=> NBody_SSE2.Main(NBodyTest.args);
        }),
        new ArgOption("o1", nameof(NBody.Original.NBodyNum1), (args, val) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum1.Main(NBodyTest.args));
        }),
        new ArgOption("o2", nameof(NBody.Original.NBodyNum2), (args, val) => {
          Test =()=> Console.WriteLine(NBody.Original.NBodyNum2.Main(NBodyTest.args));
        }),
        new ArgOption("o3", nameof(NBody.Original.NBodyNum3), (args, val) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum3.Main(NBodyTest.args));
        }),
        new ArgOption("o4", nameof(NBody.Original.NBodyNum4), (args, val) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum4.Main(NBodyTest.args));
        }),
        new ArgOption("o5", nameof(NBody.Original.NBodyNum5), (args, val) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum8.Main(NBodyTest.args));
        }),
        new ArgOption("o6", nameof(NBody.Original.NBodyNum6), (args, val) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum6.Main(NBodyTest.args));
        }),
        new ArgOption("o8", nameof(NBody.Original.NBodyNum8), (args, val) => {
          Test = ()=> Console.WriteLine(NBody.Original.NBodyNum8.Main(NBodyTest.args));
        })
      };


  public static void Main(string[] args) {
    Options.Evaluate(args);
    if(Test != null) {
      Test();
    } else {
      var summary = BenchmarkRunner.Run<NBodyTest>();
    }
    Test = null;
    Console.WriteLine("Finished");
    Main(Console.ReadLine().Split());
  }



}



