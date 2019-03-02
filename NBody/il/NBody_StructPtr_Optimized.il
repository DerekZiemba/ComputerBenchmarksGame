// Type: NBody_StructPtr_Optimized
// Assembly: NBody, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AE708C84-2F5F-465A-999C-8CE4F1597C78
// Location: A:\Dropbox\GitHub\ComputerBenchmarksGame\NBody\bin\Release\netcoreapp2.2\NBody.dll
// Sequence point data from A:\Dropbox\GitHub\ComputerBenchmarksGame\NBody\bin\Release\netcoreapp2.2\NBody.pdb

.class public abstract sealed auto ansi beforefieldinit
  NBody_StructPtr_Optimized
    extends [System.Runtime]System.Object
{

  .class nested private sealed sequential ansi beforefieldinit
    NBody
      extends [System.Runtime]System.ValueType
  {

    .field public float64 x

    .field public float64 y

    .field public float64 z

    .field public float64 vx

    .field public float64 vy

    .field public float64 vz

    .field public float64 mass
  } // end of class NBody

  .method public hidebysig static void
    Main(
      string[] args
    ) cil managed
  {
    .maxstack 3
    .locals init (
      [0] valuetype NBody_StructPtr_Optimized/NBody* bodies,
      [1] valuetype NBody_StructPtr_Optimized/NBody* last,
      [2] int32 advancements,
      [3] float64 V_3
    )

    // [13 7 - 13 43]
    IL_0000: ldc.i4.5
    IL_0001: conv.u
    IL_0002: sizeof       NBody_StructPtr_Optimized/NBody
    IL_0008: mul.ovf.un
    IL_0009: localloc
    IL_000b: stloc.0      // bodies

    // [14 7 - 14 32]
    IL_000c: ldloc.0      // bodies
    IL_000d: ldc.i4.4
    IL_000e: conv.i
    IL_000f: sizeof       NBody_StructPtr_Optimized/NBody
    IL_0015: mul
    IL_0016: add
    IL_0017: stloc.1      // last

    // [15 7 - 15 32]
    IL_0018: ldloc.0      // bodies
    IL_0019: ldloc.1      // last
    IL_001a: call         void NBody_StructPtr_Optimized::InitBodies(valuetype NBody_StructPtr_Optimized/NBody*, valuetype NBody_StructPtr_Optimized/NBody*)

    // [17 7 - 17 66]
    IL_001f: call         class [System.Runtime.Extensions]System.IO.TextWriter [System.Console]System.Console::get_Out()
    IL_0024: ldloc.0      // bodies
    IL_0025: ldloc.1      // last
    IL_0026: call         float64 NBody_StructPtr_Optimized::Energy(valuetype NBody_StructPtr_Optimized/NBody*, valuetype NBody_StructPtr_Optimized/NBody*)
    IL_002b: stloc.3      // V_3
    IL_002c: ldloca.s     V_3
    IL_002e: ldstr        "F9"
    IL_0033: call         instance string [System.Runtime]System.Double::ToString(string)
    IL_0038: callvirt     instance void [System.Runtime.Extensions]System.IO.TextWriter::WriteLine(string)

    // [19 7 - 19 72]
    IL_003d: ldarg.0      // args
    IL_003e: ldlen
    IL_003f: brtrue.s     IL_0048
    IL_0041: ldc.i4       1000 // 0x000003e8
    IL_0046: br.s         IL_0050
    IL_0048: ldarg.0      // args
    IL_0049: ldc.i4.0
    IL_004a: ldelem.ref
    IL_004b: call         int32 [System.Runtime]System.Int32::Parse(string)
    IL_0050: stloc.2      // advancements

    IL_0051: br.s         IL_0063
    // start of loop, entry point: IL_0063

      // [21 9 - 21 38]
      IL_0053: ldloc.0      // bodies
      IL_0054: ldloc.1      // last
      IL_0055: ldc.r8       0.01
      IL_005e: call         void NBody_StructPtr_Optimized::Advance(valuetype NBody_StructPtr_Optimized/NBody*, valuetype NBody_StructPtr_Optimized/NBody*, float64)

      // [20 7 - 20 33]
      IL_0063: ldloc.2      // advancements
      IL_0064: dup
      IL_0065: ldc.i4.1
      IL_0066: sub
      IL_0067: stloc.2      // advancements
      IL_0068: ldc.i4.0
      IL_0069: bgt.s        IL_0053
    // end of loop

    // [23 7 - 23 66]
    IL_006b: call         class [System.Runtime.Extensions]System.IO.TextWriter [System.Console]System.Console::get_Out()
    IL_0070: ldloc.0      // bodies
    IL_0071: ldloc.1      // last
    IL_0072: call         float64 NBody_StructPtr_Optimized::Energy(valuetype NBody_StructPtr_Optimized/NBody*, valuetype NBody_StructPtr_Optimized/NBody*)
    IL_0077: stloc.3      // V_3
    IL_0078: ldloca.s     V_3
    IL_007a: ldstr        "F9"
    IL_007f: call         instance string [System.Runtime]System.Double::ToString(string)
    IL_0084: callvirt     instance void [System.Runtime.Extensions]System.IO.TextWriter::WriteLine(string)

    // [25 3 - 25 4]
    IL_0089: ret

  } // end of method NBody_StructPtr_Optimized::Main

  .method private hidebysig static float64
    GetMagnitude(
      float64 dx,
      float64 dy,
      float64 dz
    ) cil managed
  {
    .maxstack 8

    // [31 5 - 31 45]
    IL_0000: ldarg.0      // dx
    IL_0001: ldarg.0      // dx
    IL_0002: mul
    IL_0003: ldarg.1      // dy
    IL_0004: ldarg.1      // dy
    IL_0005: mul
    IL_0006: add
    IL_0007: ldarg.2      // dz
    IL_0008: ldarg.2      // dz
    IL_0009: mul
    IL_000a: add

    // [32 5 - 32 31]
    IL_000b: dup
    IL_000c: call         float64 [System.Runtime.Extensions]System.Math::Sqrt(float64)
    IL_0011: mul
    IL_0012: ret

  } // end of method NBody_StructPtr_Optimized::GetMagnitude

  .method private hidebysig static void
    Advance(
      valuetype NBody_StructPtr_Optimized/NBody* bi,
      valuetype NBody_StructPtr_Optimized/NBody* last,
      float64 dt
    ) cil managed
  {
    .maxstack 4
    .locals init (
      [0] float64 ix,
      [1] float64 iy,
      [2] float64 iz,
      [3] float64 ivx,
      [4] float64 ivy,
      [5] float64 ivz,
      [6] float64 imass,
      [7] valuetype NBody_StructPtr_Optimized/NBody* bj,
      [8] float64 dx,
      [9] float64 dy,
      [10] float64 dz,
      [11] float64 jmass,
      [12] float64 mag
    )

    // start of loop, entry point: IL_0000
      // [40 7 - 41 20]
      IL_0000: ldarg.0      // bi
      IL_0001: ldfld        float64 NBody_StructPtr_Optimized/NBody::x
      IL_0006: stloc.0      // ix

      // [42 10 - 42 20]
      IL_0007: ldarg.0      // bi
      IL_0008: ldfld        float64 NBody_StructPtr_Optimized/NBody::y
      IL_000d: stloc.1      // iy

      // [43 10 - 43 20]
      IL_000e: ldarg.0      // bi
      IL_000f: ldfld        float64 NBody_StructPtr_Optimized/NBody::z
      IL_0014: stloc.2      // iz

      // [44 10 - 44 22]
      IL_0015: ldarg.0      // bi
      IL_0016: ldfld        float64 NBody_StructPtr_Optimized/NBody::vx
      IL_001b: stloc.3      // ivx

      // [45 10 - 45 22]
      IL_001c: ldarg.0      // bi
      IL_001d: ldfld        float64 NBody_StructPtr_Optimized/NBody::vy
      IL_0022: stloc.s      ivy

      // [46 10 - 46 22]
      IL_0024: ldarg.0      // bi
      IL_0025: ldfld        float64 NBody_StructPtr_Optimized/NBody::vz
      IL_002a: stloc.s      ivz

      // [47 10 - 47 26]
      IL_002c: ldarg.0      // bi
      IL_002d: ldfld        float64 NBody_StructPtr_Optimized/NBody::mass
      IL_0032: stloc.s      imass

      // [48 7 - 48 26]
      IL_0034: ldarg.0      // bi
      IL_0035: sizeof       NBody_StructPtr_Optimized/NBody
      IL_003b: add
      IL_003c: stloc.s      bj
      // start of loop, entry point: IL_003e

        // [50 7 - 51 25]
        IL_003e: ldloc.s      bj
        IL_0040: ldfld        float64 NBody_StructPtr_Optimized/NBody::x
        IL_0045: ldloc.0      // ix
        IL_0046: sub
        IL_0047: stloc.s      dx

        // [52 10 - 52 25]
        IL_0049: ldloc.s      bj
        IL_004b: ldfld        float64 NBody_StructPtr_Optimized/NBody::y
        IL_0050: ldloc.1      // iy
        IL_0051: sub
        IL_0052: stloc.s      dy

        // [53 10 - 53 25]
        IL_0054: ldloc.s      bj
        IL_0056: ldfld        float64 NBody_StructPtr_Optimized/NBody::z
        IL_005b: ldloc.2      // iz
        IL_005c: sub
        IL_005d: stloc.s      dz

        // [54 10 - 54 26]
        IL_005f: ldloc.s      bj
        IL_0061: ldfld        float64 NBody_StructPtr_Optimized/NBody::mass
        IL_0066: stloc.s      jmass

        // [55 10 - 55 45]
        IL_0068: ldarg.2      // dt
        IL_0069: ldloc.s      dx
        IL_006b: ldloc.s      dy
        IL_006d: ldloc.s      dz
        IL_006f: call         float64 NBody_StructPtr_Optimized::GetMagnitude(float64, float64, float64)
        IL_0074: div
        IL_0075: stloc.s      mag

        // [56 7 - 56 42]
        IL_0077: ldloc.s      bj
        IL_0079: ldloc.s      bj
        IL_007b: ldfld        float64 NBody_StructPtr_Optimized/NBody::vx
        IL_0080: ldloc.s      dx
        IL_0082: ldloc.s      imass
        IL_0084: mul
        IL_0085: ldloc.s      mag
        IL_0087: mul
        IL_0088: sub
        IL_0089: stfld        float64 NBody_StructPtr_Optimized/NBody::vx

        // [57 7 - 57 42]
        IL_008e: ldloc.s      bj
        IL_0090: ldloc.s      bj
        IL_0092: ldfld        float64 NBody_StructPtr_Optimized/NBody::vy
        IL_0097: ldloc.s      dy
        IL_0099: ldloc.s      imass
        IL_009b: mul
        IL_009c: ldloc.s      mag
        IL_009e: mul
        IL_009f: sub
        IL_00a0: stfld        float64 NBody_StructPtr_Optimized/NBody::vy

        // [58 7 - 58 42]
        IL_00a5: ldloc.s      bj
        IL_00a7: ldloc.s      bj
        IL_00a9: ldfld        float64 NBody_StructPtr_Optimized/NBody::vz
        IL_00ae: ldloc.s      dz
        IL_00b0: ldloc.s      imass
        IL_00b2: mul
        IL_00b3: ldloc.s      mag
        IL_00b5: mul
        IL_00b6: sub
        IL_00b7: stfld        float64 NBody_StructPtr_Optimized/NBody::vz

        // [59 7 - 59 36]
        IL_00bc: ldloc.3      // ivx
        IL_00bd: ldloc.s      dx
        IL_00bf: ldloc.s      jmass
        IL_00c1: mul
        IL_00c2: ldloc.s      mag
        IL_00c4: mul
        IL_00c5: add
        IL_00c6: stloc.3      // ivx

        // [60 7 - 60 36]
        IL_00c7: ldloc.s      ivy
        IL_00c9: ldloc.s      dy
        IL_00cb: ldloc.s      jmass
        IL_00cd: mul
        IL_00ce: ldloc.s      mag
        IL_00d0: mul
        IL_00d1: add
        IL_00d2: stloc.s      ivy

        // [61 7 - 61 36]
        IL_00d4: ldloc.s      ivz
        IL_00d6: ldloc.s      dz
        IL_00d8: ldloc.s      jmass
        IL_00da: mul
        IL_00db: ldloc.s      mag
        IL_00dd: mul
        IL_00de: add
        IL_00df: stloc.s      ivz

        // [62 7 - 62 24]
        IL_00e1: ldloc.s      bj
        IL_00e3: sizeof       NBody_StructPtr_Optimized/NBody
        IL_00e9: add
        IL_00ea: dup
        IL_00eb: stloc.s      bj
        IL_00ed: ldarg.1      // last
        IL_00ee: ble.un       IL_003e
      // end of loop

      // [63 7 - 63 20]
      IL_00f3: ldarg.0      // bi
      IL_00f4: ldloc.3      // ivx
      IL_00f5: stfld        float64 NBody_StructPtr_Optimized/NBody::vx

      // [64 7 - 64 20]
      IL_00fa: ldarg.0      // bi
      IL_00fb: ldloc.s      ivy
      IL_00fd: stfld        float64 NBody_StructPtr_Optimized/NBody::vy

      // [65 7 - 65 20]
      IL_0102: ldarg.0      // bi
      IL_0103: ldloc.s      ivz
      IL_0105: stfld        float64 NBody_StructPtr_Optimized/NBody::vz

      // [66 7 - 66 29]
      IL_010a: ldarg.0      // bi
      IL_010b: ldloc.0      // ix
      IL_010c: ldloc.3      // ivx
      IL_010d: ldarg.2      // dt
      IL_010e: mul
      IL_010f: add
      IL_0110: stfld        float64 NBody_StructPtr_Optimized/NBody::x

      // [67 7 - 67 29]
      IL_0115: ldarg.0      // bi
      IL_0116: ldloc.1      // iy
      IL_0117: ldloc.s      ivy
      IL_0119: ldarg.2      // dt
      IL_011a: mul
      IL_011b: add
      IL_011c: stfld        float64 NBody_StructPtr_Optimized/NBody::y

      // [68 7 - 68 29]
      IL_0121: ldarg.0      // bi
      IL_0122: ldloc.2      // iz
      IL_0123: ldloc.s      ivz
      IL_0125: ldarg.2      // dt
      IL_0126: mul
      IL_0127: add
      IL_0128: stfld        float64 NBody_StructPtr_Optimized/NBody::z

      // [69 7 - 69 23]
      IL_012d: ldarg.0      // bi
      IL_012e: sizeof       NBody_StructPtr_Optimized/NBody
      IL_0134: add
      IL_0135: dup
      IL_0136: starg.s      bi
      IL_0138: ldarg.1      // last
      IL_0139: blt.un       IL_0000
    // end of loop

    // [70 7 - 70 41]
    IL_013e: ldarg.1      // last
    IL_013f: ldarg.1      // last
    IL_0140: ldfld        float64 NBody_StructPtr_Optimized/NBody::x
    IL_0145: ldarg.1      // last
    IL_0146: ldfld        float64 NBody_StructPtr_Optimized/NBody::vx
    IL_014b: ldarg.2      // dt
    IL_014c: mul
    IL_014d: add
    IL_014e: stfld        float64 NBody_StructPtr_Optimized/NBody::x

    // [71 7 - 71 41]
    IL_0153: ldarg.1      // last
    IL_0154: ldarg.1      // last
    IL_0155: ldfld        float64 NBody_StructPtr_Optimized/NBody::y
    IL_015a: ldarg.1      // last
    IL_015b: ldfld        float64 NBody_StructPtr_Optimized/NBody::vy
    IL_0160: ldarg.2      // dt
    IL_0161: mul
    IL_0162: add
    IL_0163: stfld        float64 NBody_StructPtr_Optimized/NBody::y

    // [72 7 - 72 41]
    IL_0168: ldarg.1      // last
    IL_0169: ldarg.1      // last
    IL_016a: ldfld        float64 NBody_StructPtr_Optimized/NBody::z
    IL_016f: ldarg.1      // last
    IL_0170: ldfld        float64 NBody_StructPtr_Optimized/NBody::vz
    IL_0175: ldarg.2      // dt
    IL_0176: mul
    IL_0177: add
    IL_0178: stfld        float64 NBody_StructPtr_Optimized/NBody::z

    // [74 3 - 74 4]
    IL_017d: ret

  } // end of method NBody_StructPtr_Optimized::Advance

  .method private hidebysig static float64
    Energy(
      valuetype NBody_StructPtr_Optimized/NBody* bi,
      valuetype NBody_StructPtr_Optimized/NBody* last
    ) cil managed
  {
    .maxstack 5
    .locals init (
      [0] float64 e,
      [1] float64 ix,
      [2] float64 iy,
      [3] float64 iz,
      [4] float64 ivx,
      [5] float64 ivy,
      [6] float64 ivz,
      [7] float64 imass,
      [8] valuetype NBody_StructPtr_Optimized/NBody* bj,
      [9] float64 dx,
      [10] float64 dy,
      [11] float64 dz
    )

    // [80 7 - 80 22]
    IL_0000: ldc.r8       0.0
    IL_0009: stloc.0      // e

    IL_000a: br           IL_00d0
    // start of loop, entry point: IL_00d0

      // [82 9 - 83 22]
      IL_000f: ldarg.0      // bi
      IL_0010: ldfld        float64 NBody_StructPtr_Optimized/NBody::x
      IL_0015: stloc.1      // ix

      // [84 12 - 84 22]
      IL_0016: ldarg.0      // bi
      IL_0017: ldfld        float64 NBody_StructPtr_Optimized/NBody::y
      IL_001c: stloc.2      // iy

      // [85 12 - 85 22]
      IL_001d: ldarg.0      // bi
      IL_001e: ldfld        float64 NBody_StructPtr_Optimized/NBody::z
      IL_0023: stloc.3      // iz

      // [86 12 - 86 24]
      IL_0024: ldarg.0      // bi
      IL_0025: ldfld        float64 NBody_StructPtr_Optimized/NBody::vx
      IL_002a: stloc.s      ivx

      // [87 12 - 87 24]
      IL_002c: ldarg.0      // bi
      IL_002d: ldfld        float64 NBody_StructPtr_Optimized/NBody::vy
      IL_0032: stloc.s      ivy

      // [88 12 - 88 24]
      IL_0034: ldarg.0      // bi
      IL_0035: ldfld        float64 NBody_StructPtr_Optimized/NBody::vz
      IL_003a: stloc.s      ivz

      // [89 12 - 89 28]
      IL_003c: ldarg.0      // bi
      IL_003d: ldfld        float64 NBody_StructPtr_Optimized/NBody::mass
      IL_0042: stloc.s      imass

      // [90 9 - 90 64]
      IL_0044: ldloc.0      // e
      IL_0045: ldc.r8       0.5
      IL_004e: ldloc.s      imass
      IL_0050: mul
      IL_0051: ldloc.s      ivx
      IL_0053: ldloc.s      ivx
      IL_0055: mul
      IL_0056: ldloc.s      ivy
      IL_0058: ldloc.s      ivy
      IL_005a: mul
      IL_005b: add
      IL_005c: ldloc.s      ivz
      IL_005e: ldloc.s      ivz
      IL_0060: mul
      IL_0061: add
      IL_0062: mul
      IL_0063: add
      IL_0064: stloc.0      // e

      // [91 14 - 91 32]
      IL_0065: ldarg.0      // bi
      IL_0066: sizeof       NBody_StructPtr_Optimized/NBody
      IL_006c: add
      IL_006d: stloc.s      bj

      IL_006f: br.s         IL_00c1
      // start of loop, entry point: IL_00c1

        // [92 11 - 93 29]
        IL_0071: ldloc.1      // ix
        IL_0072: ldloc.s      bj
        IL_0074: ldfld        float64 NBody_StructPtr_Optimized/NBody::x
        IL_0079: sub
        IL_007a: stloc.s      dx

        // [94 14 - 94 29]
        IL_007c: ldloc.2      // iy
        IL_007d: ldloc.s      bj
        IL_007f: ldfld        float64 NBody_StructPtr_Optimized/NBody::y
        IL_0084: sub
        IL_0085: stloc.s      dy

        // [95 14 - 95 29]
        IL_0087: ldloc.3      // iz
        IL_0088: ldloc.s      bj
        IL_008a: ldfld        float64 NBody_StructPtr_Optimized/NBody::z
        IL_008f: sub
        IL_0090: stloc.s      dz

        // [96 11 - 96 74]
        IL_0092: ldloc.0      // e
        IL_0093: ldloc.s      imass
        IL_0095: ldloc.s      bj
        IL_0097: ldfld        float64 NBody_StructPtr_Optimized/NBody::mass
        IL_009c: mul
        IL_009d: ldloc.s      dx
        IL_009f: ldloc.s      dx
        IL_00a1: mul
        IL_00a2: ldloc.s      dy
        IL_00a4: ldloc.s      dy
        IL_00a6: mul
        IL_00a7: add
        IL_00a8: ldloc.s      dz
        IL_00aa: ldloc.s      dz
        IL_00ac: mul
        IL_00ad: add
        IL_00ae: call         float64 [System.Runtime.Extensions]System.Math::Sqrt(float64)
        IL_00b3: div
        IL_00b4: sub
        IL_00b5: stloc.0      // e

        // [91 46 - 91 50]
        IL_00b6: ldloc.s      bj
        IL_00b8: sizeof       NBody_StructPtr_Optimized/NBody
        IL_00be: add
        IL_00bf: stloc.s      bj

        // [91 34 - 91 44]
        IL_00c1: ldloc.s      bj
        IL_00c3: ldarg.1      // last
        IL_00c4: ble.un.s     IL_0071
      // end of loop

      // [81 26 - 81 30]
      IL_00c6: ldarg.0      // bi
      IL_00c7: sizeof       NBody_StructPtr_Optimized/NBody
      IL_00cd: add
      IL_00ce: starg.s      bi

      // [81 14 - 81 24]
      IL_00d0: ldarg.0      // bi
      IL_00d1: ldarg.1      // last
      IL_00d2: ble.un       IL_000f
    // end of loop

    // [99 7 - 99 16]
    IL_00d7: ldloc.0      // e
    IL_00d8: ret

  } // end of method NBody_StructPtr_Optimized::Energy

  .method private hidebysig static void
    InitBodies(
      valuetype NBody_StructPtr_Optimized/NBody* bodies,
      valuetype NBody_StructPtr_Optimized/NBody* last
    ) cil managed
  {
    .maxstack 3
    .locals init (
      [0] float64 vx,
      [1] float64 vy,
      [2] float64 vz,
      [3] valuetype NBody_StructPtr_Optimized/NBody V_3,
      [4] valuetype NBody_StructPtr_Optimized/NBody* planet,
      [5] float64 mass
    )

    // [108 7 - 116 9]
    IL_0000: ldarg.0      // bodies
    IL_0001: sizeof       NBody_StructPtr_Optimized/NBody
    IL_0007: add
    IL_0008: ldloca.s     V_3
    IL_000a: initobj      NBody_StructPtr_Optimized/NBody
    IL_0010: ldloca.s     V_3
    IL_0012: ldc.r8       4.84143144246472
    IL_001b: stfld        float64 NBody_StructPtr_Optimized/NBody::x
    IL_0020: ldloca.s     V_3
    IL_0022: ldc.r8       -1.16032004402743
    IL_002b: stfld        float64 NBody_StructPtr_Optimized/NBody::y
    IL_0030: ldloca.s     V_3
    IL_0032: ldc.r8       -0.103622044471123
    IL_003b: stfld        float64 NBody_StructPtr_Optimized/NBody::z
    IL_0040: ldloca.s     V_3
    IL_0042: ldc.r8       0.606326392995832
    IL_004b: stfld        float64 NBody_StructPtr_Optimized/NBody::vx
    IL_0050: ldloca.s     V_3
    IL_0052: ldc.r8       2.81198684491626
    IL_005b: stfld        float64 NBody_StructPtr_Optimized/NBody::vy
    IL_0060: ldloca.s     V_3
    IL_0062: ldc.r8       -0.0252183616598876
    IL_006b: stfld        float64 NBody_StructPtr_Optimized/NBody::vz
    IL_0070: ldloca.s     V_3
    IL_0072: ldc.r8       0.0376936748703895
    IL_007b: stfld        float64 NBody_StructPtr_Optimized/NBody::mass
    IL_0080: ldloc.3      // V_3
    IL_0081: stobj        NBody_StructPtr_Optimized/NBody

    // [117 7 - 125 9]
    IL_0086: ldarg.0      // bodies
    IL_0087: ldc.i4.2
    IL_0088: conv.i
    IL_0089: sizeof       NBody_StructPtr_Optimized/NBody
    IL_008f: mul
    IL_0090: add
    IL_0091: ldloca.s     V_3
    IL_0093: initobj      NBody_StructPtr_Optimized/NBody
    IL_0099: ldloca.s     V_3
    IL_009b: ldc.r8       8.34336671824458
    IL_00a4: stfld        float64 NBody_StructPtr_Optimized/NBody::x
    IL_00a9: ldloca.s     V_3
    IL_00ab: ldc.r8       4.1247985641243
    IL_00b4: stfld        float64 NBody_StructPtr_Optimized/NBody::y
    IL_00b9: ldloca.s     V_3
    IL_00bb: ldc.r8       -0.403523417114321
    IL_00c4: stfld        float64 NBody_StructPtr_Optimized/NBody::z
    IL_00c9: ldloca.s     V_3
    IL_00cb: ldc.r8       -1.01077434617879
    IL_00d4: stfld        float64 NBody_StructPtr_Optimized/NBody::vx
    IL_00d9: ldloca.s     V_3
    IL_00db: ldc.r8       1.82566237123041
    IL_00e4: stfld        float64 NBody_StructPtr_Optimized/NBody::vy
    IL_00e9: ldloca.s     V_3
    IL_00eb: ldc.r8       0.00841576137658415
    IL_00f4: stfld        float64 NBody_StructPtr_Optimized/NBody::vz
    IL_00f9: ldloca.s     V_3
    IL_00fb: ldc.r8       0.0112863261319688
    IL_0104: stfld        float64 NBody_StructPtr_Optimized/NBody::mass
    IL_0109: ldloc.3      // V_3
    IL_010a: stobj        NBody_StructPtr_Optimized/NBody

    // [126 7 - 134 9]
    IL_010f: ldarg.0      // bodies
    IL_0110: ldc.i4.3
    IL_0111: conv.i
    IL_0112: sizeof       NBody_StructPtr_Optimized/NBody
    IL_0118: mul
    IL_0119: add
    IL_011a: ldloca.s     V_3
    IL_011c: initobj      NBody_StructPtr_Optimized/NBody
    IL_0122: ldloca.s     V_3
    IL_0124: ldc.r8       12.8943695621391
    IL_012d: stfld        float64 NBody_StructPtr_Optimized/NBody::x
    IL_0132: ldloca.s     V_3
    IL_0134: ldc.r8       -15.1111514016986
    IL_013d: stfld        float64 NBody_StructPtr_Optimized/NBody::y
    IL_0142: ldloca.s     V_3
    IL_0144: ldc.r8       -0.223307578892656
    IL_014d: stfld        float64 NBody_StructPtr_Optimized/NBody::z
    IL_0152: ldloca.s     V_3
    IL_0154: ldc.r8       1.08279100644154
    IL_015d: stfld        float64 NBody_StructPtr_Optimized/NBody::vx
    IL_0162: ldloca.s     V_3
    IL_0164: ldc.r8       0.868713018169608
    IL_016d: stfld        float64 NBody_StructPtr_Optimized/NBody::vy
    IL_0172: ldloca.s     V_3
    IL_0174: ldc.r8       -0.0108326374013636
    IL_017d: stfld        float64 NBody_StructPtr_Optimized/NBody::vz
    IL_0182: ldloca.s     V_3
    IL_0184: ldc.r8       0.00172372405705971
    IL_018d: stfld        float64 NBody_StructPtr_Optimized/NBody::mass
    IL_0192: ldloc.3      // V_3
    IL_0193: stobj        NBody_StructPtr_Optimized/NBody

    // [135 7 - 143 9]
    IL_0198: ldarg.0      // bodies
    IL_0199: ldc.i4.4
    IL_019a: conv.i
    IL_019b: sizeof       NBody_StructPtr_Optimized/NBody
    IL_01a1: mul
    IL_01a2: add
    IL_01a3: ldloca.s     V_3
    IL_01a5: initobj      NBody_StructPtr_Optimized/NBody
    IL_01ab: ldloca.s     V_3
    IL_01ad: ldc.r8       15.3796971148509
    IL_01b6: stfld        float64 NBody_StructPtr_Optimized/NBody::x
    IL_01bb: ldloca.s     V_3
    IL_01bd: ldc.r8       -25.919314609988
    IL_01c6: stfld        float64 NBody_StructPtr_Optimized/NBody::y
    IL_01cb: ldloca.s     V_3
    IL_01cd: ldc.r8       0.179258772950371
    IL_01d6: stfld        float64 NBody_StructPtr_Optimized/NBody::z
    IL_01db: ldloca.s     V_3
    IL_01dd: ldc.r8       0.979090732243898
    IL_01e6: stfld        float64 NBody_StructPtr_Optimized/NBody::vx
    IL_01eb: ldloca.s     V_3
    IL_01ed: ldc.r8       0.594698998647676
    IL_01f6: stfld        float64 NBody_StructPtr_Optimized/NBody::vy
    IL_01fb: ldloca.s     V_3
    IL_01fd: ldc.r8       -0.0347559555040781
    IL_0206: stfld        float64 NBody_StructPtr_Optimized/NBody::vz
    IL_020b: ldloca.s     V_3
    IL_020d: ldc.r8       0.00203368686992463
    IL_0216: stfld        float64 NBody_StructPtr_Optimized/NBody::mass
    IL_021b: ldloc.3      // V_3
    IL_021c: stobj        NBody_StructPtr_Optimized/NBody

    // [145 7 - 145 20]
    IL_0221: ldc.r8       0.0
    IL_022a: stloc.0      // vx

    // [145 22 - 145 28]
    IL_022b: ldc.r8       0.0
    IL_0234: stloc.1      // vy

    // [145 30 - 145 36]
    IL_0235: ldc.r8       0.0
    IL_023e: stloc.2      // vz

    // [146 12 - 146 38]
    IL_023f: ldarg.0      // bodies
    IL_0240: sizeof       NBody_StructPtr_Optimized/NBody
    IL_0246: add
    IL_0247: stloc.s      planet

    IL_0249: br.s         IL_0286
    // start of loop, entry point: IL_0286

      // [147 9 - 147 36]
      IL_024b: ldloc.s      planet
      IL_024d: ldfld        float64 NBody_StructPtr_Optimized/NBody::mass
      IL_0252: stloc.s      mass

      // [148 9 - 148 33]
      IL_0254: ldloc.0      // vx
      IL_0255: ldloc.s      planet
      IL_0257: ldfld        float64 NBody_StructPtr_Optimized/NBody::vx
      IL_025c: ldloc.s      mass
      IL_025e: mul
      IL_025f: add
      IL_0260: stloc.0      // vx

      // [149 9 - 149 33]
      IL_0261: ldloc.1      // vy
      IL_0262: ldloc.s      planet
      IL_0264: ldfld        float64 NBody_StructPtr_Optimized/NBody::vy
      IL_0269: ldloc.s      mass
      IL_026b: mul
      IL_026c: add
      IL_026d: stloc.1      // vy

      // [150 9 - 150 33]
      IL_026e: ldloc.2      // vz
      IL_026f: ldloc.s      planet
      IL_0271: ldfld        float64 NBody_StructPtr_Optimized/NBody::vz
      IL_0276: ldloc.s      mass
      IL_0278: mul
      IL_0279: add
      IL_027a: stloc.2      // vz

      // [146 56 - 146 64]
      IL_027b: ldloc.s      planet
      IL_027d: sizeof       NBody_StructPtr_Optimized/NBody
      IL_0283: add
      IL_0284: stloc.s      planet

      // [146 40 - 146 54]
      IL_0286: ldloc.s      planet
      IL_0288: ldarg.1      // last
      IL_0289: ble.un.s     IL_024b
    // end of loop

    // [152 7 - 152 36]
    IL_028b: ldarg.0      // bodies
    IL_028c: ldloc.0      // vx
    IL_028d: ldc.r8       -39.4784176043574
    IL_0296: div
    IL_0297: stfld        float64 NBody_StructPtr_Optimized/NBody::vx

    // [153 7 - 153 36]
    IL_029c: ldarg.0      // bodies
    IL_029d: ldloc.1      // vy
    IL_029e: ldc.r8       -39.4784176043574
    IL_02a7: div
    IL_02a8: stfld        float64 NBody_StructPtr_Optimized/NBody::vy

    // [154 7 - 154 36]
    IL_02ad: ldarg.0      // bodies
    IL_02ae: ldloc.2      // vz
    IL_02af: ldc.r8       -39.4784176043574
    IL_02b8: div
    IL_02b9: stfld        float64 NBody_StructPtr_Optimized/NBody::vz

    // [155 7 - 155 32]
    IL_02be: ldarg.0      // bodies
    IL_02bf: ldc.r8       39.4784176043574
    IL_02c8: stfld        float64 NBody_StructPtr_Optimized/NBody::mass

    // [157 3 - 157 4]
    IL_02cd: ret

  } // end of method NBody_StructPtr_Optimized::InitBodies
} // end of class NBody_StructPtr_Optimized
