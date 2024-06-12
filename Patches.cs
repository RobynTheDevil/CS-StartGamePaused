using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;
using SecretHistories.Infrastructure;
using HarmonyLib;

public class MainPatch : Patch
{
    public MainPatch()
    {
        var method = AccessTools.Method(typeof(GameGateway), "LoadGame");
        var attr = method.GetCustomAttribute<AsyncStateMachineAttribute>();
        this.original = AccessTools.Method(attr.StateMachineType, "MoveNext");
        this.patch = AccessTools.Method(typeof(MainPatch), "Transpiler");
    }

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        int index = PatchHelper.FindOpcode(codes, OpCodes.Ldc_I4_2); // ControlPriorityLevel=2
        index = PatchHelper.FindOpcode(codes, OpCodes.Ldarg_0, start: index); //this
        codes[index]   = new CodeInstruction(OpCodes.Nop, null);
        codes[index+1] = new CodeInstruction(OpCodes.Nop, null); //ldfld gamepersistenceprovider
        codes[index+2] = new CodeInstruction(OpCodes.Ldc_I4_0); //callvirt getdefaultgamespeed -> enum paused
        return codes.AsEnumerable();
    }

}

