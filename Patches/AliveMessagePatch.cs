using HarmonyLib;
using UnityEngine;
using TMPro;
using RandomizedTexts.Extensions;

namespace RandomizedTexts.Patches;

internal class AliveMessagePatch
{
    [HarmonyPatch(typeof(StartOfRound), nameof(StartOfRound.Start))]
    [HarmonyPostfix]
    private static void StartPatch(StartOfRound __instance)
    {
        GameObject aliveTextObject = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/BottomMiddle/SystemsOnline/TipLeft1");
        if (aliveTextObject != null)
        {
            TextMeshProUGUI component = aliveTextObject.GetComponent<TextMeshProUGUI>();
            string selectedText;
            if (RandomizedTexts.seedBasedRandom)
            {
                System.Random rand = new(StartOfRound.Instance.randomMapSeed + 29);
                selectedText = rand.NextItem(RandomizedTexts.aliveMessages);
            }
            else
            {
                System.Random rand = new();
                selectedText = rand.NextItem(RandomizedTexts.aliveMessages);
            }
            component.text = selectedText;
            component.fontSize = RandomizedTexts.aliveFontSize;
        }
    }

    [HarmonyPatch(typeof(HUDManager), nameof(HUDManager.RemoveSpectateUI))]
    [HarmonyPostfix]
    private static void RemoveSpectateUIPatch(HUDManager __instance)
    {
        GameObject aliveTextObject = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/BottomMiddle/SystemsOnline/TipLeft1");
        if (aliveTextObject != null)
        {
            TextMeshProUGUI component = aliveTextObject.GetComponent<TextMeshProUGUI>();
            string selectedText;
            if (RandomizedTexts.seedBasedRandom)
            {
                System.Random rand = new(StartOfRound.Instance.randomMapSeed + 39);
                selectedText = rand.NextItem(RandomizedTexts.aliveMessages);
            }
            else
            {
                System.Random rand = new();
                selectedText = rand.NextItem(RandomizedTexts.aliveMessages);
            }
            component.text = selectedText;
            component.fontSize = RandomizedTexts.aliveFontSize;
        }
    }
}
