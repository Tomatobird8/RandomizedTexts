using HarmonyLib;

namespace RandomizedTexts.Patches;
internal class LoadingLevelPatch
{
    [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.GenerateNewLevelClientRpc))]
    [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.Start))]
    [HarmonyPostfix]
    private static void GenerateNewLevelClientRpc_Postfix()
    {
        RandomizedTexts.ChangeText("Systems/UI/Canvas/LoadingText/LoadText", "Entering The Atmosphere", RandomizedTexts.landingToMoonMessages, RandomizedTexts.landingToMoonFontSize, 114);
    }
}
