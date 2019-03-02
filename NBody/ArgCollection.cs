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




public class ArgCollection: KeyedCollection<string, ArgOption> {

  public ArgCollection() : base(StringComparer.OrdinalIgnoreCase) { }

  protected override string GetKeyForItem(ArgOption item) {
    return item.Flag;
  }

  public void Evaluate(string[] args) {
    foreach(var arg in args) {
      if(this.TryGetValue(arg, out var option)) {
        option.Action(args, arg);
      }
    }
  }
}


public class ArgOption {
  public string Flag { get; set; }
  public string Description { get; set; }
  public Action<string[], string> Action { get; set; }
  public ArgOption(string flag, string description, Action<string[], string> op) {
    this.Flag = flag;
    this.Description = description;
    this.Action = op;
  }
}