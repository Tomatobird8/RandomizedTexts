using HarmonyLib;

namespace RandomizedTexts.Patches;
internal class LoadingGamePatch
{
    [HarmonyPatch(typeof(MenuManager), nameof(MenuManager.SetLoadingScreen))]
    [HarmonyPrefix]
    internal static void SetLoadingScreen_Prefix()
    {
        RandomizedTexts.ChangeText("Canvas/MenuContainer/LoadingScreen/LoadingTextContainer/LoadingText", "Loading (Game/Lobby)", RandomizedTexts.loadingMessages, RandomizedTexts.loadingFontSize, 518);
    }
}
