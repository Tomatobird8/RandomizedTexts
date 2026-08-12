using GameNetcodeStuff;
using HarmonyLib;
using RandomizedTexts.Extensions;
using TMPro;
using UnityEngine;

namespace RandomizedTexts.Patches;

internal class HandsFullMessagePatch
{
    [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.SwitchToItemSlot))]
    [HarmonyPostfix]
    private static void SwitchToItemSlotPostFix(PlayerControllerB __instance)
    {
        if (__instance.IsOwner && HUDManager.Instance.holdingTwoHandedItem.enabled)
        {
            GameObject handsFullTextObject = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/HandsFullText");
            if (handsFullTextObject != null)
            {
                TextMeshProUGUI component = handsFullTextObject.GetComponent<TextMeshProUGUI>();
                string selectedText;
                if (RandomizedTexts.seedBasedRandom)
                {
                    System.Random rand = new(StartOfRound.Instance.randomMapSeed + 38);
                    selectedText = rand.NextItem(RandomizedTexts.handsFullMessages);
                }
                else
                {
                    System.Random rand = new();
                    selectedText = rand.NextItem(RandomizedTexts.handsFullMessages);
                }
                component.text = selectedText;
                component.fontSize = RandomizedTexts.handsFullFontSize;
            }
        }
    }
}
