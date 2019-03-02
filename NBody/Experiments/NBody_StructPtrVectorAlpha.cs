/* The Computer Language Benchmarks Game http://benchmarksgame.alioth.debian.org/
    Originally contributed by Isaac Gouy.
    Optimized to use Structs and Pointers by Derek Ziemba. 
*/
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Numerics;


public unsafe struct NBody_StructPtrVectorAlpha {
   [StructLayout(LayoutKind.Explicit)] struct NBody {
      [FieldOffset(0)] public double x;
      [FieldOffset(8)] public double y;
      [FieldOffset(16)] public double z;
      [FieldOffset(32)] public double vx;
      [FieldOffset(40)] public double vy;
      [FieldOffset(48)] public double vz;
      [FieldOffset(56)] public double mass;
   }

   private static double[] arrdubs3 = new double[3];
   private static Vector<double> ipos = new Vector<double>(arrdubs3);
   private static Vector<double> ivel = new Vector<double>(arrdubs3);
   private static Vector<double> jpos = new Vector<double>(arrdubs3);
   private static Vector<double> jvel = new Vector<double>(arrdubs3);

   public static void Main(string[] args) {
      int count = Vector<double>.Count;
      unchecked {
         NBody* ptrSun = stackalloc NBody[5];   // There doesn't seem to be a difference between 
         NBody* ptrEnd = ptrSun+4;              // using stackalloc or fixed pointer.  
         InitBodies(ptrSun, ptrEnd);            // stackalloc uses fewer lines though. 

         Energy(ptrSun, ptrEnd);

         int advancements = args.Length>0 ? Int32.Parse(args[0]) : 1000;
         while (advancements-- > 0) {
            Advance(ptrSun, ptrEnd, 0.01d);
         }
         Energy(ptrSun, ptrEnd);
      }
   }


   /// <summary> Apparently minimizing the number of parameters in a function leads to improvements... This eliminates d2 from Advance() </summary>
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private static double GetMagnitude(double dx, double dy, double dz) {
      double d2 = dx * dx + dy * dy + dz * dz;
      return d2 * Math.Sqrt(d2);
   }


   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private static void Advance(NBody* ptrSun, NBody* ptrEnd, double distance) {
      unchecked {
         for (NBody* bi = ptrSun; bi < ptrEnd; ++bi) {
            double
               ix = bi->x,
               iy = bi->y,
               iz = bi->z,
               ivx = bi->vx,
               ivy = bi->vy,
               ivz = bi->vz,
               imass = bi->mass;
            for (NBody* bj = bi + 1; bj <= ptrEnd; ++bj) {
               Vector<double> jpos = new Vector<double>(new []{bj->x, bj->y, bj->z });

               double
                  dx = bj->x - ix,
                  dy = bj->y - iy,
                  dz = bj->z - iz,
                  jmass = bj->mass,
                  mag = distance / GetMagnitude(dx, dy, dz);
               bj->vx = bj->vx - dx * imass * mag;
               bj->vy = bj->vy - dy * imass * mag;
               bj->vz = bj->vz - dz * imass * mag;
               ivx = ivx + dx * jmass * mag;
               ivy = ivy + dy * jmass * mag;
               ivz = ivz + dz * jmass * mag;
            }
            bi->vx = ivx;
            bi->vy = ivy;
            bi->vz = ivz;
            bi->x = ix + ivx * distance;
            bi->y = iy + ivy * distance;
            bi->z = iz + ivz * distance;
         }
         ptrEnd->x = ptrEnd->x + ptrEnd->vx * distance;
         ptrEnd->y = ptrEnd->y + ptrEnd->vy * distance;
         ptrEnd->z = ptrEnd->z + ptrEnd->vz * distance;
      }
   }


   private static void Energy(NBody* ptrSun, NBody* ptrEnd) {
      unchecked {
         double e = 0.0;
         for (NBody* bi = ptrSun; bi <= ptrEnd; ++bi) {
            double
               imass = bi->mass,
               ix = bi->x,
               iy = bi->y,
               iz = bi->z,
               ivx = bi->vx,
               ivy = bi->vy,
               ivz = bi->vz;
            e += 0.5 * imass * (ivx * ivx + ivy * ivy + ivz * ivz);
            for (NBody* bj = bi + 1; bj <= ptrEnd; ++bj) {
               double
                  jmass = bj->mass,
                  dx = ix - bj->x,
                  dy = iy - bj->y,
                  dz = iz - bj->z;
               e -= imass * jmass / Math.Sqrt(dx * dx + dy * dy + dz * dz);
            }
         }
         Console.Out.WriteLine(e.ToString("F9"));
      }
   }

   private static void InitBodies(NBody* ptrSun, NBody* ptrEnd) {
      const double Pi = 3.141592653589793;
      const double Solarmass = 4 * Pi * Pi;
      const double DaysPeryear = 365.24;
      unchecked {
         ptrSun[ 1 ] = new NBody { //jupiter
            mass = 9.54791938424326609e-04 * Solarmass,
            x = 4.84143144246472090e+00,
            y = -1.16032004402742839e+00,
            z = -1.03622044471123109e-01,
            vx = 1.66007664274403694e-03 * DaysPeryear,
            vy = 7.69901118419740425e-03 * DaysPeryear,
            vz = -6.90460016972063023e-05 * DaysPeryear
         };
         ptrSun[ 2 ] = new NBody { //saturn
            mass = 2.85885980666130812e-04 * Solarmass,
            x = 8.34336671824457987e+00,
            y = 4.12479856412430479e+00,
            z = -4.03523417114321381e-01,
            vx = -2.76742510726862411e-03 * DaysPeryear,
            vy = 4.99852801234917238e-03 * DaysPeryear,
            vz = 2.30417297573763929e-05 * DaysPeryear
         };
         ptrSun[ 3 ] = new NBody { //uranus
            mass = 4.36624404335156298e-05 * Solarmass,
            x = 1.28943695621391310e+01,
            y = -1.51111514016986312e+01,
            z = -2.23307578892655734e-01,
            vx = 2.96460137564761618e-03 * DaysPeryear,
            vy = 2.37847173959480950e-03 * DaysPeryear,
            vz = -2.96589568540237556e-05 * DaysPeryear
         };
         ptrSun[ 4 ] = new NBody { //neptune
            mass = 5.15138902046611451e-05 * Solarmass,
            x = 1.53796971148509165e+01,
            y = -2.59193146099879641e+01,
            z = 1.79258772950371181e-01,
            vx = 2.68067772490389322e-03 * DaysPeryear,
            vy = 1.62824170038242295e-03 * DaysPeryear,
            vz = -9.51592254519715870e-05 * DaysPeryear
         };

         double vx=0, vy=0, vz = 0;
         for (NBody* planet = ptrSun + 1; planet <= ptrEnd; ++planet) {
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