// Type: NBody_StructPtr_Official
// Assembly: NBody, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76A2C9E4-73C4-49F0-A9DD-DC2B7A760286
// Location: A:\Dropbox\GitHub\ComputerBenchmarksGame\NBody\bin\Release\netcoreapp2.2\NBody.dll
// Sequence point data from A:\Dropbox\GitHub\ComputerBenchmarksGame\NBody\bin\Release\netcoreapp2.2\NBody.pdb

.class public abstract sealed auto ansi beforefieldinit
  NBody_StructPtr_Official
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
      [0] valuetype NBody_StructPtr_Official/NBody* bodies,
      [1] valuetype NBody_StructPtr_Official/NBody* last,
      [2] int32 advancements,
      [3] float64 V_3
    )

    // [13 7 - 13 43]
    IL_0000: ldc.i4.5
    IL_0001: conv.u
    IL_0002: sizeof       NBody_StructPtr_Official/NBody
    IL_0008: mul.ovf.un
    IL_0009: localloc
    IL_000b: stloc.0      // bodies

    // [14 7 - 14 32]
    IL_000c: ldloc.0      // bodies
    IL_000d: ldc.i4.4
    IL_000e: conv.i
    IL_000f: sizeof       NBody_StructPtr_Official/NBody
    IL_0015: mul
    IL_0016: add
    IL_0017: stloc.1      // last

    // [15 7 - 15 32]
    IL_0018: ldloc.0      // bodies
    IL_0019: ldloc.1      // last
    IL_001a: call         void NBody_StructPtr_Official::InitBodies(valuetype NBody_StructPtr_Official/NBody*, valuetype NBody_StructPtr_Official/NBody*)

    // [17 7 - 17 66]
    IL_001f: call         class [System.Runtime.Extensions]System.IO.TextWriter [System.Console]System.Console::get_Out()
    IL_0024: ldloc.0      // bodies
    IL_0025: ldloc.1      // last
    IL_0026: call         float64 NBody_StructPtr_Official::Energy(valuetype NBody_StructPtr_Official/NBody*, valuetype NBody_StructPtr_Official/NBody*)
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
      IL_005e: call         void NBody_StructPtr_Official::Advance(valuetype NBody_StructPtr_Official/NBody*, valuetype NBody_StructPtr_Official/NBody*, float64)

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
    IL_0072: call         float64 NBody_StructPtr_Official::Energy(valuetype NBody_StructPtr_Official/NBody*, valuetype NBody_StructPtr_Official/NBody*)
    IL_0077: stloc.3      // V_3
    IL_0078: ldloca.s     V_3
    IL_007a: ldstr        "F9"
    IL_007f: call         instance string [System.Runtime]System.Double::ToString(string)
    IL_0084: callvirt     instance void [System.Runtime.Extensions]System.IO.TextWriter::WriteLine(string)

    // [25 3 - 25 4]
    IL_0089: ret

  } // end of method NBody_StructPtr_Official::Main

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

  } // end of method NBody_StructPtr_Official::GetMagnitude

  .method private hidebysig static void
    Advance(
      valuetype NBody_StructPtr_Official/NBody* bodies,
      valuetype NBody_StructPtr_Official/NBody* last,
      float64 dt
    ) cil managed
  {
    .maxstack 4
    .locals init (
      [0] valuetype NBody_StructPtr_Official/NBody* bi,
      [1] float64 ix,
      [2] float64 iy,
      [3] float64 iz,
      [4] float64 ivx,
      [5] float64 ivy,
      [6] float64 ivz,
      [7] float64 imass,
      [8] valuetype NBody_StructPtr_Official/NBody* bj,
      [9] float64 dx,
      [10] float64 dy,
      [11] float64 dz,
      [12] float64 jmass,
      [13] float64 mag
    )

    // [39 12 - 39 30]
    IL_0000: ldarg.0      // bodies
    IL_0001: stloc.0      // bi

    IL_0002: br           IL_0148
    // start of loop, entry point: IL_0148

      // [43 9 - 44 22]
      IL_0007: ldloc.0      // bi
      IL_0008: ldfld        float64 NBody_StructPtr_Official/NBody::x
      IL_000d: stloc.1      // ix

      // [45 12 - 45 22]
      IL_000e: ldloc.0      // bi
      IL_000f: ldfld        float64 NBody_StructPtr_Official/NBody::y
      IL_0014: stloc.2      // iy

      // [46 12 - 46 22]
      IL_0015: ldloc.0      // bi
      IL_0016: ldfld        float64 NBody_StructPtr_Official/NBody::z
      IL_001b: stloc.3      // iz

      // [47 12 - 47 24]
      IL_001c: ldloc.0      // bi
      IL_001d: ldfld        float64 NBody_StructPtr_Official/NBody::vx
      IL_0022: stloc.s      ivx

      // [48 12 - 48 24]
      IL_0024: ldloc.0      // bi
      IL_0025: ldfld        float64 NBody_StructPtr_Official/NBody::vy
      IL_002a: stloc.s      ivy

      // [49 12 - 49 24]
      IL_002c: ldloc.0      // bi
      IL_002d: ldfld        float64 NBody_StructPtr_Official/NBody::vz
      IL_0032: stloc.s      ivz

      // [50 12 - 50 28]
      IL_0034: ldloc.0      // bi
      IL_0035: ldfld        float64 NBody_StructPtr_Official/NBody::mass
      IL_003a: stloc.s      imass

      // [51 14 - 51 32]
      IL_003c: ldloc.0      // bi
      IL_003d: sizeof       NBody_StructPtr_Official/NBody
      IL_0043: add
      IL_0044: stloc.s      bj

      IL_0046: br           IL_00fb
      // start of loop, entry point: IL_00fb

        // [52 11 - 53 29]
        IL_004b: ldloc.s      bj
        IL_004d: ldfld        float64 NBody_StructPtr_Official/NBody::x
        IL_0052: ldloc.1      // ix
        IL_0053: sub
        IL_0054: stloc.s      dx

        // [54 14 - 54 29]
        IL_0056: ldloc.s      bj
        IL_0058: ldfld        float64 NBody_StructPtr_Official/NBody::y
        IL_005d: ldloc.2      // iy
        IL_005e: sub
        IL_005f: stloc.s      dy

        // [55 14 - 55 29]
        IL_0061: ldloc.s      bj
        IL_0063: ldfld        float64 NBody_StructPtr_Official/NBody::z
        IL_0068: ldloc.3      // iz
        IL_0069: sub
        IL_006a: stloc.s      dz

        // [56 14 - 56 30]
        IL_006c: ldloc.s      bj
        IL_006e: ldfld        float64 NBody_StructPtr_Official/NBody::mass
        IL_0073: stloc.s      jmass

        // [57 14 - 57 49]
        IL_0075: ldarg.2      // dt
        IL_0076: ldloc.s      dx
        IL_0078: ldloc.s      dy
        IL_007a: ldloc.s      dz
        IL_007c: call         float64 NBody_StructPtr_Official::GetMagnitude(float64, float64, float64)
        IL_0081: div
        IL_0082: stloc.s      mag

        // [58 11 - 58 46]
        IL_0084: ldloc.s      bj
        IL_0086: ldloc.s      bj
        IL_0088: ldfld        float64 NBody_StructPtr_Official/NBody::vx
        IL_008d: ldloc.s      dx
        IL_008f: ldloc.s      imass
        IL_0091: mul
        IL_0092: ldloc.s      mag
        IL_0094: mul
        IL_0095: sub
        IL_0096: stfld        float64 NBody_StructPtr_Official/NBody::vx

        // [59 11 - 59 46]
        IL_009b: ldloc.s      bj
        IL_009d: ldloc.s      bj
        IL_009f: ldfld        float64 NBody_StructPtr_Official/NBody::vy
        IL_00a4: ldloc.s      dy
        IL_00a6: ldloc.s      imass
        IL_00a8: mul
        IL_00a9: ldloc.s      mag
        IL_00ab: mul
        IL_00ac: sub
        IL_00ad: stfld        float64 NBody_StructPtr_Official/NBody::vy

        // [60 11 - 60 46]
        IL_00b2: ldloc.s      bj
        IL_00b4: ldloc.s      bj
        IL_00b6: ldfld        float64 NBody_StructPtr_Official/NBody::vz
        IL_00bb: ldloc.s      dz
        IL_00bd: ldloc.s      imass
        IL_00bf: mul
        IL_00c0: ldloc.s      mag
        IL_00c2: mul
        IL_00c3: sub
        IL_00c4: stfld        float64 NBody_StructPtr_Official/NBody::vz

        // [61 11 - 61 40]
        IL_00c9: ldloc.s      ivx
        IL_00cb: ldloc.s      dx
        IL_00cd: ldloc.s      jmass
        IL_00cf: mul
        IL_00d0: ldloc.s      mag
        IL_00d2: mul
        IL_00d3: add
        IL_00d4: stloc.s      ivx

        // [62 11 - 62 40]
        IL_00d6: ldloc.s      ivy
        IL_00d8: ldloc.s      dy
        IL_00da: ldloc.s      jmass
        IL_00dc: mul
        IL_00dd: ldloc.s      mag
        IL_00df: mul
        IL_00e0: add
        IL_00e1: stloc.s      ivy

        // [63 11 - 63 40]
        IL_00e3: ldloc.s      ivz
        IL_00e5: ldloc.s      dz
        IL_00e7: ldloc.s      jmass
        IL_00e9: mul
        IL_00ea: ldloc.s      mag
        IL_00ec: mul
        IL_00ed: add
        IL_00ee: stloc.s      ivz

        // [51 46 - 51 50]
        IL_00f0: ldloc.s      bj
        IL_00f2: sizeof       NBody_StructPtr_Official/NBody
        IL_00f8: add
        IL_00f9: stloc.s      bj

        // [51 34 - 51 44]
        IL_00fb: ldloc.s      bj
        IL_00fd: ldarg.1      // last
        IL_00fe: ble.un       IL_004b
      // end of loop

      // [65 9 - 65 22]
      IL_0103: ldloc.0      // bi
      IL_0104: ldloc.s      ivx
      IL_0106: stfld        float64 NBody_StructPtr_Official/NBody::vx

      // [66 9 - 66 22]
      IL_010b: ldloc.0      // bi
      IL_010c: ldloc.s      ivy
      IL_010e: stfld        float64 NBody_StructPtr_Official/NBody::vy

      // [67 9 - 67 22]
      IL_0113: ldloc.0      // bi
      IL_0114: ldloc.s      ivz
      IL_0116: stfld        float64 NBody_StructPtr_Official/NBody::vz

      // [68 9 - 68 31]
      IL_011b: ldloc.0      // bi
      IL_011c: ldloc.1      // ix
      IL_011d: ldloc.s      ivx
      IL_011f: ldarg.2      // dt
      IL_0120: mul
      IL_0121: add
      IL_0122: stfld        float64 NBody_StructPtr_Official/NBody::x

      // [69 9 - 69 31]
      IL_0127: ldloc.0      // bi
      IL_0128: ldloc.2      // iy
      IL_0129: ldloc.s      ivy
      IL_012b: ldarg.2      // dt
      IL_012c: mul
      IL_012d: add
      IL_012e: stfld        float64 NBody_StructPtr_Official/NBody::y

      // [70 9 - 70 31]
      IL_0133: ldloc.0      // bi
      IL_0134: ldloc.3      // iz
      IL_0135: ldloc.s      ivz
      IL_0137: ldarg.2      // dt
      IL_0138: mul
      IL_0139: add
      IL_013a: stfld        float64 NBody_StructPtr_Official/NBody::z

      // [39 43 - 39 47]
      IL_013f: ldloc.0      // bi
      IL_0140: sizeof       NBody_StructPtr_Official/NBody
      IL_0146: add
      IL_0147: stloc.0      // bi

      // [39 32 - 39 41]
      IL_0148: ldloc.0      // bi
      IL_0149: ldarg.1      // last
      IL_014a: blt.un       IL_0007
    // end of loop

    // [72 7 - 72 41]
    IL_014f: ldarg.1      // last
    IL_0150: ldarg.1      // last
    IL_0151: ldfld        float64 NBody_StructPtr_Official/NBody::x
    IL_0156: ldarg.1      // last
    IL_0157: ldfld        float64 NBody_StructPtr_Official/NBody::vx
    IL_015c: ldarg.2      // dt
    IL_015d: mul
    IL_015e: add
    IL_015f: stfld        float64 NBody_StructPtr_Official/NBody::x

    // [73 7 - 73 41]
    IL_0164: ldarg.1      // last
    IL_0165: ldarg.1      // last
    IL_0166: ldfld        float64 NBody_StructPtr_Official/NBody::y
    IL_016b: ldarg.1      // last
    IL_016c: ldfld        float64 NBody_StructPtr_Official/NBody::vy
    IL_0171: ldarg.2      // dt
    IL_0172: mul
    IL_0173: add
    IL_0174: stfld        float64 NBody_StructPtr_Official/NBody::y

    // [74 7 - 74 41]
    IL_0179: ldarg.1      // last
    IL_017a: ldarg.1      // last
    IL_017b: ldfld        float64 NBody_StructPtr_Official/NBody::z
    IL_0180: ldarg.1      // last
    IL_0181: ldfld        float64 NBody_StructPtr_Official/NBody::vz
    IL_0186: ldarg.2      // dt
    IL_0187: mul
    IL_0188: add
    IL_0189: stfld        float64 NBody_StructPtr_Official/NBody::z

    // [76 3 - 76 4]
    IL_018e: ret

  } // end of method NBody_StructPtr_Official::Advance

  .method private hidebysig static float64
    Energy(
      valuetype NBody_StructPtr_Official/NBody* bodies,
      valuetype NBody_StructPtr_Official/NBody* last
    ) cil managed
  {
    .maxstack 5
    .locals init (
      [0] float64 e,
      [1] valuetype NBody_StructPtr_Official/NBody* bi,
      [2] float64 imass,
      [3] float64 ix,
      [4] float64 iy,
      [5] float64 iz,
      [6] float64 ivx,
      [7] float64 ivy,
      [8] float64 ivz,
      [9] valuetype NBody_StructPtr_Official/NBody* bj,
      [10] float64 jmass,
      [11] float64 dx,
      [12] float64 dy,
      [13] float64 dz
    )

    // [82 7 - 82 22]
    IL_0000: ldc.r8       0.0
    IL_0009: stloc.0      // e

    // [83 12 - 83 30]
    IL_000a: ldarg.0      // bodies
    IL_000b: stloc.1      // bi

    IL_000c: br           IL_00d6
    // start of loop, entry point: IL_00d6

      // [84 9 - 85 28]
      IL_0011: ldloc.1      // bi
      IL_0012: ldfld        float64 NBody_StructPtr_Official/NBody::mass
      IL_0017: stloc.2      // imass

      // [86 12 - 86 22]
      IL_0018: ldloc.1      // bi
      IL_0019: ldfld        float64 NBody_StructPtr_Official/NBody::x
      IL_001e: stloc.3      // ix

      // [87 12 - 87 22]
      IL_001f: ldloc.1      // bi
      IL_0020: ldfld        float64 NBody_StructPtr_Official/NBody::y
      IL_0025: stloc.s      iy

      // [88 12 - 88 22]
      IL_0027: ldloc.1      // bi
      IL_0028: ldfld        float64 NBody_StructPtr_Official/NBody::z
      IL_002d: stloc.s      iz

      // [89 12 - 89 24]
      IL_002f: ldloc.1      // bi
      IL_0030: ldfld        float64 NBody_StructPtr_Official/NBody::vx
      IL_0035: stloc.s      ivx

      // [90 12 - 90 24]
      IL_0037: ldloc.1      // bi
      IL_0038: ldfld        float64 NBody_StructPtr_Official/NBody::vy
      IL_003d: stloc.s      ivy

      // [91 12 - 91 24]
      IL_003f: ldloc.1      // bi
      IL_0040: ldfld        float64 NBody_StructPtr_Official/NBody::vz
      IL_0045: stloc.s      ivz

      // [92 9 - 92 64]
      IL_0047: ldloc.0      // e
      IL_0048: ldc.r8       0.5
      IL_0051: ldloc.2      // imass
      IL_0052: mul
      IL_0053: ldloc.s      ivx
      IL_0055: ldloc.s      ivx
      IL_0057: mul
      IL_0058: ldloc.s      ivy
      IL_005a: ldloc.s      ivy
      IL_005c: mul
      IL_005d: add
      IL_005e: ldloc.s      ivz
      IL_0060: ldloc.s      ivz
      IL_0062: mul
      IL_0063: add
      IL_0064: mul
      IL_0065: add
      IL_0066: stloc.0      // e

      // [93 14 - 93 32]
      IL_0067: ldloc.1      // bi
      IL_0068: sizeof       NBody_StructPtr_Official/NBody
      IL_006e: add
      IL_006f: stloc.s      bj

      IL_0071: br.s         IL_00c8
      // start of loop, entry point: IL_00c8

        // [94 11 - 95 30]
        IL_0073: ldloc.s      bj
        IL_0075: ldfld        float64 NBody_StructPtr_Official/NBody::mass
        IL_007a: stloc.s      jmass

        // [96 14 - 96 29]
        IL_007c: ldloc.3      // ix
        IL_007d: ldloc.s      bj
        IL_007f: ldfld        float64 NBody_StructPtr_Official/NBody::x
        IL_0084: sub
        IL_0085: stloc.s      dx

        // [97 14 - 97 29]
        IL_0087: ldloc.s      iy
        IL_0089: ldloc.s      bj
        IL_008b: ldfld        float64 NBody_StructPtr_Official/NBody::y
        IL_0090: sub
        IL_0091: stloc.s      dy

        // [98 14 - 98 29]
        IL_0093: ldloc.s      iz
        IL_0095: ldloc.s      bj
        IL_0097: ldfld        float64 NBody_StructPtr_Official/NBody::z
        IL_009c: sub
        IL_009d: stloc.s      dz

        // [99 11 - 99 71]
        IL_009f: ldloc.0      // e
        IL_00a0: ldloc.2      // imass
        IL_00a1: ldloc.s      jmass
        IL_00a3: mul
        IL_00a4: ldloc.s      dx
        IL_00a6: ldloc.s      dx
        IL_00a8: mul
        IL_00a9: ldloc.s      dy
        IL_00ab: ldloc.s      dy
        IL_00ad: mul
        IL_00ae: add
        IL_00af: ldloc.s      dz
        IL_00b1: ldloc.s      dz
        IL_00b3: mul
        IL_00b4: add
        IL_00b5: call         float64 [System.Runtime.Extensions]System.Math::Sqrt(float64)
        IL_00ba: div
        IL_00bb: sub
        IL_00bc: stloc.0      // e

        // [93 46 - 93 50]
        IL_00bd: ldloc.s      bj
        IL_00bf: sizeof       NBody_StructPtr_Official/NBody
        IL_00c5: add
        IL_00c6: stloc.s      bj

        // [93 34 - 93 44]
        IL_00c8: ldloc.s      bj
        IL_00ca: ldarg.1      // last
        IL_00cb: ble.un.s     IL_0073
      // end of loop

      // [83 44 - 83 48]
      IL_00cd: ldloc.1      // bi
      IL_00ce: sizeof       NBody_StructPtr_Official/NBody
      IL_00d4: add
      IL_00d5: stloc.1      // bi

      // [83 32 - 83 42]
      IL_00d6: ldloc.1      // bi
      IL_00d7: ldarg.1      // last
      IL_00d8: ble.un       IL_0011
    // end of loop

    // [102 7 - 102 16]
    IL_00dd: ldloc.0      // e
    IL_00de: ret

  } // end of method NBody_StructPtr_Official::Energy

  .method private hidebysig static void
    InitBodies(
      valuetype NBody_StructPtr_Official/NBody* bodies,
      valuetype NBody_StructPtr_Official/NBody* last
    ) cil managed
  {
    .maxstack 3
    .locals init (
      [0] float64 vx,
      [1] float64 vy,
      [2] float64 vz,
      [3] valuetype NBody_StructPtr_Official/NBody V_3,
      [4] valuetype NBody_StructPtr_Official/NBody* planet,
      [5] float64 mass
    )

    // [111 7 - 119 9]
    IL_0000: ldarg.0      // bodies
    IL_0001: sizeof       NBody_StructPtr_Official/NBody
    IL_0007: add
    IL_0008: ldloca.s     V_3
    IL_000a: initobj      NBody_StructPtr_Official/NBody
    IL_0010: ldloca.s     V_3
    IL_0012: ldc.r8       0.0376936748703895
    IL_001b: stfld        float64 NBody_StructPtr_Official/NBody::mass
    IL_0020: ldloca.s     V_3
    IL_0022: ldc.r8       4.84143144246472
    IL_002b: stfld        float64 NBody_StructPtr_Official/NBody::x
    IL_0030: ldloca.s     V_3
    IL_0032: ldc.r8       -1.16032004402743
    IL_003b: stfld        float64 NBody_StructPtr_Official/NBody::y
    IL_0040: ldloca.s     V_3
    IL_0042: ldc.r8       -0.103622044471123
    IL_004b: stfld        float64 NBody_StructPtr_Official/NBody::z
    IL_0050: ldloca.s     V_3
    IL_0052: ldc.r8       0.606326392995832
    IL_005b: stfld        float64 NBody_StructPtr_Official/NBody::vx
    IL_0060: ldloca.s     V_3
    IL_0062: ldc.r8       2.81198684491626
    IL_006b: stfld        float64 NBody_StructPtr_Official/NBody::vy
    IL_0070: ldloca.s     V_3
    IL_0072: ldc.r8       -0.0252183616598876
    IL_007b: stfld        float64 NBody_StructPtr_Official/NBody::vz
    IL_0080: ldloc.3      // V_3
    IL_0081: stobj        NBody_StructPtr_Official/NBody

    // [120 7 - 128 9]
    IL_0086: ldarg.0      // bodies
    IL_0087: ldc.i4.2
    IL_0088: conv.i
    IL_0089: sizeof       NBody_StructPtr_Official/NBody
    IL_008f: mul
    IL_0090: add
    IL_0091: ldloca.s     V_3
    IL_0093: initobj      NBody_StructPtr_Official/NBody
    IL_0099: ldloca.s     V_3
    IL_009b: ldc.r8       0.0112863261319688
    IL_00a4: stfld        float64 NBody_StructPtr_Official/NBody::mass
    IL_00a9: ldloca.s     V_3
    IL_00ab: ldc.r8       8.34336671824458
    IL_00b4: stfld        float64 NBody_StructPtr_Official/NBody::x
    IL_00b9: ldloca.s     V_3
    IL_00bb: ldc.r8       4.1247985641243
    IL_00c4: stfld        float64 NBody_StructPtr_Official/NBody::y
    IL_00c9: ldloca.s     V_3
    IL_00cb: ldc.r8       -0.403523417114321
    IL_00d4: stfld        float64 NBody_StructPtr_Official/NBody::z
    IL_00d9: ldloca.s     V_3
    IL_00db: ldc.r8       -1.01077434617879
    IL_00e4: stfld        float64 NBody_StructPtr_Official/NBody::vx
    IL_00e9: ldloca.s     V_3
    IL_00eb: ldc.r8       1.82566237123041
    IL_00f4: stfld        float64 NBody_StructPtr_Official/NBody::vy
    IL_00f9: ldloca.s     V_3
    IL_00fb: ldc.r8       0.00841576137658415
    IL_0104: stfld        float64 NBody_StructPtr_Official/NBody::vz
    IL_0109: ldloc.3      // V_3
    IL_010a: stobj        NBody_StructPtr_Official/NBody

    // [129 7 - 137 9]
    IL_010f: ldarg.0      // bodies
    IL_0110: ldc.i4.3
    IL_0111: conv.i
    IL_0112: sizeof       NBody_StructPtr_Official/NBody
    IL_0118: mul
    IL_0119: add
    IL_011a: ldloca.s     V_3
    IL_011c: initobj      NBody_StructPtr_Official/NBody
    IL_0122: ldloca.s     V_3
    IL_0124: ldc.r8       0.00172372405705971
    IL_012d: stfld        float64 NBody_StructPtr_Official/NBody::mass
    IL_0132: ldloca.s     V_3
    IL_0134: ldc.r8       12.8943695621391
    IL_013d: stfld        float64 NBody_StructPtr_Official/NBody::x
    IL_0142: ldloca.s     V_3
    IL_0144: ldc.r8       -15.1111514016986
    IL_014d: stfld        float64 NBody_StructPtr_Official/NBody::y
    IL_0152: ldloca.s     V_3
    IL_0154: ldc.r8       -0.223307578892656
    IL_015d: stfld        float64 NBody_StructPtr_Official/NBody::z
    IL_0162: ldloca.s     V_3
    IL_0164: ldc.r8       1.08279100644154
    IL_016d: stfld        float64 NBody_StructPtr_Official/NBody::vx
    IL_0172: ldloca.s     V_3
    IL_0174: ldc.r8       0.868713018169608
    IL_017d: stfld        float64 NBody_StructPtr_Official/NBody::vy
    IL_0182: ldloca.s     V_3
    IL_0184: ldc.r8       -0.0108326374013636
    IL_018d: stfld        float64 NBody_StructPtr_Official/NBody::vz
    IL_0192: ldloc.3      // V_3
    IL_0193: stobj        NBody_StructPtr_Official/NBody

    // [138 7 - 146 9]
    IL_0198: ldarg.0      // bodies
    IL_0199: ldc.i4.4
    IL_019a: conv.i
    IL_019b: sizeof       NBody_StructPtr_Official/NBody
    IL_01a1: mul
    IL_01a2: add
    IL_01a3: ldloca.s     V_3
    IL_01a5: initobj      NBody_StructPtr_Official/NBody
    IL_01ab: ldloca.s     V_3
    IL_01ad: ldc.r8       0.00203368686992463
    IL_01b6: stfld        float64 NBody_StructPtr_Official/NBody::mass
    IL_01bb: ldloca.s     V_3
    IL_01bd: ldc.r8       15.3796971148509
    IL_01c6: stfld        float64 NBody_StructPtr_Official/NBody::x
    IL_01cb: ldloca.s     V_3
    IL_01cd: ldc.r8       -25.919314609988
    IL_01d6: stfld        float64 NBody_StructPtr_Official/NBody::y
    IL_01db: ldloca.s     V_3
    IL_01dd: ldc.r8       0.179258772950371
    IL_01e6: stfld        float64 NBody_StructPtr_Official/NBody::z
    IL_01eb: ldloca.s     V_3
    IL_01ed: ldc.r8       0.979090732243898
    IL_01f6: stfld        float64 NBody_StructPtr_Official/NBody::vx
    IL_01fb: ldloca.s     V_3
    IL_01fd: ldc.r8       0.594698998647676
    IL_0206: stfld        float64 NBody_StructPtr_Official/NBody::vy
    IL_020b: ldloca.s     V_3
    IL_020d: ldc.r8       -0.0347559555040781
    IL_0216: stfld        float64 NBody_StructPtr_Official/NBody::vz
    IL_021b: ldloc.3      // V_3
    IL_021c: stobj        NBody_StructPtr_Official/NBody

    // [148 7 - 148 20]
    IL_0221: ldc.r8       0.0
    IL_022a: stloc.0      // vx

    // [148 22 - 148 28]
    IL_022b: ldc.r8       0.0
    IL_0234: stloc.1      // vy

    // [148 30 - 148 36]
    IL_0235: ldc.r8       0.0
    IL_023e: stloc.2      // vz

    // [149 12 - 149 38]
    IL_023f: ldarg.0      // bodies
    IL_0240: sizeof       NBody_StructPtr_Official/NBody
    IL_0246: add
    IL_0247: stloc.s      planet

    IL_0249: br.s         IL_0286
    // start of loop, entry point: IL_0286

      // [150 9 - 150 36]
      IL_024b: ldloc.s      planet
      IL_024d: ldfld        float64 NBody_StructPtr_Official/NBody::mass
      IL_0252: stloc.s      mass

      // [151 9 - 151 33]
      IL_0254: ldloc.0      // vx
      IL_0255: ldloc.s      planet
      IL_0257: ldfld        float64 NBody_StructPtr_Official/NBody::vx
      IL_025c: ldloc.s      mass
      IL_025e: mul
      IL_025f: add
      IL_0260: stloc.0      // vx

      // [152 9 - 152 33]
      IL_0261: ldloc.1      // vy
      IL_0262: ldloc.s      planet
      IL_0264: ldfld        float64 NBody_StructPtr_Official/NBody::vy
      IL_0269: ldloc.s      mass
      IL_026b: mul
      IL_026c: add
      IL_026d: stloc.1      // vy

      // [153 9 - 153 33]
      IL_026e: ldloc.2      // vz
      IL_026f: ldloc.s      planet
      IL_0271: ldfld        float64 NBody_StructPtr_Official/NBody::vz
      IL_0276: ldloc.s      mass
      IL_0278: mul
      IL_0279: add
      IL_027a: stloc.2      // vz

      // [149 56 - 149 64]
      IL_027b: ldloc.s      planet
      IL_027d: sizeof       NBody_StructPtr_Official/NBody
      IL_0283: add
      IL_0284: stloc.s      planet

      // [149 40 - 149 54]
      IL_0286: ldloc.s      planet
      IL_0288: ldarg.1      // last
      IL_0289: ble.un.s     IL_024b
    // end of loop

    // [155 7 - 155 32]
    IL_028b: ldarg.0      // bodies
    IL_028c: ldc.r8       39.4784176043574
    IL_0295: stfld        float64 NBody_StructPtr_Official/NBody::mass

    // [156 7 - 156 36]
    IL_029a: ldarg.0      // bodies
    IL_029b: ldloc.0      // vx
    IL_029c: ldc.r8       -39.4784176043574
    IL_02a5: div
    IL_02a6: stfld        float64 NBody_StructPtr_Official/NBody::vx

    // [157 7 - 157 36]
    IL_02ab: ldarg.0      // bodies
    IL_02ac: ldloc.1      // vy
    IL_02ad: ldc.r8       -39.4784176043574
    IL_02b6: div
    IL_02b7: stfld        float64 NBody_StructPtr_Official/NBody::vy

    // [158 7 - 158 36]
    IL_02bc: ldarg.0      // bodies
    IL_02bd: ldloc.2      // vz
    IL_02be: ldc.r8       -39.4784176043574
    IL_02c7: div
    IL_02c8: stfld        float64 NBody_StructPtr_Official/NBody::vz

    // [160 3 - 160 4]
    IL_02cd: ret

  } // end of method NBody_StructPtr_Official::InitBodies
} // end of class NBody_StructPtr_Official
