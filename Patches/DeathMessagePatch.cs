using GameNetcodeStuff;
using HarmonyLib;

namespace RandomizedTexts.Patches;

internal class DeathMessagePatch
{
    [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.KillPlayer))]
    [HarmonyPrefix]
    private static void KillPlayerPrefix(PlayerControllerB __instance)
    {
        RandomizedTexts.ChangeText("Systems/UI/Canvas/DeathScreen/GameOverText", "Life Support: Offline", RandomizedTexts.deathMessages, RandomizedTexts.deathFontSize, 420);
    }
}
