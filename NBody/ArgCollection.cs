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




public class ArgCollection: KeyedCollection<string, ArgOption> {

  public ArgCollection() : base(StringComparer.OrdinalIgnoreCase) { }

  protected override string GetKeyForItem(ArgOption item) {
    return item.Flag;
  }

  public void Evaluate(string[] args) {
    for (var i = 0; i < args.Length; i++) {
      if (this.TryGetValue(args[i], out var option)) {
        var _params = args.Skip(i+1).TakeWhile(x => !x.StartsWith('-')).ToArray();
        option.Action(_params);
      }
    }
  }
}


public class ArgOption {
  public string Flag { get; set; }
  public string Description { get; set; }
  public Action<string[]> Action { get; set; }

  public ArgOption(string flag, string description, Action<string[]> op) {
    this.Flag = flag;
    this.Description = description;
    this.Action = op;
  }

}