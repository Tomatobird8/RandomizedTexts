using HarmonyLib;

namespace RandomizedTexts.Patches;

internal class GameOverMessagePatch
{
    [HarmonyPatch(typeof(HUDManager), nameof(HUDManager.ShowPlayersFiredScreen))]
    [HarmonyPrefix]
    private static void ShowPlayersFiredScreenPreFix(StartOfRound __instance)
    {
        if (RandomizedTexts.gameOverMessages.Count > 0)
        {
            RandomizedTexts.ChangeText("Systems/UI/Canvas/GameOverScreen/MaskImage/HeaderText", "You Are Fired", RandomizedTexts.gameOverMessages, RandomizedTexts.gameOverFontSize, 169);
        }
        if (RandomizedTexts.gameOverSubtitles.Count > 0)
        {
            RandomizedTexts.ChangeText("Systems/UI/Canvas/GameOverScreen/MaskImage/HeaderText (1)", "You Are Fired Subtitle", RandomizedTexts.gameOverSubtitles, RandomizedTexts.gameOverSubtitleFontSize, 160);
        }
    }
}
