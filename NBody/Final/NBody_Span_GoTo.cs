/* The Computer Language Benchmarks Game http://benchmarksgame.alioth.debian.org/
    Originally contributed by Isaac Gouy.
    Optimized to use Structs and Pointers by Derek Ziemba. 
*/
using System;
using System.Runtime.CompilerServices;

public static unsafe class NBody_Span_GoTo {

  private const double DT = 0.01;

  unsafe struct NBody { public double x, y, z, vx, vy, vz, mass; }

  public unsafe static void Main(string[] args) {
    unchecked { Run(args.Length > 0 ? Int32.Parse(args[0]) : 1000); }
  }

  private unsafe static void Run(int advancements) {
    unchecked {
      Span<NBody> bodies = stackalloc NBody[5];
      InitBodies(bodies);
      Energy(bodies);

ADVANCE:
      int i = 0;
OUTERLOOP:
      int j = i + 1;
      double
         ix =  bodies[i].x,
         iy =  bodies[i].y,
         iz =  bodies[i].z,
         ivx = bodies[i].vx,
         ivy = bodies[i].vy,
         ivz = bodies[i].vz,
         imass = bodies[i].mass;
INNERLOOP:
      double
         dx = bodies[j].x - ix,
         dy = bodies[j].y - iy,
         dz = bodies[j].z - iz,
         jmass = bodies[j].mass,
         mag = GetMagnitude(dx, dy, dz);
      bodies[j].vx = bodies[j].vx - dx * imass * mag;
      bodies[j].vy = bodies[j].vy - dy * imass * mag;
      bodies[j].vz = bodies[j].vz - dz * imass * mag;
      ivx = ivx + dx * jmass * mag;
      ivy = ivy + dy * jmass * mag;
      ivz = ivz + dz * jmass * mag;
      if (++j < bodies.Length) { goto INNERLOOP; }
      bodies[i].x = ix + ivx * DT;
      bodies[i].y = iy + ivy * DT;
      bodies[i].z = iz + ivz * DT;
      bodies[i].vx = ivx;
      bodies[i].vy = ivy;
      bodies[i].vz = ivz;
      if (++i < bodies.Length - 1) { goto OUTERLOOP; }
      bodies[i].x = bodies[i].x + bodies[i].vx * DT;
      bodies[i].y = bodies[i].y + bodies[i].vy * DT;
      bodies[i].z = bodies[i].z + bodies[i].vz * DT;
      if (--advancements > 0) { goto ADVANCE; }

      Energy(bodies);
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private unsafe static void Advance(Span<NBody> bodies) {
    unchecked {
      int i = 0;
      for (; i < bodies.Length - 1; ++i) {
        for (int j = i + 1; j < bodies.Length; ++j) {
          double
             dx = bodies[j].x - bodies[i].x,
             dy = bodies[j].y - bodies[i].y,
             dz = bodies[j].z - bodies[i].z,
             mag = GetMagnitude(dx, dy, dz);
          bodies[j].vx = bodies[j].vx - bodies[i].mass * dx * mag;
          bodies[j].vy = bodies[j].vy - bodies[i].mass * dy * mag;
          bodies[j].vz = bodies[j].vz - bodies[i].mass * dz * mag;
          bodies[i].vx = bodies[i].vx + bodies[j].mass * dx * mag;
          bodies[i].vy = bodies[i].vy + bodies[j].mass * dy * mag;
          bodies[i].vz = bodies[i].vz + bodies[j].mass * dz * mag;
        }
        bodies[i].x = bodies[i].x + bodies[i].vx * DT;
        bodies[i].y = bodies[i].y + bodies[i].vy * DT;
        bodies[i].z = bodies[i].z + bodies[i].vz * DT;
      }
      bodies[i].x = bodies[i].x + bodies[i].vx * DT;
      bodies[i].y = bodies[i].y + bodies[i].vy * DT;
      bodies[i].z = bodies[i].z + bodies[i].vz * DT;
    }
  }

  /// <summary> Apparently minimizing the number of parameters in a function leads to improvements... This eliminates d2 from Advance() </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private unsafe static double GetMagnitude(double dx, double dy, double dz) {
    double d2 = dx * dx + dy * dy + dz * dz;
    return DT / (d2 * Math.Sqrt(d2));
  }

  private unsafe static void Energy(Span<NBody> bodies) {
    unchecked {
      double e = 0.0;
      for (int i = 0; i < bodies.Length; ++i) {
        NBody bi = bodies[i];
        e += 0.5 * (bi.vx * bi.vx + bi.vy * bi.vy + bi.vz * bi.vz) * bi.mass;
        for (int j = i + 1; j < bodies.Length; ++j) {
          double
             dx = bi.x - bodies[j].x,
             dy = bi.y - bodies[j].y,
             dz = bi.z - bodies[j].z;
          e -= bodies[j].mass * bi.mass / Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
      }
      Console.Out.WriteLine(e.ToString("F9"));
    }
  }

  private static void InitBodies(Span<NBody> bodies) {
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
      for (int i = 1; i < bodies.Length; ++i) {
        double mass = bodies[i].mass;
        vx += bodies[i].vx * mass;
        vy += bodies[i].vy * mass;
        vz += bodies[i].vz * mass;
      }
      bodies[0].vx = vx / -Solarmass;
      bodies[0].vy = vy / -Solarmass;
      bodies[0].vz = vz / -Solarmass;
      bodies[0].mass = Solarmass;
    }
  }
}