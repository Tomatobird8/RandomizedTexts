using HarmonyLib;

namespace RandomizedTexts.Patches;

internal class AliveMessagePatch
{
    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Start))]
    [HarmonyPatch(typeof(HUDManager), nameof(HUDManager.RemoveSpectateUI))]
    [HarmonyPostfix]
    private static void StartPatch(StartOfRound __instance)
    {
        RandomizedTexts.ChangeText("Systems/UI/Canvas/IngamePlayerHUD/BottomMiddle/SystemsOnline/TipLeft1", "Systems Online", RandomizedTexts.aliveMessages, RandomizedTexts.aliveFontSize, 29);
    }
}
