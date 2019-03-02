/* The Computer Language Benchmarks Game http://benchmarksgame.alioth.debian.org/
    By Derek Ziemba. 
*/
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

public unsafe struct NBody_FixedArraysSSE2 {

  private const int SIZE = 5;
  private const int N = (SIZE - 1) * SIZE / 2;
  private const int X = 0;
  private const int Y = 1;
  private const int Z = 2;
  private const int VX = 4;
  private const int VY = 5;
  private const int VZ = 6;
  private const int MASS = 7;
  private const int DX = 0;
  private const int DY = 1;
  private const int DZ = 2;
  private const int SUN = 0;
  private const int JUPITER = 8;
  private const int SATURN = 16;
  private const int URANUS = 24;
  private const int NEPTUNE = 32;
  private const int BODYSIZE = 8;
  private const int SYSTEMSIZE = SIZE * BODYSIZE;

  public fixed double bodies[SIZE * 8];
  public fixed double r[N * 4];
  public fixed double mag[N];

  private void Init() {
    const double Pi = 3.141592653589793;
    const double Solarmass = 4 * Pi * Pi;
    const double DaysPeryear = 365.24;
    unchecked {
      fixed (double* bods = bodies) { 
      bodies[SUN + MASS] = Solarmass;

      bodies[JUPITER + X   ] = 4.84143144246472090e+00;
      bodies[JUPITER + Y   ]= -1.16032004402742839e+00;
      bodies[JUPITER + Z   ]= -1.03622044471123109e-01;
      bodies[JUPITER + VX  ]= 1.66007664274403694e-03 * DaysPeryear;
      bodies[JUPITER + VY  ]= 7.69901118419740425e-03 * DaysPeryear;
      bodies[JUPITER + VZ  ]= -6.90460016972063023e-05 * DaysPeryear;
      bodies[JUPITER + MASS] = 9.54791938424326609e-04 * Solarmass;

      bodies[SATURN + X] = 8.34336671824457987e+00;
      bodies[SATURN + Y] = 4.12479856412430479e+00;
      bodies[SATURN + Z] = -4.03523417114321381e-01;
      bodies[SATURN + VX] = -2.76742510726862411e-03 * DaysPeryear;
      bodies[SATURN + VY] = 4.99852801234917238e-03 * DaysPeryear;
      bodies[SATURN + VZ] = 2.30417297573763929e-05 * DaysPeryear;
      bodies[SATURN + MASS] = 2.85885980666130812e-04 * Solarmass;
      
      bodies[URANUS + X] = 1.28943695621391310e+01;
      bodies[URANUS + Y] = -1.51111514016986312e+01;
      bodies[URANUS + Z] = -2.23307578892655734e-01;
      bodies[URANUS + VX] = 2.96460137564761618e-03 * DaysPeryear;
      bodies[URANUS + VY] = 2.37847173959480950e-03 * DaysPeryear;
      bodies[URANUS + VZ] = -2.96589568540237556e-05 * DaysPeryear;
      bodies[URANUS + MASS] = 4.36624404335156298e-05 * Solarmass;
    
      bodies[NEPTUNE + X] = 1.53796971148509165e+01;
      bodies[NEPTUNE + Y] = -2.59193146099879641e+01;
      bodies[NEPTUNE + Z] = 1.79258772950371181e-01;
      bodies[NEPTUNE + VX] = 2.68067772490389322e-03 * DaysPeryear;
      bodies[NEPTUNE + VY] = 1.62824170038242295e-03 * DaysPeryear;
      bodies[NEPTUNE + VZ] = -9.51592254519715870e-05 * DaysPeryear;
      bodies[NEPTUNE + MASS] = 5.15138902046611451e-05 * Solarmass;

      for (int planet = SUN + BODYSIZE; planet < SYSTEMSIZE; planet += BODYSIZE) {
        bodies[SUN + VX] += bodies[planet + VX] * bodies[planet + MASS];
        bodies[SUN + VY] += bodies[planet + VY] * bodies[planet + MASS];
        bodies[SUN + VZ] += bodies[planet + VZ] * bodies[planet + MASS];
      }

      bodies[SUN + VX] /= -Solarmass;
      bodies[SUN + VY] /= -Solarmass;
      bodies[SUN + VZ] /= -Solarmass;
      }
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private void Advance(double dt) {
    unchecked {
      fixed (double* bodies = this.bodies)
      fixed (double* r = this.r)
      fixed (double* mag = this.mag) {

        for (int i = 0, k = 0; i < SIZE - 1; ++i) {
          for (int j = i + 1; j < SIZE; ++j, ++k) {
            r[k + DX] = bodies[i + X] - bodies[j + X];
            r[k + DY] = bodies[i + Y] - bodies[j + Y];
            r[k + DZ] = bodies[i + Z] - bodies[j + Z];
          }
        }

        for (int i = 0; i < N; i += 1) {
          Vector128<double> dx = default, dy = default, dz = default;
          dx = Sse2.LoadLow(dx, &r[i + DX]);
          dy = Sse2.LoadLow(dy, &r[i + DY]);
          dz = Sse2.LoadLow(dz, &r[i + DZ]);
          i++;
          dx = Sse2.LoadHigh(dx, &r[i + DX]);
          dy = Sse2.LoadHigh(dy, &r[i + DY]);
          dz = Sse2.LoadHigh(dz, &r[i + DZ]);

          Vector128<double> dSquared = Sse2.Add(
            Sse2.Add(Sse2.Multiply(dx, dx), Sse2.Multiply(dy, dy)),
            Sse2.Multiply(dz, dz));

          Vector128<double> distance =
            Sse2.ConvertToVector128Double(Sse.ReciprocalSqrt(Sse2.ConvertToVector128Single(dSquared)));

          for (int j = 0; j < 2; ++j) {
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
            Sse2.Divide(Sse2.SetAllVector128(dt), dSquared), distance);

          Sse2.Store(&mag[i-1], dmag);
        }
      }

      for (int i = 0, k = 0; i < SIZE - 1; ++i) {
        for (int j = i + 1; j < SIZE; ++j, ++k) {
          bodies[i + VX] -= r[k + DX] * bodies[j + MASS] * mag[k];
          bodies[i + VY] -= r[k + DY] * bodies[j + MASS] * mag[k];
          bodies[i + VZ] -= r[k + DZ] * bodies[j + MASS] * mag[k];

          bodies[j + VX] += r[k + DX] * bodies[i + MASS] * mag[k];
          bodies[j + VY] += r[k + DY] * bodies[i + MASS] * mag[k];
          bodies[j + VZ] += r[k + DZ] * bodies[i + MASS] * mag[k];
        }
      }
      for (int i = 0; i < SIZE; ++i) {
        bodies[i + X] += dt * bodies[i + VX];
        bodies[i + Y] += dt * bodies[i + VY];
        bodies[i + Z] += dt * bodies[i + VZ];
      }
    }

  }


  private double Energy() {
    unchecked {
      double e = 0.0;
      for (int planet = SUN; planet < SYSTEMSIZE; planet += BODYSIZE) {
        double
           ix = bodies[planet + X],
           iy = bodies[planet + Y],
           iz = bodies[planet + Z],
           ivx = bodies[planet + VX],
           ivy = bodies[planet + VY],
           ivz = bodies[planet + VZ],
           imass = bodies[planet + MASS];
        e += 0.5 * imass * (ivx * ivx + ivy * ivy + ivz * ivz);
        for(int j = planet + BODYSIZE; j < SYSTEMSIZE; j += BODYSIZE) {
          double
             dx = ix - bodies[j + X],
             dy = iy - bodies[j + Y],
             dz = iz - bodies[j + Z];
          e -= imass * bodies[j + MASS] / Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
      }
      return e;
    }
  }

  /// <summary> Apparently minimizing the number of parameters in a function leads to improvements... This eliminates d2 from Advance() </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static double GetMagnitude(double dx, double dy, double dz) {
    double d2 = dx * dx + dy * dy + dz * dz;
    return d2 * Math.Sqrt(d2);
  }

  public static void Main(string[] args) {
    unchecked {
      NBody_FixedArraysSSE2 system = new NBody_FixedArraysSSE2();
      system.Init();

      Console.Out.WriteLine(system.Energy().ToString("F9"));
      int advancements = args.Length > 0 ? Int32.Parse(args[0]) : 1000;
      while (advancements-- > 0) {
        system.Advance(0.01d);
      }
      Console.Out.WriteLine(system.Energy().ToString("F9"));
    }
  }

  public static T[] ToArray<T>(T* ptr, int size = 10) where T : unmanaged {
    T[] result = new T[size];
    for (var i = 0; i < size; i++) { result[i] = ptr[i]; }
    return result;
  }

}