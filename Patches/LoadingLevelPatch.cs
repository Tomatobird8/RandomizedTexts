using HarmonyLib;
using RandomizedTexts.Extensions;
using TMPro;
using UnityEngine;

namespace RandomizedTexts.Patches;
internal class LoadingLevelPatch
{
    [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.GenerateNewLevelClientRpc))]
    [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.Start))]
    [HarmonyPrefix]
    private static void GenerateNewLevelClientRpc_Prefix()
    {
        GameObject loadingTextObject = GameObject.Find("Systems/UI/Canvas/LoadingText/LoadText");
        if (loadingTextObject != null)
        {
            TextMeshProUGUI component = loadingTextObject.GetComponent<TextMeshProUGUI>();
            string selectedText;
            if (RandomizedTexts.seedBasedRandom)
            {
                System.Random rand = new(StartOfRound.Instance.randomMapSeed + 466);
                selectedText = rand.NextItem(RandomizedTexts.loadingTextMessages);
            }
            else
            {
                System.Random rand = new();
                selectedText = rand.NextItem(RandomizedTexts.loadingTextMessages);
            }
            component.text = selectedText;
            component.fontSize = RandomizedTexts.loadingTextFontSize;
        }
    }
}
