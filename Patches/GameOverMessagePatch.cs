using HarmonyLib;
using RandomizedTexts.Extensions;
using TMPro;
using UnityEngine;

namespace RandomizedTexts.Patches;

internal class GameOverMessagePatch
{
    [HarmonyPatch(typeof(HUDManager), nameof(HUDManager.ShowPlayersFiredScreen))]
    [HarmonyPrefix]
    private static void ShowPlayersFiredScreenPreFix(StartOfRound __instance)
    {
        if (RandomizedTexts.gameOverMessages.Count > 0)
        {
            GameObject gameOverTextObject = GameObject.Find("Systems/UI/Canvas/GameOverScreen/MaskImage/HeaderText");
            if (gameOverTextObject != null)
            {
                TextMeshProUGUI component = gameOverTextObject.GetComponent<TextMeshProUGUI>();
                string selectedText;
                if (RandomizedTexts.seedBasedRandom)
                {
                    System.Random rand = new(StartOfRound.Instance.randomMapSeed + 169);
                    selectedText = rand.NextItem(RandomizedTexts.gameOverMessages);
                }
                else
                {
                    System.Random rand = new();
                    selectedText = rand.NextItem(RandomizedTexts.gameOverMessages);
                }
                component.text = selectedText;
                component.fontSize = RandomizedTexts.gameOverFontSize;
            }
        }
        if (RandomizedTexts.gameOverSubtitles.Count > 0)
        {
            GameObject gameOverSubtitleTextObject = GameObject.Find("Systems/UI/Canvas/GameOverScreen/MaskImage/HeaderText (1)");
            if (gameOverSubtitleTextObject != null)
            {
                TextMeshProUGUI component = gameOverSubtitleTextObject.GetComponent<TextMeshProUGUI>();
                string selectedText;
                if (RandomizedTexts.seedBasedRandom)
                {
                    System.Random rand = new(StartOfRound.Instance.randomMapSeed + 160);
                    selectedText = rand.NextItem(RandomizedTexts.gameOverSubtitles);
                }
                else
                {
                    System.Random rand = new();
                    selectedText = rand.NextItem(RandomizedTexts.gameOverSubtitles);
                }
                component.text = selectedText;
                component.fontSize = RandomizedTexts.gameOverSubtitleFontSize;
            }
        }
    }
}
