using System;
using System.Linq;
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




public class ArgCollection: List<ArgOption> {

  public ArgCollection() : base() { }

  public void Evaluate(string[] args) {
    for (var i = 0; i < args.Length; i++) {
      var candidates = this.Where(op => op.Flag.StartsWith(args[i], StringComparison.OrdinalIgnoreCase)).ToArray();
      if (candidates.Length > 1) {
        Console.WriteLine($"AMBIGUOUS OPTION '{args[i]}' with multiple candidates:");
        foreach (var op in candidates) { Console.WriteLine($"\tPartial Match: {op.Flag} : {op.Description}"); }
      } else if (candidates.Length == 0) {
        Console.WriteLine($"INVALID OPTION '{args[i]}'");
      } else if (candidates.Length == 1) {
        var _params = args.Skip(i + 1).TakeWhile(x => !x.StartsWith('-')).ToArray();
        i += candidates[0].Action(_params);
      }
    }
  }
}


public class ArgOption {
  public string Flag { get; set; }
  public string Description { get; set; }
  public Func<string[], int> Action { get; set; }

  public ArgOption(string flag, string description, Func<string[], int> op) {
    this.Flag = flag;
    this.Description = description;
    this.Action = op;
  }

}