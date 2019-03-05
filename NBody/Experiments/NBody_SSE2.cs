/* The Computer Language Benchmarks Game http://benchmarksgame.alioth.debian.org/
    By Derek Ziemba. 
*/
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

public unsafe static class NBody_SSE2 {
  public static T[] ToArray<T>(T* ptr, int size = 10) where T : unmanaged {
    T[] result = new T[size];
    for (var i = 0; i < size; i++) { result[i] = ptr[i]; }
    return result;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string Fmt(this double val) => val.ToString("F9");

  [StructLayout(LayoutKind.Sequential)]
  unsafe struct NBody {
    public double x, y, z, vx, vy, vz, mass;
    public override string ToString() {
      return $"x={x.Fmt()}, y={y.Fmt()}, z={z.Fmt()}, vx={vx.Fmt()}, vy={vy.Fmt()}, vz={vz.Fmt()}";
    }
  }
  [StructLayout(LayoutKind.Sequential)]
  unsafe struct Delta {
    public double dx, dy, dz;
    public override string ToString() {
      return String.Concat(dx.Fmt(), ", ", dy.Fmt(), ", ", dz.Fmt());
    }
  }


  private const int SIZE = 5;
  private const int N = (SIZE - 1) * SIZE / 2;


  public static void Main(string[] args) {
    unchecked {
      fixed (NBody* bodies = new NBody[5])
      fixed (Delta* r = new Delta[N])
      fixed (double* mag = new double[N]) {
        InitBodies(bodies);

        Energy(bodies);
        int advancements = args.Length > 0 ? Int32.Parse(args[0]) : 1000;
        while (advancements-- > 0) { Advance(bodies, r, mag); }
        Energy(bodies);
      }
    }
  }


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void Advance(NBody* bodies, Delta* r, double* mag) {
    const double DT = 0.01;
    unchecked {
      for (int i = 0, k = 0; i < SIZE - 1; ++i) {
        NBody iBody = bodies[i];
        for (int j = i + 1; j < SIZE; ++j, ++k) {
          r[k].dx = iBody.x - bodies[j].x; 
          r[k].dy = iBody.y - bodies[j].y;
          r[k].dz = iBody.z - bodies[j].z;
        }
      }
      for (int i =0; i < N; i += 2) {
        Vector128<double> dx = default, dy = default, dz = default;
        dx = Sse2.LoadLow(dx, &r[i].dx);
        dy = Sse2.LoadLow(dy, &r[i].dy);
        dz = Sse2.LoadLow(dz, &r[i].dz);

        dx = Sse2.LoadHigh(dx, &r[i+1].dx);
        dy = Sse2.LoadHigh(dy, &r[i+1].dy);
        dz = Sse2.LoadHigh(dz, &r[i+1].dz);

        Vector128<double> dSquared = Sse2.Add(
          Sse2.Add(Sse2.Multiply(dx,dx), Sse2.Multiply(dy,dy)),
          Sse2.Multiply(dz,dz));

        Vector128<double> distance =
          Sse2.ConvertToVector128Double(Sse.ReciprocalSqrt(Sse2.ConvertToVector128Single(dSquared)));

        for(int j = 0; j < 2; ++j) {
          distance = Sse2.Subtract(
            Sse2.Multiply(distance, Sse2.SetAllVector128(1.5)),
            Sse2.Multiply(
                Sse2.Multiply(
                  Sse2.Multiply(Sse2.SetAllVector128(0.5), dSquared), 
                  distance),
                Sse2.Multiply(distance, distance)
              )
            );
        }

        Vector128<double> dmag = Sse2.Multiply(
          Sse2.Divide(Sse2.SetAllVector128(DT), dSquared), distance);

        Sse2.Store(&mag[i], dmag);
      }

      for (int i = 0, k=0; i < SIZE-1; ++i) {
        NBody iBody = bodies[i];
        for(int j= i+1; j<SIZE; ++j, ++k) {
          double imass = bodies[i].mass, jmass = bodies[j].mass, kmag = mag[k];
          iBody.vx -= r[k].dx * jmass * kmag;
          iBody.vy -= r[k].dy * jmass * kmag;
          iBody.vz -= r[k].dz * jmass * kmag;

          bodies[j].vx += r[k].dx * imass * kmag;
          bodies[j].vy += r[k].dy * imass * kmag;
          bodies[j].vz += r[k].dz * imass * kmag;
        }
      }
      for (int i = 0; i < SIZE; ++i) {
        bodies[i].x += DT * bodies[i].vx;
        bodies[i].y += DT * bodies[i].vy;
        bodies[i].z += DT * bodies[i].vz;
      }
    }

  }


  private static void Energy(NBody* ptrSun) {
    unchecked {
      double e = 0.0;
      for (NBody* bi = ptrSun; bi < ptrSun+SIZE; ++bi) {
        double
           imass = bi->mass,
           ix = bi->x,
           iy = bi->y,
           iz = bi->z,
           ivx = bi->vx,
           ivy = bi->vy,
           ivz = bi->vz;
        e += 0.5 * imass * (ivx * ivx + ivy * ivy + ivz * ivz);
        for (NBody* bj = bi + 1; bj < ptrSun + SIZE; ++bj) {
          double
             jmass = bj->mass,
             dx = ix - bj->x,
             dy = iy - bj->y,
             dz = iz - bj->z;
          e -= imass * jmass / Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
      }
      Console.Out.WriteLine(e.Fmt());
    }
  }

  private static void InitBodies(NBody* ptrSun) {
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
        mass = 2.85885980666130812e-04 * Solarmass,
        x = 8.34336671824457987e+00,
        y = 4.12479856412430479e+00,
        z = -4.03523417114321381e-01,
        vx = -2.76742510726862411e-03 * DaysPeryear,
        vy = 4.99852801234917238e-03 * DaysPeryear,
        vz = 2.30417297573763929e-05 * DaysPeryear
      };
      ptrSun[3] = new NBody { //uranus
        mass = 4.36624404335156298e-05 * Solarmass,
        x = 1.28943695621391310e+01,
        y = -1.51111514016986312e+01,
        z = -2.23307578892655734e-01,
        vx = 2.96460137564761618e-03 * DaysPeryear,
        vy = 2.37847173959480950e-03 * DaysPeryear,
        vz = -2.96589568540237556e-05 * DaysPeryear
      };
      ptrSun[4] = new NBody { //neptune
        mass = 5.15138902046611451e-05 * Solarmass,
        x = 1.53796971148509165e+01,
        y = -2.59193146099879641e+01,
        z = 1.79258772950371181e-01,
        vx = 2.68067772490389322e-03 * DaysPeryear,
        vy = 1.62824170038242295e-03 * DaysPeryear,
        vz = -9.51592254519715870e-05 * DaysPeryear
      };

      double vx = 0, vy = 0, vz = 0;
      for (NBody* planet = ptrSun + 1; planet < ptrSun+SIZE; ++planet) {
        double mass = planet->mass;
        vx += planet->vx * mass;
        vy += planet->vy * mass;
        vz += planet->vz * mass;
      }
      ptrSun->mass = Solarmass;
      ptrSun->vx = vx / -Solarmass;
      ptrSun->vy = vy / -Solarmass;
      ptrSun->vz = vz / -Solarmass;
    }
  }
}