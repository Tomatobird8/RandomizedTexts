using GameNetcodeStuff;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace RandomizedTexts.Patches;
internal class PlayerJoinMessagePatch
{
    private static string FormatJoinMessage(string username)
    {
        return RandomizedTexts.SelectRandom(RandomizedTexts.playerJoinMessages, 240).Replace("&$", username);
    }

    [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.ConnectClientToPlayerObject))]
    [HarmonyTranspiler]
    internal static IEnumerable<CodeInstruction> ConnectClientToPlayerObject_Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        FieldInfo playerUsernameField = AccessTools.Field(typeof(PlayerControllerB), nameof(PlayerControllerB.playerUsername));
        MethodInfo customFormatMethod = AccessTools.Method(typeof(PlayerJoinMessagePatch), nameof(FormatJoinMessage));

        return new CodeMatcher(instructions)
            .MatchForward(false, new CodeMatch(OpCodes.Ldfld, playerUsernameField))
            .ThrowIfInvalid("Could not find ldfld playerUsername")
            .RemoveInstructions(3)
            .Insert(
                new CodeInstruction(OpCodes.Ldfld, playerUsernameField),
                new CodeInstruction(OpCodes.Call, customFormatMethod)
            )
            .InstructionEnumeration();
    }
}
