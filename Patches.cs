using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using SecretHistories.Infrastructure;
using HarmonyLib;

public class MainPatch : Patch
{
    public MainPatch()
    {
        this.original = AccessTools.Method(typeof(GameGateway), "LoadGame");
        this.patch = AccessTools.Method(typeof(MainPatch), "Transpiler");
    }

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        int index = PatchHelper.FindOpcode(codes, OpCodes.Newobj);
        index = PatchHelper.FindOpcode(codes, OpCodes.Ldarg_1, start: index);
        codes[index]   = new CodeInstruction(OpCodes.Nop, null); //previously gamePersistanceProvider
        codes[index+1] = new CodeInstruction(OpCodes.Ldc_I4_0); //enum paused
        return codes.AsEnumerable();
    }

}

