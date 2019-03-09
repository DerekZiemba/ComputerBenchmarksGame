using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
using System.Reflection;



public static class EntryPoint {
  public static string[] Input = new string[] { 50000000.ToString() };
  public static int IterationCount = 10;
  public static int LaunchCount = 1;
  public static RunStrategy Strategy = RunStrategy.ColdStart;

  

  private static Action CreateTest(Type type, IConfig cfg = null) { 
    Action action = null;
    if (cfg != null) {
      action = () => BenchmarkRunner.Run(type, cfg);
    } else {
      var method = type.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);
      if (method?.ReturnType == typeof(string)) {
        var del = (Func<string[], string>)(method.CreateDelegate(typeof(Func<string[], string>)));
        action = () => Console.Out.WriteLine(del(EntryPoint.Input));
      }
      else if (method?.ReturnType == typeof(void)) {
        var del = (Action<string[]>)method.CreateDelegate(typeof(Action<string[]>));
        action = () => del(EntryPoint.Input);
      }
    }

    return () => {
      Console.WriteLine("Test: " + type.FullName);
      Stopwatch sw = Stopwatch.StartNew();
      action();
      Console.WriteLine("Millis: " + sw.ElapsedMilliseconds);
      Console.WriteLine("Finished. \n");
      Options.Evaluate(new string[] { "-h" });
    };
  }
  private static ArgOption BuildTestOption(Type type, string flag, string desc = null, Func<IConfig> cfg = null) {
    Action test = CreateTest(type, cfg?.Invoke());
    return new ArgOption(flag, desc == null ? type.FullName : desc, (args) => Task.Delay(1).ContinueWith((task) => test()));
  }



  public static ArgCollection Options { get; set; } =
      new ArgCollection {
        new ArgOption("-h", "List options", (args) => {
          Console.WriteLine("Available Options: ");
          foreach(var option in Options) {
            Console.WriteLine($"\t{option.Flag} = {option.Description}");
          }
          Console.Write("\n\nEnter Command: ");
        }),
        new ArgOption("-i", "Set number of times NBody Advance() is called.", (args) => {
          if (Int64.TryParse(args[0], out var val)) {
            Input = new string[] { val.ToString() }; ;
          }
          Console.WriteLine("Iterations: " + Input[0].ToString());
        }),
        new ArgOption("-c", "Count: Number of times each individual test is run.", (args) => {
          if (Int32.TryParse(args[0], out var val)) { IterationCount = val; }
          Console.WriteLine("IterationCount: " + IterationCount.ToString());
        }),
        new ArgOption("-l", "Launches: Number of times each test is launched.", (args) => {
          if (Int32.TryParse(args[0], out var val)) { LaunchCount = val; }
          Console.WriteLine("Launches: " + LaunchCount.ToString());
        }),
        new ArgOption("-s", "Strategy: 0-Throughput, 1-ColdStart, or 2-Monitoring", (args) => {
          if(Int32.TryParse(args[0], out var val)) {
            Strategy = (RunStrategy)val;
          } else {
            Strategy = Enum.GetValues(typeof(RunStrategy)).Cast<RunStrategy>().First(e=> e.ToString().StartsWith(args[0], StringComparison.OrdinalIgnoreCase));
          }
          Console.WriteLine("Strategy: " + Strategy.ToString());
        }),
        BuildTestOption(typeof(StructPtrTest), "structptr", "Run the StructPtr Test Bench", ()=>new Config()),
        BuildTestOption(typeof(SSETest), "net30", "Run the SSETest on newcore 3.0", ()=>new Config30()),
        BuildTestOption(typeof(NBody_StructPtr_Optimized), "opt"),
        BuildTestOption(typeof(NBody_Span_GoTo), "span"),
        BuildTestOption(typeof(NBody_StructPtr_GoTo), "goto"),
        BuildTestOption(typeof(SwitchJump), "jump"),
        BuildTestOption(typeof(NBody_FixedArrays), "FixedArrays"),
        BuildTestOption(typeof(NBody_CPP7_Translated), "cpp7"),
        BuildTestOption(typeof(NBody_SSE2), "SSE2"),
        BuildTestOption(typeof(NBody.Original.NBodyNum1), "o1"),
        BuildTestOption(typeof(NBody.Original.NBodyNum2), "o2"),
        BuildTestOption(typeof(NBody.Original.NBodyNum3), "o3"),
        BuildTestOption(typeof(NBody.Original.NBodyNum4), "o4"),
        BuildTestOption(typeof(NBody.Original.NBodyNum5), "o5"),
        BuildTestOption(typeof(NBody.Original.NBodyNum6), "o6"),
        BuildTestOption(typeof(NBody_StructPtr_Official), "o7"),
        BuildTestOption(typeof(NBody.Original.NBodyNum8), "o8")
      };


  public static void Main(string[] args) {
    while (true) {
      Options.Evaluate(args);       
      args = Console.ReadLine().Split();
    }
  }



}


