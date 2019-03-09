using BenchmarkDotNet;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
//using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using System.Collections.ObjectModel;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Validators;

using BenchmarkDotNet.Characteristics;
using BenchmarkDotNet.Code;
using BenchmarkDotNet.Extensions;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Helpers;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Parameters;
using BenchmarkDotNet.Properties;
using BenchmarkDotNet.Reports;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Config : ManualConfig {
  public Config() {
    Add(ConsoleLogger.Default);
    Add(TargetMethodColumn.Method, 
      StatisticColumn.Mean, 
      StatisticColumn.Median, 
      StatisticColumn.Min,
      StatisticColumn.Q1,
      StatisticColumn.Q3,
      StatisticColumn.Max,
      BaselineRatioColumn.RatioMean);
    Add(BenchmarkDotNet.Diagnosers.MemoryDiagnoser.Default);
    Add(new InliningDiagnoser());
    Add(new TailCallDiagnoser());
    //Add(new BenchmarkDotNet.Diagnosers.Asm());

    Add(RPlotExporter.Default, CsvExporter.Default);
    Add(EnvironmentAnalyser.Default);
    UnionRule = ConfigUnionRule.AlwaysUseLocal;
    Add(new Job(EnvironmentMode.Core, RunMode.Dry) {
      Infrastructure = { Toolchain = CsProjCoreToolchain.NetCoreApp22 },
      Environment = { Runtime = Runtime.Core },
      Run = {
        LaunchCount = EntryPoint.LaunchCount,
        WarmupCount = 0,
        IterationCount = EntryPoint.IterationCount,
        RunStrategy = EntryPoint.Strategy },
      Accuracy = { MaxRelativeError = 0.01 }
    });
  }
}

public class Config30 : ManualConfig {
  public Config30() {
    Add(ConsoleLogger.Default);
    Add(TargetMethodColumn.Method,
      StatisticColumn.Mean,
      StatisticColumn.Median,
      StatisticColumn.Min,
      StatisticColumn.Q1,
      StatisticColumn.Q3,
      StatisticColumn.Max,
      BaselineRatioColumn.RatioMean);
    Add(RPlotExporter.Default, CsvExporter.Default);
    Add(EnvironmentAnalyser.Default);
    UnionRule = ConfigUnionRule.AlwaysUseLocal;
    Add(new Job(EnvironmentMode.Core, RunMode.Dry) {
      Infrastructure = { Toolchain = CsProjCoreToolchain.NetCoreApp30 },
      Environment = { Runtime = Runtime.Core },
      Run = {
        LaunchCount = EntryPoint.LaunchCount,
        WarmupCount = 0,
        IterationCount = EntryPoint.IterationCount,
        RunStrategy = EntryPoint.Strategy },
      Accuracy = { MaxRelativeError = 0.01 }
    });
  }
}



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

  [Benchmark] public void SwitchJumpTest() => SwitchJump.Main(EntryPoint.Input);
  [Benchmark] public void StructPtrGoTo() => NBody_StructPtr_GoTo.Main(EntryPoint.Input);
  [Benchmark] public void StructPtrOptimized() => NBody_StructPtr_Optimized.Main(EntryPoint.Input);
  [Benchmark(Baseline = true)] public void StructPtrOffical() => NBody_StructPtr_Official.Main(EntryPoint.Input);
}



public class SSETest {

  [Benchmark] public void SSE2_Test() => NBody_SSE2.Main(EntryPoint.Input);
  [Benchmark(Baseline = true)] public void StructPtrOffical() => NBody_StructPtr_Official.Main(EntryPoint.Input);
}





