/* The Computer Language Benchmarks Game http://benchmarksgame.alioth.debian.org/
    Originally contributed by Isaac Gouy.
    Optimized to use Structs and Pointers by Derek Ziemba. 
*/
using System;
using System.Runtime.CompilerServices;

public static unsafe class NBody_StructPtr_SSE {
  struct NBody {
    public double x, y, z, vx, vy, vz, mass;
  }

  private const int SIZE = 5;
  private const int N = (SIZE - 1) * SIZE / 2;

  public static void Main(string[] args) {
    unchecked {
      NBody* ptrSun = stackalloc NBody[SIZE];
      NBody* ptrEnd = ptrSun + SIZE - 1;     
      InitBodies(ptrSun, ptrEnd);            

      Console.Out.WriteLine(Energy(ptrSun, ptrEnd).ToString("F9"));

      double* mags = stackalloc double[N];
      int advancements = args.Length > 0 ? Int32.Parse(args[0]) : 1000;
      while (advancements-- > 0) {
        Advance(ptrSun, ptrEnd, mags, 0.01d);
      }
      Console.Out.WriteLine(Energy(ptrSun, ptrEnd).ToString("F9"));
    }
  }


  /// <summary> Apparently minimizing the number of parameters in a function leads to improvements... This eliminates d2 from Advance() </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static double GetMagnitude(double dx, double dy, double dz) {
    double d2 = dx * dx + dy * dy + dz * dz;
    return d2 * Math.Sqrt(d2);
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void Advance(NBody* ptrSun, NBody* ptrEnd, double* mags, double dt) {
    unchecked {
      for (NBody* bi = ptrSun; bi < ptrEnd; ++bi) {
        // Dereference common variables now so they're likely to
        // get stored in registers. The performance advantage is
        // lost if pointers are dereferenced every time. Accounts for ~7%. 
        double
           ix = bi->x,
           iy = bi->y,
           iz = bi->z,
           ivx = bi->vx,
           ivy = bi->vy,
           ivz = bi->vz,
           imass = bi->mass;


        for (NBody* bj = bi + 1; bj <= ptrEnd; ++bj) {
          double
             dx = bj->x - ix,
             dy = bj->y - iy,
             dz = bj->z - iz,
             vx = bj->vx,
             vy = bj->vy,
             vz = bj->vz,
             jmass = bj->mass,
             mag = dt / GetMagnitude(dx, dy, dz);
          bj->vx =vx - dx * imass * mag;
          bj->vy =vy - dy * imass * mag;
          bj->vz =vz - dz * imass * mag;
          ivx = ivx + dx * jmass * mag;
          ivy = ivy + dy * jmass * mag;
          ivz = ivz + dz * jmass * mag;
        }
        bi->x = ix + ivx * dt;
        bi->y = iy + ivy * dt;
        bi->z = iz + ivz * dt;
        bi->vx = ivx;
        bi->vy = ivy;
        bi->vz = ivz;
      }
      ptrEnd->x = ptrEnd->x + ptrEnd->vx * dt;
      ptrEnd->y = ptrEnd->y + ptrEnd->vy * dt;
      ptrEnd->z = ptrEnd->z + ptrEnd->vz * dt;
    }
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static double Energy(NBody* ptrSun, NBody* ptrEnd) {
    unchecked {
      double e = 0.0;
      for (NBody* bi = ptrSun; bi <= ptrEnd; ++bi) {
        double
           ix = bi->x,
           iy = bi->y,
           iz = bi->z,
           ivx = bi->vx,
           ivy = bi->vy,
           ivz = bi->vz,
           imass = bi->mass;
        e += 0.5 * imass * (ivx * ivx + ivy * ivy + ivz * ivz);
        for (NBody* bj = bi + 1; bj <= ptrEnd; ++bj) {
          double
             dx = ix - bj->x,
             dy = iy - bj->y,
             dz = iz - bj->z;
          e -= imass * bj->mass / Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
      }
      return e;
    }
  }

  private static void InitBodies(NBody* ptrSun, NBody* ptrEnd) {
    const double Pi = 3.141592653589793;
    const double Solarmass = 4 * Pi * Pi;
    const double DaysPeryear = 365.24;
    unchecked {
      ptrSun[1] = new NBody { //jupiter
        x = 4.84143144246472090e+00,
        y = -1.16032004402742839e+00,
        z = -1.03622044471123109e-01,
        vx = 1.66007664274403694e-03 * DaysPeryear,
        vy = 7.69901118419740425e-03 * DaysPeryear,
        vz = -6.90460016972063023e-05 * DaysPeryear,
        mass = 9.54791938424326609e-04 * Solarmass
      };
      ptrSun[2] = new NBody { //saturn
        x = 8.34336671824457987e+00,
        y = 4.12479856412430479e+00,
        z = -4.03523417114321381e-01,
        vx = -2.76742510726862411e-03 * DaysPeryear,
        vy = 4.99852801234917238e-03 * DaysPeryear,
        vz = 2.30417297573763929e-05 * DaysPeryear,
        mass = 2.85885980666130812e-04 * Solarmass
      };
      ptrSun[3] = new NBody { //uranus           
        x = 1.28943695621391310e+01,
        y = -1.51111514016986312e+01,
        z = -2.23307578892655734e-01,
        vx = 2.96460137564761618e-03 * DaysPeryear,
        vy = 2.37847173959480950e-03 * DaysPeryear,
        vz = -2.96589568540237556e-05 * DaysPeryear,
        mass = 4.36624404335156298e-05 * Solarmass
      };
      ptrSun[4] = new NBody { //neptune
        x = 1.53796971148509165e+01,
        y = -2.59193146099879641e+01,
        z = 1.79258772950371181e-01,
        vx = 2.68067772490389322e-03 * DaysPeryear,
        vy = 1.62824170038242295e-03 * DaysPeryear,
        vz = -9.51592254519715870e-05 * DaysPeryear,
        mass = 5.15138902046611451e-05 * Solarmass
      };

      double vx = 0, vy = 0, vz = 0;
      for (NBody* planet = ptrSun + 1; planet <= ptrEnd; ++planet) {
        double mass = planet->mass;
        vx += planet->vx * mass;
        vy += planet->vy * mass;
        vz += planet->vz * mass;
      }     
      ptrSun->vx = vx / -Solarmass;
      ptrSun->vy = vy / -Solarmass;
      ptrSun->vz = vz / -Solarmass;
      ptrSun->mass = Solarmass;
    }
  }
}