/* The Computer Language Benchmarks Game http://benchmarksgame.alioth.debian.org/
    Originally contributed by Isaac Gouy.
    Optimized to use Structs and Pointers by Derek Ziemba. 
*/
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

/// <summary>
/// This method is slow on the first call but fast on later calls. The Run method contains all logic so JIT doesn't work for us. 
/// </summary>
public static class SwitchJump {

  private const int COUNT = 5;
  private const int SIZE = COUNT * 7;
  private const double DT = 0.01;

  unsafe struct NBody { public double x, y, z, vx, vy, vz, mass;
    public override string ToString() {
      return $"x={x.Fmt()}, y={y.Fmt()}, z={z.Fmt()}, vx={vx.Fmt()}, vy={vy.Fmt()}, vz={vz.Fmt()}, mass={mass.Fmt()}";
    }
  }

  public unsafe static void Main(string[] args) {
    //RuntimeHelpers.PrepareMethod(typeof(NBody_Jump).GetMethod("Run", BindingFlags.Static | BindingFlags.NonPublic).MethodHandle);
    Run(args.Length > 0 ? Int32.Parse(args[0]) : 1000);
  }

  private enum Method : byte {  
    DEFAULT,
    Energy,
    EnergyOuterLoop,
    EnergyInnerLoop,
    EnergyInnerLoopBody,
    Advance,
    AdvanceOuterLoop,
    AdvanceInnerLoop,
    DerefBI,
    Finished,
  }

  private unsafe static void Run(int advancements) {
    unchecked {
      double 
        ix = 0d, iy = 0d, iz = 0d, ivx = 0d, ivy = 0d, ivz = 0d, imass = 0d, 
        dx = 0d, dy = 0d, dz = 0d, jmass = 0d, mag = 0d;
      Int64* stack = stackalloc Int64[SIZE + 1];
      NBody* bi = (NBody*)stack;
      NBody* bj = (NBody*)stack;
      NBody* last = bi + COUNT - 1;
      InitBodies(bi, last);
      byte* callstack = (byte*)(stack + SIZE);
      byte stackptr = 0;
      callstack[stackptr++] = (byte)Method.Finished;
      callstack[stackptr++] = (byte)Method.Advance;
      callstack[stackptr++] = (byte)Method.Energy;
RETURN:
      switch ((Method)callstack[--stackptr]) {
        case Method.Energy:
          mag = 0d;
          bi = (NBody*)stack;
          goto case Method.EnergyOuterLoop;

        case Method.EnergyOuterLoop:
          callstack[stackptr++] = (byte)Method.EnergyInnerLoop;
          goto case Method.DerefBI;

        case Method.EnergyInnerLoop:
          mag += 0.5 * imass * (ivx * ivx + ivy * ivy + ivz * ivz);
          goto case Method.EnergyInnerLoopBody;

        case Method.EnergyInnerLoopBody:
          dx = ix - bj->x;
          dy = iy - bj->y;
          dz = iz - bj->z;
          mag -= imass * bj->mass / Math.Sqrt(dx * dx + dy * dy + dz * dz);
          if (++bj <= last) { goto case Method.EnergyInnerLoopBody; }
          if (++bi <= last) { goto case Method.EnergyOuterLoop; }
          Console.Out.WriteLine(mag.ToString("F9"));
          goto RETURN;

        case Method.Advance:
          bi = (NBody*)stack;
          goto case Method.AdvanceOuterLoop;


        case Method.AdvanceOuterLoop:
          callstack[stackptr++] = (byte)Method.AdvanceInnerLoop;
          goto case Method.DerefBI;

        case Method.AdvanceInnerLoop:
          dx = bj->x - ix;
          dy = bj->y - iy;
          dz = bj->z - iz;
          jmass = bj->mass;
          mag = GetMagnitude(dx, dy, dz);
          bj->vx = bj->vx - dx * imass * mag;
          bj->vy = bj->vy - dy * imass * mag;
          bj->vz = bj->vz - dz * imass * mag;
          ivx = ivx + dx * jmass * mag;
          ivy = ivy + dy * jmass * mag;
          ivz = ivz + dz * jmass * mag;
          if (++bj <= last) { goto case Method.AdvanceInnerLoop; }
          bi->x = ix + ivx * DT;
          bi->y = iy + ivy * DT;
          bi->z = iz + ivz * DT;
          bi->vx = ivx;
          bi->vy = ivy;
          bi->vz = ivz;
          if (++bi < last) { goto case Method.AdvanceOuterLoop; }
          last->x = last->x + last->vx * DT;
          last->y = last->y + last->vy * DT;
          last->z = last->z + last->vz * DT;
          if (--advancements > 0) { goto case Method.Advance; }
          goto case Method.Energy;

        case Method.DerefBI:
          bj = bi + 1;
          ix = bi->x;
          iy = bi->y;
          iz = bi->z;
          ivx = bi->vx;
          ivy = bi->vy;
          ivz = bi->vz;
          imass = bi->mass;
          goto RETURN;

        case Method.Finished:
          break;
      }

    }
  }

  public unsafe static T[] ToArray<T>(T* ptr, int size = 40) where T : unmanaged {
    T[] result = new T[size];
    for (var i = 0; i < size; i++) { result[i] = ptr[i]; }
    return result;
  }

  /// <summary> Apparently minimizing the number of parameters in a function leads to improvements</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static double GetMagnitude(double dx, double dy, double dz) {
    double d2 = dx * dx + dy * dy + dz * dz;
    return DT / (d2 * Math.Sqrt(d2));
  }



  private unsafe static void InitBodies(NBody* bodies, NBody* last) {
    const double Pi = 3.141592653589793;
    const double Solarmass = 4 * Pi * Pi;
    const double DaysPeryear = 365.24;
    unchecked {
      bodies[1] = new NBody { //jupiter
        x = 4.84143144246472090e+00,
        y = -1.16032004402742839e+00,
        z = -1.03622044471123109e-01,
        vx = 1.66007664274403694e-03 * DaysPeryear,
        vy = 7.69901118419740425e-03 * DaysPeryear,
        vz = -6.90460016972063023e-05 * DaysPeryear,
        mass = 9.54791938424326609e-04 * Solarmass
      };
      bodies[2] = new NBody { //saturn
        x = 8.34336671824457987e+00,
        y = 4.12479856412430479e+00,
        z = -4.03523417114321381e-01,
        vx = -2.76742510726862411e-03 * DaysPeryear,
        vy = 4.99852801234917238e-03 * DaysPeryear,
        vz = 2.30417297573763929e-05 * DaysPeryear,
        mass = 2.85885980666130812e-04 * Solarmass
      };
      bodies[3] = new NBody { //uranus           
        x = 1.28943695621391310e+01,
        y = -1.51111514016986312e+01,
        z = -2.23307578892655734e-01,
        vx = 2.96460137564761618e-03 * DaysPeryear,
        vy = 2.37847173959480950e-03 * DaysPeryear,
        vz = -2.96589568540237556e-05 * DaysPeryear,
        mass = 4.36624404335156298e-05 * Solarmass
      };
      bodies[4] = new NBody { //neptune
        x = 1.53796971148509165e+01,
        y = -2.59193146099879641e+01,
        z = 1.79258772950371181e-01,
        vx = 2.68067772490389322e-03 * DaysPeryear,
        vy = 1.62824170038242295e-03 * DaysPeryear,
        vz = -9.51592254519715870e-05 * DaysPeryear,
        mass = 5.15138902046611451e-05 * Solarmass
      };

      double vx = 0, vy = 0, vz = 0;
      for (NBody* planet = bodies + 1; planet <= last; ++planet) {
        double mass = planet->mass;
        vx += planet->vx * mass;
        vy += planet->vy * mass;
        vz += planet->vz * mass;
      }
      bodies->vx = vx / -Solarmass;
      bodies->vy = vy / -Solarmass;
      bodies->vz = vz / -Solarmass;
      bodies->mass = Solarmass;
    }
  }
}